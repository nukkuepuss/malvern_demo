/// <summary>
/// MoveSlider.cs
/// move a slider, update a readout and send value to movement script
/// *** must have a parent
/// *** place sliders at middle point on scale (5)
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace com.jonrummery.edison {

    public class MoveSlider : MonoBehaviour {

        public enum speedSettings { move, boost }
        public speedSettings valueToChange;
        //public GameObject clipboard;
        public TextMeshProUGUI moveSpeedDisplay, boostSpeedDisplay, valueHand, valueSlider;
        public float minX, maxX;
        public Material onMat, offMat;
        public MovePlayer movementScript;

        [Header("Haptics")]
        [Range(0, 1)]
        public float hapticFrequency;
        [Range(0, 1)]
        public float hapticAmplitude;

        public GameObject hand;
        private float _handXMovement;
        private float _handLastKnownXPos;
        private float _handCurrentXPos;
        private float _sliderCurrentXPos;
        private bool _isTriggering;
        private MeshRenderer _sliderMeshRenderer;
        private GameObject _parent;


        private void Update() {

            if (_isTriggering) {

                // NB OnTriggerEnter initially sets _handLastKnownXPos

                if ((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 1) && (!OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))) {

                    // stop player movement (it buggers up the transform.Translate)
                    movementScript._isSliding = true;

                    // reinforce button-press with haptics
                    StartCoroutine("HapticFeedback");

                    // change the slider color
                    _sliderMeshRenderer.material = onMat;

                    // get the current hand position.x
                    // NB localPosition won't work
                    _handCurrentXPos = hand.transform.position.x;

                    // parent
                   // _parent = this.transform.parent.gameObject;
                    //this.transform.parent = hand.transform;

                    // get the current slider position.x
                    _sliderCurrentXPos = transform.localPosition.x;

                    // calculate how far the hand has moved
                    _handXMovement = (_handLastKnownXPos - _handCurrentXPos);

                    // calculate where the slider would be if it was moved in sync with the hand
                    float _potentialSliderXPos = _sliderCurrentXPos + _handXMovement;

                    // check movement is within bounds
                    if (IsBetween(_potentialSliderXPos, minX, maxX)) {

                        // move the slider
                        transform.localPosition += new Vector3(_handXMovement, 0f, 0f);
                        //transform.Translate(_handXMovement, 0f, 0f);

                        UpdateStuff();
                    }

                    // update the last known position of the hand
                    _handLastKnownXPos = _handCurrentXPos;
                }
                else {

                    // change the slider color
                    _sliderMeshRenderer.material = offMat;
                }

                // de-parent
                //this.transform.parent = _parent.transform;
            }
        }

        private void OnTriggerEnter(Collider other) {

            if (other.tag == "bingo") {

                // set the flag
                _isTriggering = true;

                // store the initial position of the hand
                // NB localPosition won't work
                _handLastKnownXPos = hand.transform.position.x;
            }
        }

        void OnTriggerExit(Collider other) {

            movementScript._isSliding = false;

            // change the slider color
            _sliderMeshRenderer.material = offMat;

            // set the flag
            _isTriggering = false;
        }

        void UpdateStuff() {

            valueHand.text = RoundedNumber(_handXMovement);
            valueSlider.text = RoundedNumber(_handCurrentXPos);

            // calculate a normalized value between 1 and 10
            int _normalizedSliderValue = NormalizeValue(_sliderCurrentXPos);

            switch (valueToChange) {

                case speedSettings.move:

                    // update the movement script with new value
                    movementScript.moveSpeed = _normalizedSliderValue;

                    // update the display
                    moveSpeedDisplay.text = _normalizedSliderValue.ToString();
                    break;

                case speedSettings.boost:

                    // update the movement script with new value
                    movementScript.boostSpeed = _normalizedSliderValue;

                    // update the display
                    boostSpeedDisplay.text = _normalizedSliderValue.ToString();
                    break;

                default:
                    break;
            }
        }

        bool IsBetween(float value, float x, float y) {

            // true if value is between x and y
            return (value >= Mathf.Min(x, y) && value <= Mathf.Max(x, y));
        }

        int NormalizeValue(float pos) {

            // get a value between 1 and 10 from any number in a range
            return (int)(Mathf.Round((Mathf.InverseLerp(minX, maxX, pos)) * 9) + 1);
        }

        string RoundedNumber(float value) {

            // get a number rounded to two decimal places as a string
            // NB not rounded anymore!
            return value.ToString();
            //return Mathf.Round((value * 10000f) * 0.0001f).ToString();
        }

        IEnumerator HapticFeedback() {

            // set haptics on and off
            OVRInput.SetControllerVibration(hapticFrequency, hapticAmplitude, OVRInput.Controller.RTouch);
            yield return new WaitForSeconds(0.1f);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
    }
}
