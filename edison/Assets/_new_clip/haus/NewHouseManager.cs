/// <summary>
/// NewHouseManager.cs
/// handles the House clipboard buttons, actions and persistance
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class NewHouseManager : MonoBehaviour {

        [Header("This clipboard")]
        public GameObject thisClipboard;
        //public GameObject otherClip1;
        //public GameObject otherClip2;

        [Header("Models for this clipboard")]
        public GameObject[] models;

        [Header("Button pics")]
        public Texture2D[] onPics;
        public Texture2D[] offPics;

        [Header("Button material positions")]
        public int[] buttonMatPos;
        public int[] fringeMatPos;

        private Material[] _mats;
        private int _numberofModels;

        private void Start() {

            // get the clipboard materials so that we can change the button pic/fringe
            _mats = GetComponent<MeshRenderer>().materials;

            // set the number of models
            _numberofModels = models.Length;

            // initalize models and button states : we want them all on when the clipboard is first selected
            ActivateInnerWalls();
            ActivateOuterWalls();
            ActivateFoundation();
            ActivateRoof();
            ActivateEnergy();
        }

        private void OnEnable() {

            // make this clipboard active
            thisClipboard.SetActive(true);

            // cycle through the number of models to set each button/model On or Off
            for (int i = 0; i < _numberofModels; i++) {

                // we can use use the fringe color to see if the models should be active or not (as it won't change when disabled/enabled)
                if (_mats[fringeMatPos[i]].color == Color.green) {

                    models[i].SetActive(true);
                }

                else {

                    models[i].SetActive(false);
                }
            }
        }

        public void DestroyHausClipboard() {

            // turn off all the models for this clipboard
            for (int i = 0; i < _numberofModels; i++) {

                models[i].SetActive(false);
            }

            MySharedData.lastActiveClipboard = MySharedData.hausClipboards.house;

            // turn off the clipboard
            thisClipboard.SetActive(false);
        }

        public void InnerWallsTrigger() {

            // models[0] = inner walls
            if (models[0].activeSelf) {

                DeactivateInnerWalls();
            }

            else {

                ActivateInnerWalls();
            }
        }

        public void OuterWallsTrigger() {

            // models[1] = outer walls
            if (models[1].activeSelf) {

                DeactivateOuterWalls();
            }

            else {
                ActivateOuterWalls();
            }
        }

        public void FoundationTrigger() {

            // models[2] = foundation
            if (models[2].activeSelf) {

                DeactivateFoundation();
            }

            else {

                ActivateFoundation();
            }
        }

        public void RoofTrigger() {

            // models[3] = roof
            if (models[3].activeSelf) {

                DeactivateRoof();
            }

            else {

                ActivateRoof();
            }
        }

        public void EnergyTrigger() {

            // models[4] = energy
            if (models[4].activeSelf) {

                DeactivateEnergy();
            }

            else {

                ActivateEnergy();
            }
        }

        void ActivateInnerWalls() {

            _mats[buttonMatPos[0]].SetTexture("_MainTex", onPics[0]);
            _mats[fringeMatPos[0]].color = Color.green;
            models[0].SetActive(true);
        }

        void ActivateOuterWalls() {

            _mats[buttonMatPos[1]].SetTexture("_MainTex", onPics[1]);
            _mats[fringeMatPos[1]].color = Color.green;
            models[1].SetActive(true);
        }

        void ActivateFoundation() {

            _mats[buttonMatPos[2]].SetTexture("_MainTex", onPics[2]);
            _mats[fringeMatPos[2]].color = Color.green;
            models[2].SetActive(true);
        }

        void ActivateRoof() {

            _mats[buttonMatPos[3]].SetTexture("_MainTex", onPics[3]);
            _mats[fringeMatPos[3]].color = Color.green;
            models[3].SetActive(true);
        }

        void ActivateEnergy() {

            _mats[buttonMatPos[4]].SetTexture("_MainTex", onPics[4]);
            _mats[fringeMatPos[4]].color = Color.green;
            models[4].SetActive(true);
        }

        void DeactivateInnerWalls() {

            _mats[buttonMatPos[0]].SetTexture("_MainTex", offPics[0]);
            _mats[fringeMatPos[0]].color = Color.white;
            models[0].SetActive(false);
        }

        void DeactivateOuterWalls() {

            _mats[buttonMatPos[1]].SetTexture("_MainTex", offPics[1]);
            _mats[fringeMatPos[1]].color = Color.white;
            models[1].SetActive(false);
        }

        void DeactivateFoundation() {

            _mats[buttonMatPos[2]].SetTexture("_MainTex", offPics[2]);
            _mats[fringeMatPos[2]].color = Color.white;
            models[2].SetActive(false);
        }

        void DeactivateRoof() {

            _mats[buttonMatPos[3]].SetTexture("_MainTex", offPics[3]);
            _mats[fringeMatPos[3]].color = Color.white;
            models[3].SetActive(false);
        }

        void DeactivateEnergy() {

            _mats[buttonMatPos[4]].SetTexture("_MainTex", offPics[4]);
            _mats[fringeMatPos[4]].color = Color.white;
            models[4].SetActive(false);
        }
    }
}
