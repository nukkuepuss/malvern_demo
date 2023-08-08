/// <summary>
/// NewConferenceManager.cs
/// handles the Conference centre clipboard buttons, actions and persistance
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class NewConferenceManager : MonoBehaviour {

        [Header("This clipboard")]
        public GameObject thisClipboard;

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

        void Start() {

            // get the clipboard materials so that we can change the button pic/fringe
            _mats = GetComponent<MeshRenderer>().materials;

            // set the number of models
            _numberofModels = models.Length;

            // initalize models and button states
            ActivateEx1();
            DeactivatePlanetarium();
            DeactivateMinimap();
        }

        private void OnEnable() {

            // make this clipboard active
            thisClipboard.SetActive(true);

            // show the Conference Centre
            //models[_numberofModels - 1].SetActive(true);

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

        public void DestroyConferenceClipboard() {

            // turn off all the models for this clipboard (including the Conference Centre itself - the last model)
            for (int i = 0; i < _numberofModels; i++) {

                models[i].SetActive(false);
            }

            // update the last active clipboard
            MySharedData.lastActiveClipboard = MySharedData.hausClipboards.conference;

            // turn off the clipboard
            thisClipboard.SetActive(false);
        }

        public void Ex1Trigger() {

            // models[0] = Exhibition1
            if (models[0].activeSelf) {

                DeactivateEx1();
            }

            else {

                ActivateEx1();
                DeactivatePlanetarium();
                DeactivateMinimap();
            }
        }
        public void PlanetariumTrigger() {

            // models[1] = Planetarium
            if (models[1].activeSelf) {

                DeactivatePlanetarium();
            }

            else {

                ActivatePlanetarium();
                DeactivateEx1();
                DeactivateMinimap();
            }
        }

        public void MinimapTrigger() {

            // models[2] = minimap
            if (models[2].activeSelf) {

                DeactivateMinimap();
            }

            else {

                ActivateMinimap();
                DeactivateEx1();
                DeactivatePlanetarium();
            }
        }

        void ActivateEx1() {

            _mats[buttonMatPos[0]].SetTexture("_MainTex", onPics[0]);
            _mats[fringeMatPos[0]].color = Color.green;
            models[0].SetActive(true);
        }

        void ActivatePlanetarium() {

            _mats[buttonMatPos[1]].SetTexture("_MainTex", onPics[1]);
            _mats[fringeMatPos[1]].color = Color.green;
            models[1].SetActive(true);
        }

        void ActivateMinimap() {

            _mats[buttonMatPos[2]].SetTexture("_MainTex", onPics[2]);
            _mats[fringeMatPos[2]].color = Color.green;
            models[2].SetActive(true);
        }
        void DeactivateEx1() {

            _mats[buttonMatPos[0]].SetTexture("_MainTex", offPics[0]);
            _mats[fringeMatPos[0]].color = Color.white;
            models[0].SetActive(false);
        }

        void DeactivatePlanetarium() {

            _mats[buttonMatPos[1]].SetTexture("_MainTex", offPics[1]);
            _mats[fringeMatPos[1]].color = Color.white;
            models[1].SetActive(false);
        }

        void DeactivateMinimap() {

            _mats[buttonMatPos[2]].SetTexture("_MainTex", offPics[2]);
            _mats[fringeMatPos[2]].color = Color.white;
            models[2].SetActive(false);
        }
    }
}
