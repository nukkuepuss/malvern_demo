/// <summary>
/// ClipSolarmanager.cs
/// handles the solar (renewable energy) clipboard
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace com.jonrummery.edison {

    public class ClipSolarManager : MonoBehaviour {

        [Header("This clipboard")]
        public GameObject clip;

        [Header("Sun")]
        public GameObject sun;

        [Header("Solar stages")]
        public GameObject stage1;
        public GameObject stage2;
        public GameObject stage3;
        public GameObject stage4;

        [Header("Readouts")]
        public TMP_Text totalEnergyCost, gridElectricityPercentage, transitionElectricityPrecentage;

        [Header("Button setup")]
        public Texture2D stage1PicOn, stage2PicOn, stage3PicOn, stage4PicOn;
        public Texture2D stage1PicOff, stage2PicOff, stage3PicOff, stage4PicOff;
        public int stage1PicPos, stage2PicPos, stage3PicPos, stage4PicPos;
        public int stage1FringePos, stage2FringePos, stage3FringePos, stage4FringePos;

        private Material[] _mats;
        private int _currentNumberSolarPanels;
        private int _currentNumberWindTurbines;

        private void Start() {

            _mats = clip.GetComponent<MeshRenderer>().materials;

            //initialize
            DeactivateStage1();
            DeactivateStage2();
            DeactivateStage3();
            ActivateStage4();
        }

        public void p1Trigger() {

            if (stage1.activeSelf) {

                DeactivateStage1();
            }
            else {
                ActivateStage1();
            }
        }

        public void p2Trigger() {

            if (stage2.activeSelf) {

                DeactivateStage2();
            }
            else {
                ActivateStage2();
            }
        }

        public void p3Trigger() {

            if (stage3.activeSelf) {

                DeactivateStage3();
            }
            else {
                ActivateStage3();
            }
        }

        public void p4Trigger() {

            if (stage4.activeSelf) {

                DeactivateStage4();
            }
            else {
                ActivateStage4();
            }
        }

        void ActivateStage1() {

            stage1.SetActive(true);
            _mats[stage1PicPos].SetTexture("_MainTex", stage1PicOn);
            _mats[stage1FringePos].color = Color.green;

            _currentNumberSolarPanels = 400;
            _currentNumberWindTurbines = 0;

            DeactivateStage2();
            DeactivateStage3();
            DeactivateStage4();
        }

        void DeactivateStage1() {

            stage1.SetActive(false);
            _mats[stage1PicPos].SetTexture("_MainTex", stage1PicOff);
            _mats[stage1FringePos].color = Color.white;
        }

        void ActivateStage2() {

            stage2.SetActive(true);
            _mats[stage2PicPos].SetTexture("_MainTex", stage2PicOn);
            _mats[stage2FringePos].color = Color.green;

            _currentNumberSolarPanels = 800;
            _currentNumberWindTurbines = 0;

            DeactivateStage1();
            DeactivateStage3();
            DeactivateStage4();
        }

        void DeactivateStage2() {

            stage2.SetActive(false);
            _mats[stage2PicPos].SetTexture("_MainTex", stage2PicOff);
            _mats[stage2FringePos].color = Color.white;
        }

        void ActivateStage3() {

            stage3.SetActive(true);
            _mats[stage3PicPos].SetTexture("_MainTex", stage3PicOn);
            _mats[stage3FringePos].color = Color.green;

            _currentNumberSolarPanels = 1200;
            _currentNumberWindTurbines = 0;

            DeactivateStage2();
            DeactivateStage1();
            DeactivateStage4();
        }

        void DeactivateStage3() {

            stage3.SetActive(false);
            _mats[stage3PicPos].SetTexture("_MainTex", stage3PicOff);
            _mats[stage3FringePos].color = Color.white;
        }

        void ActivateStage4() {

            stage4.SetActive(true);
            _mats[stage4PicPos].SetTexture("_MainTex", stage4PicOn);
            _mats[stage4FringePos].color = Color.green;

            _currentNumberSolarPanels = 1200;
            _currentNumberWindTurbines = 4;


            DeactivateStage2();
            DeactivateStage3();
            DeactivateStage1();
        }

        void DeactivateStage4() {

            stage4.SetActive(false);
            _mats[stage4PicPos].SetTexture("_MainTex", stage4PicOff);
            _mats[stage4FringePos].color = Color.white;
        }

    }
}
