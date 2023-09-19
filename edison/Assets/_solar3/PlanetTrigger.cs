/// <summary>
/// PlanetTrigger.cs
/// rotate planet around sun when touched
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class PlanetTrigger : MonoBehaviour {

        public GameObject sun;

        public int angularVelocity;
        public int rotationSpeed;
        //public int angularVelocity;

        //[HideInInspector]
        public bool _isMoving;

        [Range(0, 1)]
        public float hapticFrequency;
        [Range(0, 1)]
        public float hapticAmplitude;

        private void OnTriggerEnter(Collider other) {

            // (1) is it the collider we're looking for? It's on the Quest PUN Template RHand in Resources.
            if ((other.tag == "bingo") &&

                // (2) is the right-hand middle-finger trigger held down?
                ((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 1)) &&

                // (3) finally, check the index finger trigger isn't held down...
                (!OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))) {

                // reinforce button-press with haptics
                StartCoroutine("HapticFeedback");

                //do something
                _isMoving = !_isMoving;
            }
        }

        void Update() {

            if (_isMoving) {

                transform.RotateAround(sun.transform.position, Vector3.up, angularVelocity * Time.deltaTime);

                this.transform.RotateAround(this.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }

        IEnumerator HapticFeedback() {
            // set haptics on and off
            OVRInput.SetControllerVibration(hapticFrequency, hapticAmplitude, OVRInput.Controller.RTouch);
            yield return new WaitForSeconds(0.1f);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
    }
}
