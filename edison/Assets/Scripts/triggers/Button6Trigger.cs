/// <summary>
/// Button6Trigger.cs
/// reset button - back to lobby
/// player's finger must stay in trigger until arrow has completed full circle
/// *** adapted from a locatio-based reset - old code still here
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;

namespace com.jonrummery.edison {

    public class Button6Trigger : MonoBehaviour {

        public FadeScreen fadeScreen;

        public GameObject buttonBody;
        public GameObject buttonPic;
        public Material onPic, offPic;

        public GameObject player;
        public Transform playerResetLocation;

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

                // reset arrow
                ResetArrow();

                // halt the countdown
                StopCoroutine("WaitABit");

                //// set the button 'on' state
                //buttonPic.GetComponent<MeshRenderer>().material = onPic;

                // stop the haptics
                OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
            }
        }

        IEnumerator WaitABit() {

            yield return new WaitForSeconds(hapticDelay);

            if (stillThere) {

                // reset arrow
                ResetArrow();

                // this is the button action : to reset the player back to the lobby
                FadeToScene();
            }
        }

        public void FadeToScene() {

            StartCoroutine(FadeToSceneRoutine());
        }

        IEnumerator FadeToSceneRoutine() {

            fadeScreen.FadeOut();
            yield return new WaitForSeconds(fadeScreen.fadeDuration);

            // disconnect from PUN server
            PhotonNetwork.Disconnect();

            // load next scene
            SceneManager.LoadScene(0);
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