using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{
    [SerializeField] Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = PlayerPrefs.GetInt("Total").ToString();
    }
}
