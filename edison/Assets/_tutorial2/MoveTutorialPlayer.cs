/// <summary>
/// MoveTutorialPlayer.cs
/// query OVRInput to move, rotate
/// do special things because we're in the tutorial
/// NB based on MovePlayer.cs
/// </summary>

namespace com.jonrummery.edison {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MoveTutorialPlayer : MonoBehaviour {

        public GameObject playerPivot;
        private GameObject _cam;
        //public GameObject tracking;

        // reference the directional arrows
        public GameObject arrowForward, arrowLatLeft, arrowLatRight, arrowRotLeft, arrowRotRight;
        private MeshRenderer _arrowForwardMesh, _arrowLeftMesh, _arrowRightMesh, _arrowRotLeftMesh, _arrowRotRightMesh;

        // script that handles the arrows overlayed on controller models
        public WaypointManager waypointManager;

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

        //public bool inTutorial;

        //[HideInInspector]
        //public bool _isSliding;

        //[HideInInspector]
        //public bool isInControlsTutorial = true;

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

        //[HideInInspector]
        //public enum movementStyles {
        //    snap,
        //    smooth
        //}

        //[HideInInspector]
        //public movementStyles movementChoice;

        private bool _actionButton1, _actionButton2, _actionButton3, _actionButton4;

        private bool _isHoldingReset;

        [Header("'Please pick up both controllers' box")]
        public GameObject resetBanner;

        [Header("Reset locations")]
        public Transform restartTransform1;
        public Transform restartTransform2;
        public Transform restartTransform3;

        [Header("'Reset' box")]
        public GameObject resetBox;

        private bool _hasCompletedMovementTutorial;

        public TutorialClipboard nextScript;

        private int _currentWaypoint;

        private void Start() {

            // reference the headset position to strafe
            _cam = GameObject.FindGameObjectWithTag("MainCamera");
            Debug.Log("Camera found at " + _cam.name);

            // get a reference to the hand/controller used for forward direction
            // TODO expose string for left-handed users
            _rightHand = GameObject.FindGameObjectWithTag("RightHand");

            // set initial movement style
            //movementChoice = movementStyles.snap;

            // set the snap values
            _snapLeftSpeed = -rotationSpeed;
            _snapRightSpeed = rotationSpeed;

            // reference the arrow material so that we can change color
            _arrowForwardMesh = arrowForward.GetComponent<MeshRenderer>();
            _arrowLeftMesh = arrowLatLeft.GetComponent<MeshRenderer>();
            _arrowRightMesh = arrowLatRight.GetComponent<MeshRenderer>();
            _arrowRotLeftMesh = arrowRotLeft.GetComponent<MeshRenderer>();
            _arrowRotRightMesh = arrowRotRight.GetComponent<MeshRenderer>();
        }

        void Update() {

            if (!_hasCompletedMovementTutorial) {

                _currentWaypoint = MySharedData.nextWaypoint;

                GetOVRInput();
                ProcessOVRInput();
            }

            // only want to do this once
            else if (this.enabled == true) {

                // switch from controller models to hands for the second part of tutorial (clipboard)

                // stop showing stuff
                resetBox.SetActive(false);
                arrowForward.SetActive(false);
                arrowLatLeft.SetActive(false);
                arrowLatRight.SetActive(false);
                arrowRotLeft.SetActive(false);
                arrowRotRight.SetActive(false);

                // start the clipboard script
                nextScript.DisplayTutorialClipboard();

                // stop this movement script (and stop Update from redundantly repeating)
                this.enabled = false;
            }
        }

        void GetOVRInput() {

            // query the thumbsticks (Vector2 ((-1->1),(-1->1)) for (x,y))
            _leftStickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            _rightStickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

            // ...and the two index-finger triggers for boost and superman modes (float)
            _supermanTriggerInput = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
            _boostTriggerInput = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

            // reset button
            _actionButton3 = (OVRInput.Get(OVRInput.Button.Three));
        }

