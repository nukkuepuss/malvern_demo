/// <summary>
/// tutorial long-press button
/// passess control to TutorialManager
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class TutorialButton2Trigger : MonoBehaviour {

        [Header("Script with trigger action code")]
        public TutorialClipboard nextScript;

        public GameObject buttonBody;
        public GameObject buttonPic;

        public Material onPic, offPic;

        public float arrowSpeed;
        public float hapticDelay;
        private bool stillThere;
        private float hapticFrequency = 0.3f, hapticAmplitude = 0.3f;

        private void Start() {

            // set the button 'off' state
            buttonPic.GetComponent<MeshRenderer>().material = offPic;
        }

        private void Update() {

            // rotate the button if player's finger is still in collider
            if (stillThere) {
                buttonBody.transform.Rotate(Vector3.up * arrowSpeed * Time.deltaTime, Space.Self);
            }
        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log("reset-OnTriggerEnter called by " + this.gameObject.name + " due to " + other.tag);

            // (1) is it the collider we're looking for? It's on the Quest PUN Template RHand in Resources.
            if ((other.tag == "bingo") &&

                // (2) is the right-hand middle-finger trigger held down?
                ((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 1)) &&

                // (3) finally, check the index finger trigger isn't held down...
                (!OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))) {

                // set the flag
                stillThere = true;

                //set the button 'on' state
                buttonPic.GetComponent<MeshRenderer>().material = onPic;

                // reinforce button-press with haptics
                StartCoroutine("Haptics");

                // initialize delay
                StartCoroutine("WaitABit");
            }
        }

        private void OnTriggerExit(Collider other) {
            // (1) is it the collider we're looking for? It's on the Quest PUN Template RHand in Resources.
            if ((other.tag == "bingo")) {

                // set the flag
                stillThere = false;

                // reset the timer
                StopCoroutine("WaitABit");

                // reset arrow
                ResetArrow();

                // stop the haptics
                OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
            }
        }

        IEnumerator WaitABit() {

            yield return new WaitForSeconds(hapticDelay);

            // is the Player's finger still in the collider?
            if (stillThere) {

                // reset arrow
                ResetArrow();

                // do something
                nextScript.Clip3CircularButton();
            }
        }

        void ResetArrow() {

            // reset the flag
            stillThere = false;

            // set the button 'off' state
            buttonPic.GetComponent<MeshRenderer>().material = offPic;

            // reset the arrow rotation
            buttonBody.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        IEnumerator Haptics() {

            // set haptics on and off
            OVRInput.SetControllerVibration(hapticFrequency, hapticAmplitude, OVRInput.Controller.LTouch);
            yield return new WaitForSeconds(hapticDelay);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
    }
}