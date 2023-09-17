/// <summary>
/// SunTrigger.cs
/// Solar Planetarium Sun trigger... make the sun glow when touched
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class SunTrigger : MonoBehaviour {

        public GameObject sun;
        public Shader sunShader1, sunShader2;
        public Light sunLight;

        public float sunStrength;

        [HideInInspector]
        public bool _isGlowing;

        private Light _sunLight;

        [Range(0, 1)]
        public float hapticFrequency;
        [Range(0, 1)]
        public float hapticAmplitude;

        private void Start() {

            _sunLight = sunLight.GetComponent<Light>();
        }

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
                SunGlow();
            }
        }

        void SunGlow() {

            if (_isGlowing) {

                sun.GetComponent<Renderer>().material.shader = sunShader1;

                _isGlowing = false;
                _sunLight.intensity = 0;
            }
            else {

                sun.GetComponent<Renderer>().material.shader = sunShader2;

                _isGlowing = true;
                _sunLight.intensity = sunStrength;
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
