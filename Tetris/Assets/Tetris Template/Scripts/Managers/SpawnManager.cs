using UnityEngine;
using System.Collections;
using Service;
public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] Transform numberParent;
    public GameObject[] shapeTypes;
    public GameObject numberPrefab;
    GameObject nextObject;
    public int spawnNumberIndex = 1;

    public void Start() =>
        nextObject = Instantiate(shapeTypes[Random.Range(0, shapeTypes.Length)]);

    bool SpawnNumber()
    {
        if(spawnNumberIndex % 10 == 2 || spawnNumberIndex % 10 == 5 || spawnNumberIndex % 10 == 8)
        {
            GameObject numberObject = Instantiate(numberPrefab);
            numberObject.GetComponent<Number>().targetTransform = nextObject.transform;
            nextObject.GetComponent<BlockParent>().number = numberObject.GetComponent<Number>();
            return true;
        }
        return false;
    }

    public void Spawn()
    {
        int i = Random.Range(0, shapeTypes.Length);
        GameObject currentObject = nextObject;
        currentObject.GetComponent<TetrisShape>().enabled = true;
        currentObject.GetComponent<ShapeMovementController>().enabled = true;
        currentObject.transform.position = new Vector3(4, 15, 0);
        nextObject = Instantiate(shapeTypes[i], numberParent);
        if (SpawnNumber())
            nextObject.GetComponent<TetrisShape>().AssignColor();
        spawnNumberIndex++;
        Managers.Game.currentShape = currentObject.GetComponent<TetrisShape>();
        currentObject.transform.parent = Managers.Game.blockHolder;
        Managers.Input.isActive = true;
    }
}
