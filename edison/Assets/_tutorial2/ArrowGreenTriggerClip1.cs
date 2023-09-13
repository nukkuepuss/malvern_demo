/// <summary>
/// Tutorial clipboard, green arrow trigger
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class ArrowGreenTriggerClip1 : MonoBehaviour {

        [Header("Script with trigger action code")]
        public TutorialClipboard tutorialManager;

        [Header("Haptics")]
        [Range(0, 1)]
        public float hapticFrequency;
        [Range(0, 1)]
        public float hapticAmplitude;

        private void OnTriggerEnter(Collider other) {

            // (1) is it the collider we're looking for? It's on the Quest PUN Template RHand in Resources.
            if ((other.tag == "bingo") &&

                // (2) is the right-hand middle-finger trigger held down?
                ((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 1)) &&

                // (3) finally, check the index finger trigger isn't held down...
                (!OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))) {

                // reinforce button-press with haptics
                StartCoroutine("HapticFeedback");

                //do something
                tutorialManager.Clip3GreenArrowTrigger();
            }
        }

        IEnumerator HapticFeedback() {
            // set haptics on and off
            OVRInput.SetControllerVibration(hapticFrequency, hapticAmplitude, OVRInput.Controller.LTouch);
            yield return new WaitForSeconds(0.01f);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
    }
}
