/// <summary>
/// tutorial.
/// press buttons + light burners before wristband is available
/// the buttons and burner call functions in this script
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class TutorialManager : MonoBehaviour {

        // we need this to freeze Player during tutorial
        public MovePlayer moveScript;

        [Tooltip("Lower the value for movement along stick y-axis to register when close to x-axis")]
        [Range(0, 1)]
        public float stickTolerance;

        // our clipboards
        public GameObject clip1, clip2, clip3;

        // progress arrows
        public GameObject clip1GreenArrow, clip2GreenArrow, clip3GreenArrow;

        // Player wristband
        public GameObject wristband;

        // clipboard ticks
        public GameObject tick1, tick2, tick3, tick4, tick5, tick6, tick7, tick8, tick9, tick10;

        public Texture2D rectangularButtonOn, rectangularButtonOff;

        public GameObject rectangularButton;

        private bool _isTouching;

        [HideInInspector]
        public Vector2 _rightStickInput;

        [HideInInspector]
        public bool _hasBurnt = false;

        // internal flag to keep track of which clipboard is showing (1,2,3)
        private int _clip = 0;

        private Material[] _mats;

        private bool _hasMoved, _hasStrafed, _hasRotated, _hasTriggered;

        void Start() {

            if (MySharedData.hasCompletedTutorial) {

                return;
            }

            //set the user interface (wristband and clipboards)
            wristband.SetActive(false);

            _clip = 1;
            clip1.SetActive(true);
            clip1GreenArrow.SetActive(false);

            _mats = rectangularButton.GetComponent<MeshRenderer>().materials;

            _mats[0].SetTexture("_MainTex", rectangularButtonOff);


            clip2.SetActive(false);
            clip2GreenArrow.SetActive(false);

            clip3.SetActive(false);
            clip3GreenArrow.SetActive(false);

            //moveScript.inTutorial = true;
        }

        private void Update() {

            switch (_clip) {

                case 1:

                    // "Welcome to Malvern, UK" tutorial clip 1
                    // has two ticks, one rectangular button and a long-press button
                    //
                    // first tick is for holding down the right-hand-middle trigger
                    // **** there's a delay here with the tick appearing **** TODO
                    tick1.SetActive((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger)) == 1);

                    // place second tick if we're not touching the right-hand-index trigger (ie. success -> we're pointing)
                    _isTouching = (OVRInput.Get(OVRInput.NearTouch.SecondaryIndexTrigger));
                    tick2.SetActive(!_isTouching);

                    //Debug.Log(tick1.activeSelf.ToString() + ", " + tick2.activeSelf.ToString() + ", " + tick3.activeSelf.ToString() + ", " + tick4.activeSelf.ToString());

                    if (tick1.activeSelf && tick2.activeSelf && tick3.activeSelf && tick4.activeSelf) {

                        clip1GreenArrow.SetActive(true);
                    }
                    else {

                        clip1GreenArrow.SetActive(false);
                    }

                    break;

                case 2:

                    clip1GreenArrow.SetActive(false);

                    // "You begin in the wicker basket of a hot air balloon" tutorial clip 2
                    // one tick for holding right-hand into the burner collider and another for pushing the right-stick
                    //
                    // check for the right thumbstick being moved
                    _rightStickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

                    // set the ticks
                    if (_hasBurnt) {

                        tick5.SetActive(true);
                    }

                    if (_rightStickInput.y != 0 && _rightStickInput.x < stickTolerance) {

                        tick6.SetActive(true);
                    }

                    // show the green arrow of progress
                    if (tick5.activeSelf && tick6.activeSelf) {

                        clip2GreenArrow.SetActive(true);
                    }

                    break;

                case 3:

                    // "Controls" tutorial clip 3
                    // Player must have lighted all 4 ticks for movement to progress
                    //
                    // Forward/backward movement
                    if (moveScript._rightStickInput.y!=0 && (Mathf.Abs(moveScript._rightStickInput.x))<stickTolerance) {

                        tick7.SetActive(true);
                        _hasMoved = true;
                    }
                    else {

                        tick7.SetActive(false);
                    }

                    // index triggers for boosting
                    if (moveScript._supermanTriggerInput>0.1f || moveScript._boostTriggerInput>0.1f) {

                        tick8.SetActive(true);
                        _hasTriggered = true;
                    }
                    else {

                        tick8.SetActive(false);
                    }

                    // strafe with right-stick x-axis
                    if (moveScript._rightStickInput.x!=0 && (Mathf.Abs(moveScript._rightStickInput.y)<stickTolerance)) {

                        tick9.SetActive(true);
                        _hasStrafed = true;
                    }
                    else {

                        tick9.SetActive(false);
                    }

                    // rotation with left-stick x-axis
                    if (moveScript._leftStickInput.x!=0 && (Mathf.Abs(moveScript._leftStickInput.y)<stickTolerance)) {

                        tick10.SetActive(true);
                    }
                    else {

                        tick10.SetActive(false);
                        _hasRotated = true;
                    }

                    if (_hasMoved && _hasRotated && _hasStrafed && _hasTriggered) {

                        clip3GreenArrow.SetActive(true);
                    }

                    break;
            }
        }

        // called from the green progress arrow trigger unlocked when both buttons are pressed on Tutorial clipboard 1
        public void Clip1GreenArrowTrigger() {

            // change clips and arrows
            clip1.SetActive(false);
            clip1GreenArrow.SetActive(false);
            clip2.SetActive(true);

            // update flag
            _clip = 2;

            //// stop haptics
            //OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);

            // unfreeze Player
            //moveScript.inTutorial = false;
        }

        // called from the green progress arrow trigger unlocked when Player has successfully performed a burn
        public void Clip2GreenArrowTrigger() {

            // change clips and arrows
            clip2.SetActive(false);
            clip2GreenArrow.SetActive(false);
            clip3.SetActive(true);
            //clip3GreenArrow.SetActive(true);

            // update flag
            _clip = 3;


        }

        // called from the green progress arrow on Tutorial clipboard 3
        public void Clip3GreenArrowTrigger() {

            clip3.SetActive(false);
            clip3GreenArrow.SetActive(false);

            // update flag (0 = tutorial finished)
            _clip = 0;

            // tutorial completed!
            MySharedData.hasCompletedTutorial = true;
            wristband.SetActive(true);
        }

        // called from clip1 - rectangular button
        public void Button1() {

            Debug.Log("*********************************BUTTON1-TICK");

            if (tick3.activeSelf) {

                tick3.SetActive(false);
                _mats[0].SetTexture("_MainTex", rectangularButtonOff);
                //_mats[0] = buttonMaterialOff;
            }
            else {

                tick3.SetActive(true);
                _mats[0].SetTexture("_MainTex", rectangularButtonOn);
                //_mats[0] = buttonMaterialOn;
            }
        }

        // called from clip1 - circular button
        public void Button2() {

            if (tick4.activeSelf) {

                tick4.SetActive(false);
            }
            else {

                tick4.SetActive(true);
            }
        }

        // called from Balloon Ride Burn Trigger
        public void SetBurntFlag() {

            _hasBurnt = true;
        }
    }
}
