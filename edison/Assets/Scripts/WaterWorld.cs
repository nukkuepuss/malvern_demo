/// <summary>
/// move a big cube up and down to simulate the sea level rising and falling
/// Button.Two + Axis2D.PrimaryThumbstick.x = move
/// Button.Two + Axis2D.Button.PrimaryThumbstick = reset
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class WaterWorld : MonoBehaviour {

        public GameObject water;

        private float _stick;
        private Vector3 _originalWaterPosition;

        private void Start() {
            // store the original position of the water so we can reset it
            _originalWaterPosition = water.transform.position;
        }

        private void Update() {

            // these features are accessed via Button Two ('B') + left-stick left/right movement
            if ((OVRInput.Get(OVRInput.Button.Two))) {

                // show water
                water.SetActive(true);

                // get state of left thumbstick
                _stick = (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y);

                // check to see if the left thumbstick is moved
                if (_stick != 0) {
                    // move the water
                    water.transform.position += new Vector3(0, _stick, 0);
                }

                // reset the water with left thumbstick click
                if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick)) {

                    //reset the water
                    water.transform.position = _originalWaterPosition;
                }
            }
            else {
                water.SetActive(false);
            }
        }
    }
}
