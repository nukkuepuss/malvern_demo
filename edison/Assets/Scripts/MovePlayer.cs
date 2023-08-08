/// <summary>
/// MovePlayer.cs
/// query OVRInput to move, rotate
/// TODO : map oculus controller, properly exposed
/// TODO expose variables on Settings clipboard
/// </summary>

namespace com.jonrummery.edison {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MovePlayer : MonoBehaviour {

        public GameObject playerPivot;
        private GameObject _cam;
        //public GameObject tracking;

        [Tooltip("Lower the value for movement along stick y-axis to register when close to x-axis")]
        [Range(0, 1)]
        public float stickTolerance;

        [Header("Speed (right-stick up/down + left/right)")]
        public int moveSpeed;

        //[Header("Strafe speed (right-stick left/right)")]
        //public int moveXSpeed;

        [Header("Rotation speed (left-stick left/right")]
        public int rotationSpeed;

        [Header("Index trigger boost")]
        [Range(0, 10)]
        public int boostSpeed;

        public bool inTutorial;

        [HideInInspector]
        public bool _isSliding;

        //[HideInInspector]
        public bool isInControlsTutorial = true;

        private float _newYMoveSpeed;

        private GameObject _rightHand;

        [HideInInspector]
        public Vector2 _leftStickInput;
        [HideInInspector]
        public Vector2 _rightStickInput;

        private float _rotation;
        private bool _isSnapping;
        private float _snapLeftSpeed;
        private float _snapRightSpeed;

        private Vector3 _movement;

        [HideInInspector]
        public float _supermanTriggerInput;
        [HideInInspector]
        public float _boostTriggerInput;

        [HideInInspector]
        public enum movementStyles {
            snap,
            smooth
        }

        [HideInInspector]
        public movementStyles movementChoice;

        private bool _actionButton1, _actionButton2;

        private void Start() {

            // reference the headset position to strafe
            _cam = GameObject.FindGameObjectWithTag("MainCamera");
            Debug.Log("Camera found at " + _cam.name);

            // get a reference to the hand/controller used for forward direction
            // TODO expose string for left-handed users
            _rightHand = GameObject.FindGameObjectWithTag("RightHand");

            // set initial movement style
            movementChoice = movementStyles.snap;

            // set the snap values
            _snapLeftSpeed = -rotationSpeed;
            _snapRightSpeed = rotationSpeed;

        }

        void Update() {

            // check if tutorial has been completed (no moving beforehand) and that a slider is being slid
            if (!inTutorial && !_isSliding) {

                GetOVRInput();

                // don't do anything if either action button is pressed
                if (!_actionButton1 && !_actionButton2) {

                    ProcessOVRInput();
                }
            }
        }

        void GetOVRInput() {

            // query the thumbsticks (Vector2 ((-1->1),(-1->1)) for (x,y))
            _leftStickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            _rightStickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

            // ...and the two index-finger triggers for boost and superman modes (float)
            _supermanTriggerInput = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
            _boostTriggerInput = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

            // and we don't want to move if special features are activated (button.one)
            _actionButton1 = (OVRInput.Get(OVRInput.Button.One));
            _actionButton2 = (OVRInput.Get(OVRInput.Button.Two));
        }

        void ProcessOVRInput() {

            // ROTATION - two types
            if (movementChoice == movementStyles.smooth) {

                // SMOOTH
                // left stick x-axis for rotation, but only if the left-stick is moved tolerably AND special feature buttons 1 and 2 aren't held down
                // smooth out the rotation
                _rotation = (rotationSpeed * _leftStickInput.x * Time.deltaTime);

                // rotate around the y-axis of player
                playerPivot.transform.Rotate(0f, _rotation, 0f, Space.Self);
            }
            else {

                // SNAP
                if (_leftStickInput.x == 0f) {

                    _isSnapping = false;
                }

                if (!_isSnapping && (((_leftStickInput.x != 0) && (_leftStickInput.y < stickTolerance)) && !_actionButton1)) {

                    _isSnapping = true;

                    // moving the stick to the left requires a -ve y rotation value
                    if (_leftStickInput.x < 0) {

                        _rotation = _snapLeftSpeed;
                    }
                    else {
                        _rotation = _snapRightSpeed;
                    }

                    playerPivot.transform.Rotate(0f, _rotation, 0f, Space.Self);
                }
            }

            // STRAFE
            // right stick x-axis for lateral (side-to-side) movement
            if (_rightStickInput.x != 0 && _rightStickInput.y < stickTolerance) {

                // laterally move relative to headset position
                _movement = (_cam.transform.rotation * Vector3.right * moveSpeed * _rightStickInput.x * Time.deltaTime);
                playerPivot.transform.position += _movement;
            }

            // FORWARD/BACKWARD
            // right stick y-axis for forward/backward movement
            if ((_rightStickInput.y != 0f) && (_rightStickInput.x < stickTolerance)) {

                // Yeah! we're gonna do some moving!
                _newYMoveSpeed = moveSpeed;

                // is Boost Mode enabled?
                if (_boostTriggerInput != 0f) {
                    _newYMoveSpeed += (boostSpeed * _boostTriggerInput);
                }

                // is Superman Mode enabled?
                // NB this value has been deprecated : boost and superman speeds are the same
                if (_supermanTriggerInput != 0f) {
                    _newYMoveSpeed += (boostSpeed * _supermanTriggerInput);
                }

                // is God Mode enabled?
                if (_boostTriggerInput != 0f && _supermanTriggerInput != 0f) {
                    _newYMoveSpeed += (boostSpeed * boostSpeed * _boostTriggerInput);
                }

                // move forward/backward relative to right hand's position
                _movement = (_rightHand.transform.forward * _newYMoveSpeed * _rightStickInput.y * Time.deltaTime);
                playerPivot.transform.position += _movement;
            }
        }
    }
}
