/// <summary>
/// connect trigger button -> networking script
/// button picture must be next mesh renderer in hierarchy
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace com.jonrummery.edison {

    public class ConnectTrigger : MonoBehaviour {

        [Header("Script with trigger action script")]
        public NetworkClipboard networkScript;

        [Header("Button picture model")]
        public GameObject buttonPic;
        private Material _buttonPic;

        [Header("The rotating part of the button")]
        public GameObject buttonBody;

        [Header("Materials with pics for buttons")]
        public Material connectPic;
        public Material connectActivatedPic;
        public Material disconnectPic;
        public Material disconnectActivatedPic;

        public GameObject tick;

        [Header("Haptics")]
        [Range(0, 1)]
        public float hapticFrequency;
        [Range(0, 1)]
        public float hapticAmplitude;
        public float hapticDelay;

        public float arrowSpeed;

        private bool _stillThere;
        private bool _isConnected;

        private void Awake() {

            // grab the material element to change the button picture
            _buttonPic = buttonPic.GetComponent<MeshRenderer>().material;

            // initialize flag
            _isConnected = PhotonNetwork.IsConnected;

            // set the tick
            tick.SetActive(_isConnected);

            // initialize button
            if (_isConnected) {

                _buttonPic = disconnectPic;
            }
            else {
                _buttonPic = connectPic;
            }
        }

        private void Update() {

            if (_stillThere) {
                buttonBody.transform.Rotate(Vector3.up * arrowSpeed * Time.deltaTime, Space.Self);
            }
        }

        private void OnTriggerEnter(Collider other) {

            // (1) is it the collider we're looking for? It's on the Quest PUN Template RHand in Resources.
            if ((other.tag == "bingo") &&

                // (2) is the right-hand middle-finger trigger held down?
                ((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 1)) &&

                // (3) finally, check the index finger trigger isn't held down...
                (!OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))) {

                // set the flag
                _stillThere = true;

                // which connection state are we in?
                _isConnected = PhotonNetwork.IsConnected;

                // this is the start of a long-hold press, which can be of two states
                // first, we show an activated pic
                if (_isConnected) {

                    Debug.Log("--------------------------------connected->activated");

                    _buttonPic = disconnectActivatedPic;
                }
                else {

                    _buttonPic = connectActivatedPic;
                }

                // reinforce button-press with haptics
                StartCoroutine("HapticFeedback");

                // initialize delay
                StartCoroutine("WaitABit");
            }
        }

        private void OnTriggerExit(Collider other) {
            // (1) is it the collider we're looking for? It's on the Quest PUN Template RHand in Resources.
            if ((other.tag == "bingo")) {// &&
                                         // (2) is the right-hand middle-finger trigger held down?
                                         //  ((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 1)) &&
                                         // (3) finally, check the index finger trigger isn't held down...
                                         //  (!OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))) {

                // set the flag
                _stillThere = false;

                // reset arrow
                ResetArrow();

                // stop the haptics
                OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
            }
        }

        void ResetArrow() {

            // reset the flag
            _stillThere = false;

            // reset the arrow rotation
            buttonBody.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        IEnumerator WaitABit() {

            yield return new WaitForSeconds(hapticDelay);

            if (_stillThere) {

                // all conditions satisfied

                // reset arrow
                ResetArrow();

                //DO SOMETHING
                networkScript.ConnectTrigger();
            }
        }

        IEnumerator HapticFeedback() {
            // set haptics on and off
            OVRInput.SetControllerVibration(hapticFrequency, hapticAmplitude, OVRInput.Controller.LTouch);
            yield return new WaitForSeconds(hapticDelay);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
    }
}
