/// <summary>
/// BoostSlider.cs
/// Move a slider when triggered to change the boost speed
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class BoostSlider : MonoBehaviour {

        public int sliderSetting;
        public GameObject slider;
        public float xPos;
        public float zPos;

        public MovePlayer _movementScript;

        private void OnTriggerEnter(Collider other) {

            // (1) is it the collider we're looking for? It's tagged 'bingo'.
            if ((other.tag == "bingo") &&

                // (2) is the right-hand middle-finger trigger held down?
                ((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 1)) &&

                    // (3) check the index finger trigger isn't held down (ie. we're pointing).
                    (!OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger)) &&

                        // (4) is the collider adjacent to the current slider position?
                        SliderInRange()) {


                slider.transform.localPosition = new Vector3(xPos, 0f, zPos);

                MySharedData.boostSliderPos = sliderSetting;

                _movementScript.boostSpeed = sliderSetting;
            }
        }

        bool SliderInRange() {

            // we only want to move the slider if it's adjacent to the current position
            return ((sliderSetting == MySharedData.boostSliderPos - 2) || (sliderSetting == MySharedData.boostSliderPos + 2)
                || (sliderSetting == MySharedData.boostSliderPos - 1) || (sliderSetting == MySharedData.boostSliderPos + 1));
        }
    }
}