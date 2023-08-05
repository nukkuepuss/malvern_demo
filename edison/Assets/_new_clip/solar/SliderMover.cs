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

            if (other.tag=="bingo" && SliderInRange()) {

                MySharedData.windSliderPos = thisLevel;

                slider.transform.localPosition = new Vector3(xPos, 0f, zPos);
            }
        }

        bool SliderInRange() {

            // we only want to move the slider if it's adjacent to the current position
            return((thisLevel==MySharedData.windSliderPos-2)||(thisLevel==MySharedData.windSliderPos+2)
                || (thisLevel == MySharedData.windSliderPos - 1) || (thisLevel == MySharedData.windSliderPos + 1));
        }
    }
}
