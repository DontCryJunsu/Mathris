//  /*********************************************************************************
//   *********************************************************************************
//   *********************************************************************************
//   * Produced by Skard Games										                 *
//   * Facebook: https://goo.gl/5YSrKw											     *
//   * Contact me: https://goo.gl/y5awt4								             *
//   * Developed by Cavit Baturalp Gürdin: https://tr.linkedin.com/in/baturalpgurdin *
//   *********************************************************************************
//   *********************************************************************************
//   *********************************************************************************/

using UnityEngine;
using System.Collections;

public class ContinueButton : MonoBehaviour {

    public void Start()
    {
        Managers.Audio.PlayUIClick();
        //SpawnManager.Instance.InitSpawn();
        Managers.Game.SetState(typeof(GamePlayState));
    }
}
