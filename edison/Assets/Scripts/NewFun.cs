/// <summary>
/// connect/disconnect network long-press button
/// has four picture states : connect, connect-activated, disconnect, disconnect-activated
/// starts another script to handle event
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace com.jonrummery.edison {

    // can't have a trigger without a collider
    [RequireComponent(typeof(MeshCollider))]

    public class NewFun : MonoBehaviour {

        [Header("Script with trigger action script")]
        public ClipNetwork handoverScript;

        [Header("Button picture model")]
        public GameObject buttonPic;
        private Material _buttonPicMat;

        [Header("The rotating part of the button")]
        public GameObject buttonBody;

        [Header("Materials with pics for buttons")]
        public Texture2D connectPic;
        public Texture2D connectActivatedPic;
        public Texture2D disconnectPic;
        public Texture2D disconnectActivatedPic;

        // for trigger collider
        private MeshCollider _mesh;

        [Header("Haptics")]
        [Range(0, 1)]
        public float hapticFrequency;
        [Range(0, 1)]
        public float hapticAmplitude;
        public float hapticDelay;
        public float arrowSpeed;

        // flag for whether player's finger is still in the trigger
        private bool _stillThere;

        private void Start() {

            // initialize our convex mesh collider trigger
            _mesh = GetComponent<MeshCollider>();
            _mesh.convex = true;
            _mesh.isTrigger = true;

            // grab the material element to change the button picture
            _buttonPicMat = buttonPic.GetComponent<MeshRenderer>().material;
        }

        private void OnTriggerEnter(Collider other) {

            // (1) is it the collider we're looking for?
            if ((other.tag == "bingo") &&

                // (2) is the right-hand middle-finger trigger held down?
                ((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 1)) &&

                // (3) finally, check the index finger trigger isn't held down...
                (!OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))) {

                // set the flag
                _stillThere = true;

                // show the activated version of the button pic
                _buttonPicMat.mainTexture = PhotonNetwork.IsConnected == true ? disconnectActivatedPic : connectActivatedPic;

                // reinforce button-press with haptics
                StartCoroutine("HapticFeedback");

                // initialize delay
                StartCoroutine("WaitABit");
            }
        }

        private void OnTriggerExit(Collider other) {

            // (1) is it the collider we're looking for?
            if ((other.tag == "bingo")) {// &&
                                         // (2) is the right-hand middle-finger trigger held down?
                                         //  ((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 1)) &&
                                         // (3) finally, check the index finger trigger isn't held down...
                                         //  (!OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))) {

                // set the flag
                _stillThere = false;

                // reset arrow and button pic
                ResetArrow();

                // stop the haptics
                OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
            }
        }

        IEnumerator WaitABit() {

            yield return new WaitForSeconds(hapticDelay);

            if (_stillThere) {

                // reset arrow
                ResetArrow();

                // DO SOMETHING
                handoverScript.ConnectTrigger();
            }
        }

        private void Update() {

            // if player's finger is still in trigger, continue spinning the arrow
            if (_stillThere) {
                buttonBody.transform.Rotate(Vector3.up * arrowSpeed * Time.deltaTime, Space.Self);
            }
        }

        void ResetArrow() {

            // reset the flag
            _stillThere = false;

            // reset the arrow rotation
            buttonBody.transform.rotation = new Quaternion(0, 0, 0, 0);

            // change the button pic
            _buttonPicMat.mainTexture = PhotonNetwork.IsConnected == true ? disconnectPic : connectPic;
        }

        IEnumerator HapticFeedback() {

            // set haptics on and off
            OVRInput.SetControllerVibration(hapticFrequency, hapticAmplitude, OVRInput.Controller.RTouch);
            yield return new WaitForSeconds(hapticDelay);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
    }
}
