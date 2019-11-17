using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Stage8 : MonoBehaviour
{
    public Transform[] numberTransforms;
    public int[] numberArray;
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] GameObject nextLevelPanel;
    bool check;

    void Start()
    {
        StageManager.Instance.numbers.Clear();
        StageManager.Instance.operatorNumberIndex = 0;
        StageManager.Instance.numberArrayIndex = 0;
        ArrayInitialize();
        OperationNumberInitialize();
    }
    void Update()
    {
        if (StageManager.Instance.operatorNumberIndex >= numberTransforms.Length && !check)
            Operate();
    }
    void OperationNumberInitialize()
    {
        StageManager.Instance.numberTransforms = numberTransforms;
    }
    void ArrayInitialize()
    {
        StageManager.Instance.numberArray = numberArray;
    }
    // 이거수정
    void Operate()
    {
        check = true;

        int thisStageScore = StageManager.Instance.numbers[0] * (StageManager.Instance.numbers[1]*10 + StageManager.Instance.numbers[2]) / StageManager.Instance.numbers[3];
        int currentStageScore = PlayerPrefs.GetInt("Total")+thisStageScore;

        PlayerPrefs.SetInt("Total", currentStageScore);
        PlayerPrefs.SetInt("Stage8", thisStageScore);
        GameManager.Instance.SetState(typeof(GameOverState));
        gameoverPanel.SetActive(false);
        nextLevelPanel.SetActive(true);
    }
}
