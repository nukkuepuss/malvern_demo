/// <summary>
/// move/reset the sun
/// Axis2D.PrimaryThumbstick + Button.One = move sun
/// Axis2D.Button.PrimaryThumbstick + Button.One = reset sun
/// NB remember to check for Button.One in movement script
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class DayNightCycle : MonoBehaviour {

        // grab the sun
        [Header("what are we moving?")]
        public GameObject sun;

        [Header("slow down factor")]
        public float step;

        // original transform
        private Vector3 _originalSunPosition;
        private Quaternion _originalSunRotation;

        private float _stickX, _stickY;

        private bool _actionButton;

        private void Start() {

            // store original sun transform values
            _originalSunPosition = sun.transform.position;
            _originalSunRotation = sun.transform.rotation;
        }

        private void Update() {

            _actionButton = OVRInput.Get(OVRInput.Button.One);

            // these features are accessed via Button.One ('A')
            if (_actionButton) {

                // has the left stick been moved?
                _stickX = (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x);
                _stickY = (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y);

                if (_stickX != 0 || _stickY != 0) {

                    // calculate the jump
                    float _moveX = _stickX / step;
                    float _moveY = _stickY / step;

                    // add the 2D Vector2 axis from the left stick (x and y) to the sun transform (x, y, z)
                    // note that the x-value of the sun Vector3 is transformed by the y-value of the thumbstick
                    // So; sun.transform.localEulerAngles += new Vector3((OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y / step), (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x / step), 0f);
                    sun.transform.localEulerAngles += new Vector3(_moveY, _moveX, 0f);

                    MySharedData.hasSunMoved = true;
                }

                // has the left stick been clicked with the action-button held down?
                if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick) && _actionButton) {

                    // reset the sun
                    sun.transform.position = _originalSunPosition;
                    sun.transform.rotation = _originalSunRotation;

                    MySharedData.hasSunMoved = true;
                }
            }
            else {

                MySharedData.hasSunMoved = false;
            }
        }
    }
}
