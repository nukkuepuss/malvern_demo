/// <summary>
/// ClipSolarmanager.cs
/// handles the solar (renewable energy) clipboard
/// </summary>

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.jonrummery.edison {

    public class ClipSolarManager : MonoBehaviour {

        [Header("This clipboard")]
        public GameObject clip;

        [Header("Sun")]
        public GameObject sun;

        [HideInInspector]
        public bool windSpeedHasChanged;

        [Header("Solar stages")]
        public GameObject stage0;
        public GameObject stage1;
        public GameObject stage2;
        public GameObject stage3;
        public GameObject stage4;

        [Header("Slider")]
        public Slider renwableSlider;

        [Header("Readouts")]
        public TMP_Text investmentText;
        public TMP_Text dwellingsText;
        public TMP_Text costText;
        public TMP_Text carbonText;

        [Header("On Pics")]
        public Texture2D stage1PicOn;
        public Texture2D stage2PicOn;
        public Texture2D stage3PicOn;
        public Texture2D stage4PicOn;

        [Header("Off Pics")]
        public Texture2D stage1PicOff;
        public Texture2D stage2PicOff;
        public Texture2D stage3PicOff;
        public Texture2D stage4PicOff;

        [Header("Pic elements")]
        public int stage1PicPos;
        public int stage2PicPos;
        public int stage3PicPos;
        public int stage4PicPos;

        [Header("Fringe elements")]
        public int stage1FringePos;
        public int stage2FringePos;
        public int stage3FringePos;
        public int stage4FringePos;

        // to store the clipboard materials
        Material[] _mats;

        // to hold the currently active Stage (Stage 4 is on at initialization, but max multiplier is 3 as there's only 3 solar arrays)
        int _stageMultiplier = 3;

        // for working out the amount of renewable energy
        float _sunRadiance;
        int _windTotal;
        float _solarTotal;

        // to keep track of data readouts representing current selection
        int _investment;
        int _dwellings;
        int _cost = 52;
        int _carbon = 0;

        private void Start() {

            // reference the clipboard materials to change pics and controller models
            _mats = clip.GetComponent<MeshRenderer>().materials;

            // initialize : a horrible hack to get the buttons diplaying properly
            p4Trigger();
            p3Trigger();
            p2Trigger();
            p1Trigger();

            float xValue = calcX();
            float yValue = calcY();
            _sunRadiance = (xValue * yValue);
        }

        private void Update() {

            // get a value for the sun's radance (if the sun has been moved)
            if (MySharedData.hasSunMoved) {

                float xValue = calcX();
                float yValue = calcY();

                // we now have a value (0-45) for the sun's radiance based on the solar panel orientation
                _sunRadiance = (xValue * yValue);

                MySharedData.hasSunMoved = false;
            }

            // update the renweable/grid slider
            renwableSlider.value = GetRenewableSliderValue(_sunRadiance);

            // update the data readouts
            investmentText.text = ("£" + _investment.ToString());
            dwellingsText.text = _dwellings.ToString();

            float _tempCost = ((_cost - (_sunRadiance / 10)) - (_dwellings/100));
            float _tempCarbon = ((_carbon + (_sunRadiance / 10))*_dwellings);

            // check whether the wind speed slider has changed, as this will affect some values
            if (stage4.activeSelf) {

                _tempCost -= (_windTotal / 10);
                _tempCarbon += (_windTotal * 30);

                //cost.text = (((_cost - (_windTotal / 10))-(_sunRadiance/10)).ToString()+"p");
               // carbon.text = ((_carbon + (_windTotal * 30)).ToString()+"Kg");
            }

            costText.text = (_tempCost.ToString("F2")+"p");
            carbonText.text = (_tempCarbon.ToString("F1")+"Kg");

            //else {

            //   // cost.text = (_cost.ToString() + "p");
            //   // carbon.text = (_carbon.ToString() + "Kg");
            //}
        }

        float GetRenewableSliderValue(float _sunRadiance) {

            // value for renewable energy is made up of the radiance of the sun times the number of solar arrays
            // plus the wind speed strength (if the wind turbines in Stage4 are activated)

            //// get a value for the sun's radance (if the sun has been moved)
            //if (MySharedData.hasSunMoved) {

            //    float xValue = calcX();
            //    float yValue = calcY();

            //    // we now have a value (0-45) for the sun's radiance based on the solar panel orientation
            //    _sunRadiance = (xValue * yValue);

            //    MySharedData.hasSunMoved = false;
            //}

            // compute final solar energy amount
            _solarTotal = (_sunRadiance * _stageMultiplier);

            // compute final wind energy amount
            if (stage4.activeSelf) {

                _windTotal = (MySharedData.windSliderPos * 10);
            }
            else {

                _windTotal = 0;
            }

            // our slider has grid energy displayed left-to-right and has a max value of 225,
            // so subtract values from 225 so that the renewable value tracks right-to-left
            return (225 - (_solarTotal + _windTotal));
        }

        float calcX() {

            // ideal value is 45; 45-90 are good (sun zenith); 0-45 poor (sun horizon)
            float x = sun.transform.eulerAngles.x;

            if (x < 0f || x > 90f) {

                // sun is below horizon; darkness
                return 0;
            }

            if (x >= 45 && x <= 90) {

                // above 45 degrees there's still loads of sun, so let's say 35 for high noon (90 degrees) and 45 for ideal (45 degrees)
                x = (35 + (Mathf.InverseLerp(90f, 45f, x)) * 10);
            }
            else {

                // between horizon and ideal value is a steady increment from 0-45, so don't need to do anything
            }

            // return a value (0-45) with two decimal places
            return (Mathf.Round(x * 100f) * 0.01f);
        }

        float calcY() {

            // ideal y value is 90
            // 0-180 = sun correct hemisphere
            // 180-360 = sun behind solar panel
            float y = sun.transform.eulerAngles.y;

            // there's two cases : y is between 0-180 and y is between 180-360
            if (y >= 0 && y <= 180) {

                y = (90 - FindDifference(y, 90f));
            }
            else {

                y = ((90 - FindDifference(y, 270f)) * -1); // times by -1 to make -ve value
            }

            // calculate a value for y between 0 and 1
            y = (Mathf.InverseLerp(-90f, 90f, y));

            // return a value of two decimal places
            return (Mathf.Round(y * 100f) * 0.01f);
        }

        float FindDifference(float x, float y) {

            return Mathf.Abs(x - y);
        }

        #region triggers

        public void p1Trigger() {

            if (stage1.activeSelf) {

                DeactivateStage1();
                stage0.SetActive(true);

                // update data
                ClearData();
            }
            else {

                ActivateStage1();

                // update data
                _stageMultiplier = 1;
                _investment = 120000;
                _dwellings = 20;
                //_cost = 50;
                //_carbon = 900;
            }
        }

        public void p2Trigger() {

            if (stage2.activeSelf) {

                DeactivateStage2();
                stage0.SetActive(true);


                // update data
                ClearData();
            }
            else {

                ActivateStage2();

                // update data
                _stageMultiplier = 2;
                _investment = 240000;
                _dwellings = 125;
                //_cost = 43;
                //_carbon = 2000;
            }
        }

        public void p3Trigger() {

            if (stage3.activeSelf) {

                DeactivateStage3();
                stage0.SetActive(true);


                // update data
                ClearData();
            }
            else {

                ActivateStage3();

                // update data
                _stageMultiplier = 3;
                _investment = 360000;
                _dwellings = 276;
                //_cost = 32;
                //_carbon = 3300;
            }
        }

        public void p4Trigger() {

            if (stage4.activeSelf) {

                DeactivateStage4();
                stage0.SetActive(true);


                // update data
                ClearData();
            }
            else {

                ActivateStage4();

                // update data
                _stageMultiplier = 3;
                _investment = 745000;
                _dwellings = 276;
                //_cost = 26;
                //_carbon = 5600;
            }
        }

        void ClearData() {

            _stageMultiplier = 0;
            _investment = 0;
            _dwellings = 0;
            _cost = 52;
            _carbon = 0;
        }

        void ActivateStage1() {

            // change button
            stage1.SetActive(true);
            _mats[stage1PicPos].SetTexture("_MainTex", stage1PicOn);
            _mats[stage1FringePos].color = Color.green;

            // blanket deactivation of other buttons
            // stage0 doesn't have any button change, so can be handled differently
            stage0.SetActive(false);
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

            stage0.SetActive(false);
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

            stage0.SetActive(false);
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

            stage0.SetActive(false);
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

    #endregion
}

