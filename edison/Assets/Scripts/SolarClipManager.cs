/// <summary>
/// handles the solar clipboard triggers and events
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.jonrummery.edison {

    public class SolarClipManager : MonoBehaviour {

        public GameObject sun;
        public Text display;
        private float KWh;
        private Quaternion sunRotation;

        public GameObject stage1, stage2, stage3, stage4;
        public Texture2D stage1On, stage2On, stage3On, stage4On;
        public Texture2D stage1Off, stage2Off, stage3Off, stage4Off;
        public int stage1PicPos, stage2PicPos, stage3PicPos, stage4PicPos;
        public int stage1FringePos, stage2FringePos, stage3FringePos, stage4FringePos;

        private Material[] _mats;

        void Start() {

            _mats = GetComponentInChildren<MeshRenderer>().materials;

            DeactivateStage1();
            DeactivateStage2();
            DeactivateStage3();
            ActivateStage4();
        }

        private void Update() {

            sunRotation = sun.GetComponent<Transform>().rotation;

            if (sunRotation.x < 0) {

                KWh = 0;
            }
            else {

                KWh = Mathf.Abs(sunRotation.x * sunRotation.y) * 100;
            }


            if (stage4.activeSelf) {

                KWh = KWh * 4;
            }
            else if (stage3.activeSelf) {

                KWh = KWh * 3;
            }
            else if (stage2.activeSelf) {

                KWh = KWh * 2;
            }
            else if (!stage1.activeSelf) {

                KWh = 0;
            }

            display.text = (Mathf.Abs(KWh)).ToString("0.00") + "KWh";
        }

        public void Trigger1() {

            if (stage1.activeSelf) {

                DeactivateStage1();
            }
            else {
                ActivateStage1();
            }
        }

        void ActivateStage1() {

            stage1.SetActive(true);
            _mats[stage1PicPos].SetTexture("_MainTex", stage1On);
            _mats[stage1FringePos].color = Color.red;

            DeactivateStage2();
            DeactivateStage3();
            DeactivateStage4();
        }

        void DeactivateStage1() {

            stage1.SetActive(false);
            _mats[stage1PicPos].SetTexture("_MainTex", stage1Off);
            _mats[stage1FringePos].color = Color.black;
        }

        public void Trigger2() {

            if (stage2.activeSelf) {

                DeactivateStage2();
            }
            else {
                ActivateStage2();
            }
        }

        void ActivateStage2() {

            stage2.SetActive(true);
            _mats[stage2PicPos].SetTexture("_MainTex", stage2On);
            _mats[stage2FringePos].color = Color.red;

            DeactivateStage1();
            DeactivateStage3();
            DeactivateStage4();
        }

        void DeactivateStage2() {

            stage2.SetActive(false);
            _mats[stage2PicPos].SetTexture("_MainTex", stage2Off);
            _mats[stage2FringePos].color = Color.black;
        }

        public void Trigger3() {

            if (stage3.activeSelf) {

                DeactivateStage3();
            }
            else {
                ActivateStage3();
            }
        }

        void ActivateStage3() {

            stage3.SetActive(true);
            _mats[stage3PicPos].SetTexture("_MainTex", stage3On);
            _mats[stage3FringePos].color = Color.red;

            DeactivateStage2();
            DeactivateStage1();
            DeactivateStage4();
        }

        void DeactivateStage3() {

            stage3.SetActive(false);
            _mats[stage3PicPos].SetTexture("_MainTex", stage3Off);
            _mats[stage3FringePos].color = Color.black;
        }

        public void Trigger4() {

            if (stage4.activeSelf) {

                DeactivateStage4();
            }
            else {
                ActivateStage4();
            }
        }

        void ActivateStage4() {

            stage4.SetActive(true);
            _mats[stage4PicPos].SetTexture("_MainTex", stage4On);
            _mats[stage4FringePos].color = Color.red;

            DeactivateStage2();
            DeactivateStage3();
            DeactivateStage1();
        }

        void DeactivateStage4() {

            stage4.SetActive(false);
            _mats[stage4PicPos].SetTexture("_MainTex", stage4Off);
            _mats[stage4FringePos].color = Color.black;
        }
    }
}
