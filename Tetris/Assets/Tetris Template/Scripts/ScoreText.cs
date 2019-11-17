using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreText : MonoBehaviour
{
    [SerializeField] Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = PlayerPrefs.GetInt("Stage" + StageManager.Instance.currentStage.ToString()).ToString();
    }
}
