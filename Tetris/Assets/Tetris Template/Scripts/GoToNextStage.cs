using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextStage : MonoBehaviour
{
    // Start is called before the first frame update
    public void NextStage()
    {
        StageManager.Instance.currentStage++;
        SceneManager.LoadScene(StageManager.Instance.currentStage);
    }
}