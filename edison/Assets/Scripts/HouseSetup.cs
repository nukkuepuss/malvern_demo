/// <summary>
/// logic for the house clipboard
/// takes trigger inputs from 9 triggers (buttons)
/// size of arrays in Inspector must match
/// horrible hack to get the house button working
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class HouseSetup : MonoBehaviour {

        public GameObject[] togglableObjects;
        public string _initialState;
        public string[] offToOnStates;
        public string[] onToOffStates;
        public Texture2D[] onPics;
        public Texture2D[] offPics;
        public Texture2D houseOnPic;
        public Texture2D houseOffPic;

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

                Debug.Log("Error : _code length mismatch");
            }

            // we're passed an 8-char string to decode and act upon (0=off, 1=on, 2=no change)
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
                    _mats[i+ _numberOfObjects].color = Color.black;
                }
            }

            // hack the house button as it doesn't have it's own gameobject to toggle
            if (togglableObjects[0].activeSelf || togglableObjects[1].activeSelf || togglableObjects[2].activeSelf || togglableObjects[3].activeSelf) {

                _mats[25].mainTexture = houseOnPic;
                _mats[26].color = Color.red;
            }
            else {

                _mats[25].mainTexture = houseOffPic;
                _mats[26].color = Color.black;
            }
        }

        // just a bunch of triggers from here...
        // house trigger is special case
        public void HouseTrigger(int _picElement, int _fringeElement) {

            // are any of the four house levels active? Hack it.
            if (togglableObjects[0].activeSelf || togglableObjects[1].activeSelf || togglableObjects[2].activeSelf || togglableObjects[3].activeSelf) {

                _mats[_picElement].mainTexture = houseOffPic;
                _mats[_fringeElement].color = Color.black;
                ReadPunchtape("00000000");
            }
            else {

                _mats[_picElement].mainTexture = houseOnPic;
                _mats[_fringeElement].color = Color.red;
                ReadPunchtape("11110000");
            }
        }

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

        public void LowerTrigger(int _pos) {

            bool _isOn = togglableObjects[_pos].activeSelf;

            if (_isOn) {

                ReadPunchtape(onToOffStates[_pos]);

            }
            else {
                ReadPunchtape(offToOnStates[_pos]);
            }
        }

        public void ExteriorTrigger(int _pos) {

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
