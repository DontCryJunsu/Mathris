using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISplash : MonoBehaviour
{
    public GameObject Authenticate;
    public GameObject Home;
    public int splashIntervalTime = 3;
    public int fadeTime = 1;

    // the image you want to fade, assign in inspector
    public Image img;

    private void Start()
    {
        // fades the image out when you click
        StartCoroutine(FadeImage(true));
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        yield return new WaitForSeconds(splashIntervalTime);

        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = fadeTime; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= fadeTime; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }

        Home.SetActive(true);
    }

}
