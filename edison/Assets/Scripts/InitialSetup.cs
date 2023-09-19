/// <summary>
/// InitialSetup.cs
/// fix timestep at 1/90 for Quest2
/// initialize wristband, clipboard position
/// show peachfields stage1 buildings+solar
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class InitialSetup : MonoBehaviour {

        public FadeScreen fadeScreen;

        public GameObject playerRig;

        public Transform resetLoc;

        public Vector3 playerOffsetForCV1;
        public Vector3 playerOffsetForQuest;

        public GameObject clipboards;
        public GameObject wristband;

        public ClipSolarManager solarScript;

        private void Awake() {

            // fade screen in
            StartCoroutine(FadeTheScreen());

            // set Oculus Quest2 display frequency
            OVRManager.display.displayFrequency = 90f;

            // Quest and Rift place Player at different positions (don't know why), so adjust accordingly
            //HeadsetOffset();

            // snap the player to a starting position (same as the reset location)
            playerRig.transform.position = resetLoc.transform.position;
            playerRig.transform.rotation = resetLoc.transform.rotation;

            // attach the clipboards to the Player
            GameObject _clipboards = GameObject.FindGameObjectWithTag("clipboards");
            clipboards.transform.SetParent(_clipboards.transform, false);

            // attach the wristband to Player and set it as active
            GameObject _wristband = GameObject.FindGameObjectWithTag("wristband");
            wristband.transform.SetParent(_wristband.transform, false);
            wristband.SetActive(true);

            // show solar p1
            solarScript.p1Trigger();

        }

        IEnumerator FadeTheScreen() {

            fadeScreen.FadeIn();
            yield return new WaitForSeconds(fadeScreen.fadeDuration);
        }

        //void HeadsetOffset() {

            //OVRPlugin.SystemHeadset _headset = OVRPlugin.GetSystemHeadsetType();
            //Debug.Log("Headset type : " + _headset);

            //if (_headset.ToString() == "Rift_CV1") {

            //    //playerRig.transform.position += playerOffsetForCV1;
            //    playerRig.transform.Translate(playerOffsetForCV1, Space.World);
            //}

            //if (_headset.ToString() == "Oculus_Quest_2") {

            //    playerRig.transform.position += playerOffsetForQuest;
            //}
        //}
    }
}
