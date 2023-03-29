/// <summary>
/// connect/disconnect button on networking clipboard
/// player's finger must stay in trigger until arrow has completed full circle
/// *** picture (material) must be element 0 in mesh renderer (ideally, the only element)
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

namespace com.jonrummery.edison {

    public class NetworkingDisconnectButton : MonoBehaviourPunCallbacks {

        public Camera cam;

        [Header("Button inner and outer models")]
        public GameObject buttonPic;
        public GameObject buttonBody;

        [Header("Materials for buttons")]
        public Material connectPic;
        public Material connectActivatedPic;
        public Material disconnectPic;
        public Material disconnectActivatedPic;

        [Header("Green tick (transparent planes")]
        public GameObject tick;

        public float arrowSpeed;

        public float hapticDelay;
        private float hapticFrequency = 0.3f, hapticAmplitude = 0.3f;

        private bool stillThere = false;

        private Material _buttonPic;

        private bool _connected;

        private void Start() {

            _connected = PhotonNetwork.IsConnected;
        }

        private void Update() {

            //if (_connected) {

            //    // go from connected state to disconnected
            //    b0IsOn = false;
            //    DeactivateB0();
            //    PhotonNetwork.Disconnect();
            //}
            //else {

            //    //go from disconnected to connected
            //    if (!PhotonNetwork.IsConnected && !triesToConnectToMaster) {
            //        ConnectToMaster();
            //    }
            //    if (PhotonNetwork.IsConnected && !triesToConnectToMaster && !triesToConnectToRoom) {
            //        StartCoroutine(WaitFrameAndConnect());
            //    }
            //}

            //_connected = PhotonNetwork.IsConnected;

            //if (!_connected) {

            //    tick.SetActive(false);
            //    _buttonPic = connectPic;
            //}
            //else if (!stillThere) {

            //    tick.SetActive(true);
            //    _buttonPic = disconnectPic;
            //}

            // rotate the button if player's finger is still in collider
            if (stillThere) {
                buttonBody.transform.Rotate(Vector3.up * arrowSpeed * Time.deltaTime, Space.Self);
            }
        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log("NetworkingDisconnectButton-OnTriggerEnter called by " + this.gameObject.name + " due to " + other.tag);

            // (1) is it the collider we're looking for? It's on the Quest PUN Template RHand in Resources.
            if ((other.tag == "bingo") &&

                // (2) is the right-hand middle-finger trigger held down?
                ((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 1)) &&

                // (3) finally, check the index finger trigger isn't held down...
                (!OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))) {

                // set the flag
                stillThere = true;

                //set the button 'on' state
                //buttonPic.GetComponent<MeshRenderer>().material = onPic;

                // reinforce button-press with haptics
                StartCoroutine("Haptics");

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
                stillThere = false;

                // reset arrow
                ResetArrow();

                //// set the button 'on' state
                //buttonPic.GetComponent<MeshRenderer>().material = onPic;

                // stop the haptics
                OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
            }
        }

        IEnumerator WaitABit() {

            yield return new WaitForSeconds(hapticDelay);

            if (stillThere) {

                // all conditions satisfied

                // reset arrow
                ResetArrow();

                // hide all objects (Unity makes the Oculus display all janky when changing scenes)
                //cam.cullingMask = (1 << LayerMask.NameToLayer("Nothing"));

                //load main scene (see Build Settings)
                //SceneManager.LoadScene(nextLevel);

                //DO SOMETHING
            }
        }

        void ResetArrow() {

            // reset the flag
            stillThere = false;

            // set the button 'off' state
            //buttonPic.GetComponent<MeshRenderer>().material = offPic;

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