        IEnumerator HoldResetForThreeSeconds() {

            resetBanner.SetActive(true);

            yield return new WaitForSeconds(3);

            if (_isHoldingReset) {

                if (_currentWaypoint==0) {

                    playerPivot.transform.position = restartTransform1.position;
                    playerPivot.transform.rotation = restartTransform1.rotation;
                }
                else if (_currentWaypoint==1) {

                    playerPivot.transform.position = restartTransform2.position;
                    playerPivot.transform.rotation = restartTransform2.rotation;
                }
                else {

                    playerPivot.transform.position = restartTransform3.position;
                    playerPivot.transform.rotation = restartTransform3.rotation;
                }

                resetBanner.SetActive(false);
            }
        }

        void ProcessOVRInput() {

            // reset position if actionbutton3 held down for 3 seconds
            if (!_actionButton3 && _isHoldingReset) {

                _isHoldingReset = false;

                resetBanner.SetActive(false);
            }
            else {

                if (_actionButton3 && !_isHoldingReset) {

                    _isHoldingReset = true;
                    StartCoroutine("HoldResetForThreeSeconds");
                }
                else {

                    if (!_actionButton3) {

                        _isHoldingReset = false;
                        StopCoroutine("HoldResetForThreeSeconds");
                    }
                }

                if (_currentWaypoint >= 3) {

                    _hasCompletedMovementTutorial = true;
                }

                // TUTORIAL #2
                if (_currentWaypoint >= 2) {

                    // SNAP
                    if (_leftStickInput.x == 0f) {

                        _isSnapping = false;
                    }

                    if (!_isSnapping && ((_leftStickInput.x != 0) && (_leftStickInput.y < stickTolerance))) {

                        _isSnapping = true;

                        // moving the stick to the left requires a -ve y rotation value
                        if (_leftStickInput.x < 0) {

                            _rotation = _snapLeftSpeed;

                            _arrowRotLeftMesh.material.color = Color.red;
                        }
                        else {

                            _rotation = _snapRightSpeed;

                            _arrowRotRightMesh.material.color = Color.red;
                        }

                        playerPivot.transform.Rotate(0f, _rotation, 0f, Space.Self);
                    }

                    else {

                        _arrowRotLeftMesh.material.color = Color.white;
                        _arrowRotRightMesh.material.color = Color.white;
                    }
                }

                // TUTORIAL #1
                if (_currentWaypoint >= 1) {

                    // STRAFE
                    // right stick x-axis for lateral (side-to-side) movement
                    if (_rightStickInput.x != 0 && ((_rightStickInput.y >= -stickTolerance) && (_rightStickInput.y <= stickTolerance))) {

                        // change arrow color on controller model
                        if (_rightStickInput.x < 0) {

                            _arrowLeftMesh.material.color = Color.red;
                            _arrowRightMesh.material.color = Color.white;
                        }
                        else {

                            _arrowRightMesh.material.color = Color.red;
                            _arrowLeftMesh.material.color = Color.white;
                        }

                        // laterally move relative to headset position
                        _movement = (_cam.transform.rotation * Vector3.right * moveSpeed * _rightStickInput.x * Time.deltaTime);
                        playerPivot.transform.position += _movement;
                    }

                    else {

                        _arrowLeftMesh.material.color = Color.white;
                        _arrowRightMesh.material.color = Color.white;
                    }
                }

                // TUTORIAL #0
                if (_currentWaypoint >= 0) {

                    // FORWARD/BACKWARD
                    // right stick y-axis for forward/backward movement
                    if ((_rightStickInput.y > 0f) && ((_rightStickInput.x >= -stickTolerance) && (_rightStickInput.x <= stickTolerance))) {

                        // change arrow color on controller model
                        _arrowForwardMesh.material.color = Color.red;

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

                    else {

                        _arrowForwardMesh.material.color = Color.white;
                    }
                }
            }
        }
    }
}
