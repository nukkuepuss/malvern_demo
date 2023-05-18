/// <summary>
/// PhotoShphere.cs
/// show a 360 photosphere with Button3 when in a collider
/// metadata server for bracer information text
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.jonrummery.edison {

    public class PhotoSphere : MonoBehaviour {

        public Material photoSphere;
        public Camera cam;
        public GameObject player;
        public GameObject bracer;

        [Tooltip("what's the material element of the bracer's 360 photo light indicator?")]
        public int photoElement;

        private bool _isInPhotosphereCollider = false;
        private bool _isShowingPhotosphere = false;
        private float _originalMoveSpeed;
        private Material _originalSkybox;
        private int _originalMask;

        // metadata
        public Text display;
        public string metaData;

        // 360 indicator
        private Material[] mats;

        private void Start() {

            // grab the bracer's materials to change the photoshpere indicator light
            mats = bracer.GetComponent<MeshRenderer>().materials;

            // we don't start in a photoshpere, so set indicator light off
            mats[photoElement].color = Color.black;

            // grab some values for changing the skybox back after showing photoshere
            _originalMask = cam.cullingMask;
            _originalSkybox = RenderSettings.skybox;

            // player is frozen in photomode and should continue moving on exit
            _originalMoveSpeed = player.GetComponent<MovePlayer>().moveYSpeed;
        }

        private void Update() {

            // is player in trigger box, not already showing a photo and holding down the correct button
            if (_isInPhotosphereCollider && !_isShowingPhotosphere && (OVRInput.Get(OVRInput.Button.Three))) {

                // set the flag
                _isShowingPhotosphere = true;

                // freeze player
                player.GetComponent<MovePlayer>().moveYSpeed = 0f;

                // show the photo
                ShowPhotosphere();
            }

            // has the player released the button to exit photosphere
            if (_isShowingPhotosphere && (OVRInput.GetUp(OVRInput.Button.Three))) {

                // stop showing photo
                StopShowingPhotosphere();

                // unfreeze player
                player.GetComponent<MovePlayer>().moveYSpeed = _originalMoveSpeed;
            }
        }

        private void OnTriggerEnter(Collider other) {

            if (other.tag=="Player" || other.tag=="bingo") {

                // set the flag
                _isInPhotosphereCollider = true;

                // change indicator light
                mats[photoElement].color = Color.red;

                // metadata
                display.text = metaData;
            }
        }

        private void OnTriggerExit(Collider other) {

            // set the flag
            _isInPhotosphereCollider = false;

            // chnage the indicator light
            mats[photoElement].color = Color.black;

            // remove metadata
            display.text = "";

            // stop showing photo
            StopShowingPhotosphere();
        }

        void ShowPhotosphere() {

            // set the flag
            _isShowingPhotosphere = true;

            // hide all objects
            cam.cullingMask = (1 << LayerMask.NameToLayer("Nothing"));

            // change skybox to show photosphere
            RenderSettings.skybox = photoSphere;
        }

        void StopShowingPhotosphere() {

            // set the flag
            _isShowingPhotosphere = false;

            // change skybox back to original
            RenderSettings.skybox = _originalSkybox;

            // restore original mask
            cam.cullingMask = _originalMask;
        }
    }
}