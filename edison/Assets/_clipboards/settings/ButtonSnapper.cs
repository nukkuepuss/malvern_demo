/// <summary>
/// ButtonSnapper.cs
/// trigger a button press
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class ButtonSnapper : MonoBehaviour {

        [Header("Is this Button 1 or 2? Left=1, Right=2")]
        [Range(1, 2)]
        public int thisButton;

        [Header("Handover script")]
        public SnapSettings handoverScript;

        [Header("Haptics")]
        [Range(0, 1)]
        public float hapticFrequency;
        [Range(0, 1)]
        public float hapticAmplitude;

        //private void Start() {

        //    // initialize button1
        //    if (thisButton==1) {

        //        // move the button down a bit
        //        transform.Translate(0f, -handoverScript.buttonMovement, 0f);
        //    }
        //}

        private void OnTriggerEnter(Collider other) {

            // (1) is it the collider we're looking for? It's on the Quest PUN Template RHand in Resources.
            if ((other.tag == "bingo") &&

                // (2) is the right-hand middle-finger trigger held down?
                ((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 1)) &&

                // (3) finally, check the index finger trigger isn't held down...
                (!OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))) {

                // is this button ready to be pressed?
                if (handoverScript.activeButton!=thisButton) {

                    // reinforce button-press with haptics
                    StartCoroutine("HapticFeedback");

                    // toggle the flag
                    handoverScript.activeButton = thisButton;
                }
            }
        }

        IEnumerator HapticFeedback() {
            // set haptics on and off
            OVRInput.SetControllerVibration(hapticFrequency, hapticAmplitude, OVRInput.Controller.RTouch);
            yield return new WaitForSeconds(0.1f);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
    }
}
