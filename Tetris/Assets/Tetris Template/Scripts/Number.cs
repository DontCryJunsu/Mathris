using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public Sprite[] numberSprites;
    public Transform targetTransform;
    public int number = 1;
    public bool follow;

    void Start()
    {
        //if (NumberManager.Instance.startIndex % NumberManager.Instance.numberArray.Length == NumberManager.Instance.numberArray.Length-1)
        //{
        //    NumberManager.Instance.Shuffle();
        //}
        number = StageManager.Instance.numberArray[StageManager.Instance.numberArrayIndex % StageManager.Instance.numberArray.Length];
        spriteRenderer.sprite = numberSprites[number-1];
        StageManager.Instance.numberArrayIndex++;
    }
    void Update()
    {
        if (targetTransform)
        {
            if(!follow)
                transform.position = targetTransform.transform.position;
            else
            transform.position = Vector3.Lerp(transform.position, targetTransform.position, Time.deltaTime * 10);
        }
    }

}
