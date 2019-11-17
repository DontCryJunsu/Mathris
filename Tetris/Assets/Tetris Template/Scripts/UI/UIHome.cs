using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHome : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnGameStartButtonClicked()
    {
        // TODO: 현재 레벨에 따라 다른 화면 로딩
        SceneManager.LoadScene(1);
    }

    public void OnRankingButtonClicked()
    {

    }
}
