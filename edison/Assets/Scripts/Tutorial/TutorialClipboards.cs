/// <summary>
/// TutorialClipboards.cs
/// highlight buttons on controller models and show a pop-up description
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class TutorialClipboards : MonoBehaviour {

        public GameObject clipboardTemplate;
        public Texture2D clip1Pic;
        //public int materialToFlash;
        public float flashDelay;

        [Header("Material mappings")]
        public int leftStickMat;
        public int leftIndexMat;
        public int leftMiddleMat;
        public int rightStickMat;
        public int rightIndexMat;
        public int rightMiddleMat;
        public int buttonOneMat;
        public int buttonTwoMat;
        public int buttonThreeMat;
        public int buttonFourMat;

        private Material[] _mats;
        private Color _originalMatsColor;
        //private bool _isFlashing = false;
        //private bool _isButtonOneFlashing = false;

        void Start() {

            // grab the materials for the clipboard with the controller models
            _mats = clipboardTemplate.gameObject.GetComponent<MeshRenderer>().materials;
        }

        private void Update() {
            
            // button one
            if (OVRInput.GetDown(OVRInput.Button.One)) {

                _mats[buttonOneMat].color = Color.red;
            }
            else if (OVRInput.GetUp(OVRInput.Button.One)) {

                _mats[buttonOneMat].color = Color.white;
            }

            // button two
            if (OVRInput.GetDown(OVRInput.Button.Two)) {

                _mats[buttonTwoMat].color = Color.red;
            }
            else if (OVRInput.GetUp(OVRInput.Button.Two)) {

                _mats[buttonTwoMat].color = Color.white;
            }

            // button three
            if (OVRInput.GetDown(OVRInput.Button.Three)) {

                _mats[buttonThreeMat].color = Color.red;
            }
            else if (OVRInput.GetUp(OVRInput.Button.Three)) {

                _mats[buttonThreeMat].color = Color.white;
            }

            // button four
            if (OVRInput.GetDown(OVRInput.Button.Four)) {

                _mats[buttonFourMat].color = Color.red;
            }
            else if (OVRInput.GetUp(OVRInput.Button.Four)) {

                _mats[buttonFourMat].color = Color.white;
            }

            // left stick
            if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y!=0) {

                _mats[leftStickMat].color = Color.red;
            }
            else {

                _mats[leftStickMat].color = Color.white;
            }

            // left index trigger
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger)!=0) {

                _mats[leftIndexMat].color = Color.red;
            }
            else {

                _mats[leftIndexMat].color = Color.white;
            }

            // left middle trigger
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) != 0) {

                _mats[leftMiddleMat].color = Color.red;
            }
            else {

                _mats[leftMiddleMat].color = Color.white;
            }

            // right stick
            if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y != 0) {

                _mats[rightStickMat].color = Color.red;
            }
            else {

                _mats[rightStickMat].color = Color.white;
            }

            // right index trigger
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) != 0) {

                _mats[rightIndexMat].color = Color.red;
            }
            else {

                _mats[rightIndexMat].color = Color.white;
            }

            // right middle trigger
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) != 0) {

                _mats[rightMiddleMat].color = Color.red;
            }
            else {

                _mats[rightMiddleMat].color = Color.white;
            }
        }

        //IEnumerator StartFlashing(int _matSlot) {

        //    // toggle the flag
        //    _isFlashing = true;

        //    _originalMatsColor = _mats[_matSlot].color;

        //    // loop the flashing
        //    while (_isFlashing) {

        //        // change material to a new color
        //        _mats[_matSlot].color = Color.red;

        //        // wait a bit
        //        yield return new WaitForSeconds(flashDelay);

        //        // revert to original color
        //        _mats[_matSlot].color = _originalMatsColor;

        //        // wait a bit
        //        yield return new WaitForSeconds(flashDelay);
        //    }
        //}

        //void StopFlashing(int _matSlot) {

        //    // toggle the flag
        //    _isFlashing = false;

        //    // halt the flashing coroutine
        //    StopCoroutine("StartFlashing");

        //    // revert to original color
        //    _mats[_matSlot].color = _originalMatsColor;
        //}
    }
}
