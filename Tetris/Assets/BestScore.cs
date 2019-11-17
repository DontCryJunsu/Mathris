using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BestScore : MonoBehaviour
{
   [SerializeField] Text text;
    void Start()
    {
        text.text = PlayerPrefs.GetInt("Total").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
