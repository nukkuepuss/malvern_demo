/// <summary>
/// MovePlayer.cs
/// query OVRInput to move, rotate
/// TODO : map oculus controller, properly exposed
/// //TODO : turn on the spot (experimental offset code) <== seems to have resolved for Quest2, not Rift... different trackers..?
/// TODO expose variables on Settings clipboard
/// </summary>

namespace com.jonrummery.edison {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MovePlayer : MonoBehaviour {

        private GameObject _cam;
        //public GameObject tracking;

        [Tooltip("Lower the value for movement along stick y-axis to register when close to x-axis")]
        [Range(0, 1)]
        public float stickTolerance;

        [Header("Forward / backward speed (right-stick up and down)")]
        public float moveYSpeed;

        [Header("Strafe speed (right-stick left and right)")]
        public float moveXSpeed;

        [Header("Rotation speed (left-stick left and right")]
        public float rotationSpeed;

        [Header("Index trigger boosts")]
        [Range(0, 100)]
        public float boostSpeed;
        [Range(0, 100)]
        public float supermanSpeed;
        //public float godSpeed;

        public bool inTutorial;

        //[HideInInspector]
        //public bool isInControlsTutorial = false;

        //private float _newMoveSpeed;
        private float _newYMoveSpeed;
        //private float _newXMoveSpeed;

        private GameObject _rightHand;

        [HideInInspector]
        public Vector2 _leftStickInput;
        [HideInInspector]
        public Vector2 _rightStickInput;

        private float _rotation;

        private Vector3 _movement;

        [HideInInspector]
        public float _supermanTriggerInput;
        [HideInInspector]
        public float _boostTriggerInput;

        private bool _actionButton1, _actionButton2;

        private void Start() {

            // reference the headset position to strafe
            _cam = GameObject.FindGameObjectWithTag("MainCamera");
            Debug.Log("Camera found at " + _cam.name);

            // get a reference to the hand/controller used for forward direction
            // TODO expose string for left-handed users
            _rightHand = GameObject.FindGameObjectWithTag("RightHand");

            // need to offset camera for rotation to be 'on-the-spot', so get a reference to the MainCamera (CentreEyeAnchor)
            // _offset = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }

        void Update() {

            // check if tutorial has been completed : no moving beforehand
            if (!inTutorial) {

                GetOVRInput();

                // don't do anything if either action button is pressed
                if (!_actionButton1 || !_actionButton2) {

                    ProcessOVRInput();
                }
            }

            // experimental offset for turning on-the-spot
            // transform.position = _offset.transform.position;

            // call refresh function from Oculus **required**
            // OVRInput.Update();
        }

        //public void TutorialFlag() {

        //    // called from TutorialManager when the tutorial has completed
        //    isInTutorial = false;
        //}

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

            // ROTATION
            // left stick x-axis for rotation, but only if the left-stick is moved tolerably AND special feature buttons 1 and 2 aren't held down
            if (((_leftStickInput.x != 0) && (_leftStickInput.y < stickTolerance)) && !_actionButton1) {

                // smooth out the rotation
                _rotation = (rotationSpeed * _leftStickInput.x * Time.deltaTime);

                // rotate around the y-axis of player
                transform.Rotate(0f, _rotation, 0f);
            }

            // STRAFE
            // right stick x-axis for lateral (side-to-side) movement
            if (_rightStickInput.x != 0 && _rightStickInput.y < stickTolerance) {

                // laterally move relative to headset position
                _movement = (_cam.transform.rotation * Vector3.right * moveXSpeed * _rightStickInput.x * Time.deltaTime);
                transform.position += _movement;
            }

            // FORWARD/BACKWARD
            // right stick y-axis for forward/backward movement
            if ((_rightStickInput.y != 0f) && (_rightStickInput.x < stickTolerance)) {

                // Yeah! we're gonna do some moving!
                _newYMoveSpeed = moveYSpeed;

                // is Boost Mode enabled?
                if (_boostTriggerInput > 0.1f) {
                    _newYMoveSpeed = (boostSpeed * _boostTriggerInput);
                }

                // is Superman Mode enabled?
                if (_supermanTriggerInput > 0.1f) {
                    _newYMoveSpeed = (supermanSpeed * _supermanTriggerInput);
                }

                // is God Mode enabled?
                if (_boostTriggerInput != 0f && _supermanTriggerInput != 0f) {
                    _newYMoveSpeed = (supermanSpeed * boostSpeed * _boostTriggerInput);
                }

                // move forward/backward relative to right hand's position
                _movement = (_rightHand.transform.forward * _newYMoveSpeed * _rightStickInput.y * Time.deltaTime);
                transform.position += _movement;
            }
        }
    }
}
