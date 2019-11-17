//  /*********************************************************************************
//   *********************************************************************************
//   *********************************************************************************
//   * Produced by Skard Games										                  *
//   * Facebook: https://goo.gl/5YSrKw											      *
//   * Contact me: https://goo.gl/y5awt4								              *											
//   * Developed by Cavit Baturalp Gürdin: https://tr.linkedin.com/in/baturalpgurdin *
//   *********************************************************************************
//   *********************************************************************************
//   *********************************************************************************/

using UnityEngine;
using System.Collections;

public class GameBackButton : MonoBehaviour {

    [SerializeField] GameObject panel;
	public void OnClickBackButton()
	{
        if (Time.timeScale == 0)
        {
            panel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            panel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
