/// <summary>
/// TimeStep.cs
/// fix timestep at 1/90 for Quest2
/// adjust the player position when using CV1 (it's off by z:0.7 otherwise)
/// initialize wristband and clipboard position
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class TimeStep : MonoBehaviour {

        public GameObject playerRig;
        public Vector3 playerOffsetForCV1;
        public Vector3 playerOffsetForQuest;

        public GameObject clipboards;
        public GameObject wristband;

        private void Awake() {

            // set Oculus Quest2 display frequency
            OVRManager.display.displayFrequency = 90f;

            // Quest and Rift place Player at different positions (don't know why), so adjust accordingly
            HeadsetOffset();

            // attach the clipboards to the Player
            GameObject _clipboards = GameObject.FindGameObjectWithTag("clipboards");
            clipboards.transform.SetParent(_clipboards.transform, false);

            // attach the wristband to Player and set it as active
            GameObject _wristband = GameObject.FindGameObjectWithTag("wristband");
            wristband.transform.SetParent(_wristband.transform, false);
            wristband.SetActive(true);
            
        }

        void HeadsetOffset() {

            OVRPlugin.SystemHeadset _headset = OVRPlugin.GetSystemHeadsetType();
            Debug.Log("Headset type : " + _headset);

            if (_headset.ToString() == "Rift_CV1") {

                playerRig.transform.localPosition += playerOffsetForCV1;
            }

            if (_headset.ToString() == "Oculus_Quest_2") {

                playerRig.transform.localPosition += playerOffsetForQuest;
            }
        }
    }
}
