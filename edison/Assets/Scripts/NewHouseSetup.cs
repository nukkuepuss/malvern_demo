/// <summary>
/// logic for the house clipboard
/// takes trigger inputs from 10 triggers (buttons)
/// size of arrays in Inspector must match
/// (0=off, 1=on, 2=no change)
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace com.jonrummery.edison {

    public class NewHouseSetup : MonoBehaviour {

        public GameObject[] togglableObjects;
        public string _initialState;
        public string[] offToOnStates;
        public string[] onToOffStates;
        public Texture2D[] onPics;
        public Texture2D[] offPics;

        private int _numberOfObjects;
        private char _myData;
        private Material[] _mats;

        void Start() {

            // find out how many items we're juggling
            _numberOfObjects = togglableObjects.Length;

            // grab the materials to change later
            _mats = GetComponent<MeshRenderer>().materials;

            ReadPunchtape(_initialState);
        }

        public void ReadPunchtape(string _code) {

            if (_code.Length != _numberOfObjects) {

                // produce an error : the passed code length doesn't tally with the number of togglable objects
                MethodBase _methodBase = MethodBase.GetCurrentMethod();
                Debug.Log("========================== _code length mismatch in " + _methodBase.Name);
            }

            // we're passed a 10-char string to decode and act upon (0=off, 1=on, 2=no change)
            for (int i = 0; i < _code.Length; i++) {

                _myData = _code[i];

                if (_myData.Equals('1')) {

                    togglableObjects[i].SetActive(true);
                    _mats[i].mainTexture = onPics[i];
                    _mats[i + _numberOfObjects].color = Color.red;
                }
                else if (_myData.Equals('0')) {

                    togglableObjects[i].SetActive(false);
                    _mats[i].mainTexture = offPics[i];
                    _mats[i + _numberOfObjects].color = Color.black;
                }
            }
        }

        // just a bunch of triggers from here...
        public void FirstTrigger(int _pos) {

            bool _isOn = togglableObjects[_pos].activeSelf;

            if (_isOn) {

                ReadPunchtape(onToOffStates[_pos]);

            }
            else {
                ReadPunchtape(offToOnStates[_pos]);
            }
        }

        public void GroundTrigger(int _pos) {

            bool _isOn = togglableObjects[_pos].activeSelf;

            if (_isOn) {

                ReadPunchtape(onToOffStates[_pos]);

            }
            else {
                ReadPunchtape(offToOnStates[_pos]);
            }
        }

        public void LandingTrigger(int _pos) {

            bool _isOn = togglableObjects[_pos].activeSelf;

            if (_isOn) {

                ReadPunchtape(onToOffStates[_pos]);

            }
            else {
                ReadPunchtape(offToOnStates[_pos]);
            }
        }

        public void BedroomTrigger(int _pos) {

            bool _isOn = togglableObjects[_pos].activeSelf;

            if (_isOn) {

                ReadPunchtape(onToOffStates[_pos]);

            }
            else {
                ReadPunchtape(offToOnStates[_pos]);
            }
        }

        public void CeilingTrigger(int _pos) {

            bool _isOn = togglableObjects[_pos].activeSelf;

            if (_isOn) {

                ReadPunchtape(onToOffStates[_pos]);

            }
            else {
                ReadPunchtape(offToOnStates[_pos]);
            }
        }
        public void ContentsTrigger(int _pos) {

            bool _isOn = togglableObjects[_pos].activeSelf;

            if (_isOn) {

                ReadPunchtape(onToOffStates[_pos]);

            }
            else {
                ReadPunchtape(offToOnStates[_pos]);
            }
        }

        public void ConferenceTrigger(int _pos) {

            bool _isOn = togglableObjects[_pos].activeSelf;

            if (_isOn) {

                ReadPunchtape(onToOffStates[_pos]);

            }
            else {
                ReadPunchtape(offToOnStates[_pos]);
            }
        }

        public void Ex1Trigger(int _pos) {

            bool _isOn = togglableObjects[_pos].activeSelf;

            if (_isOn) {

                ReadPunchtape(onToOffStates[_pos]);

            }
            else {
                ReadPunchtape(offToOnStates[_pos]);
            }
        }

        public void Ex2Trigger(int _pos) {

            bool _isOn = togglableObjects[_pos].activeSelf;

            if (_isOn) {

                ReadPunchtape(onToOffStates[_pos]);

            }
            else {
                ReadPunchtape(offToOnStates[_pos]);
            }
        }

        public void HillfortTrigger(int _pos) {

            bool _isOn = togglableObjects[_pos].activeSelf;

            if (_isOn) {

                ReadPunchtape(onToOffStates[_pos]);

            }
            else {
                ReadPunchtape(offToOnStates[_pos]);
            }
        }
    }
}
