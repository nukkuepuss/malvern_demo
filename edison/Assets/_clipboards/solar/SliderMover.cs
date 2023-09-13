/// <summary>
/// SilderMover.cs
/// Move a slider when triggered
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class SliderMover : MonoBehaviour {

        public int thisLevel;
        public GameObject slider;
        public float xPos;
        public float zPos;

        private void OnTriggerEnter(Collider other) {

            // (1) is it the collider we're looking for? It's tagged 'bingo'.
            if ((other.tag == "bingo") &&

                // (2) is the right-hand middle-finger trigger held down?
                ((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 1)) &&

                    // (3) check the index finger trigger isn't held down (ie. we're pointing).
                    (!OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger)) &&
                        
                        // (4) is the collider adjacent to the current slider position?
                        SliderInRange()) {

                MySharedData.windSliderPos = thisLevel;

                // y-pos is y-pos of slider
                slider.transform.localPosition = new Vector3(xPos, 0.005f, zPos);
            }
        }

        bool SliderInRange() {

            // we only want to move the slider if it's adjacent to the current position
            return ((thisLevel == MySharedData.windSliderPos - 2) || (thisLevel == MySharedData.windSliderPos + 2)
                || (thisLevel == MySharedData.windSliderPos - 1) || (thisLevel == MySharedData.windSliderPos + 1));
        }
    }
}