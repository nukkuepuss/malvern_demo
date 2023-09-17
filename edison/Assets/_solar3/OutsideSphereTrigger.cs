/// <summary>
/// OutsideSphereTrigger.cs
/// stop the planetarium animations when Player isn't inside the planetarium
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class OutsideSphereTrigger : MonoBehaviour {

        public GameObject[] planets;
        public GameObject sun;
        public Shader sunShader1;
        public Light sunLight;
        private Light _sunLight;

        private void Start() {

            _sunLight = sunLight.GetComponent<Light>();
        }

        private void OnTriggerExit(Collider other) {

            Debug.Log("-------------exit by " + other.tag);

            if (other.tag == "bingo") {

                for (int i = 0; i < planets.Length; i++) {

                    planets[i].GetComponent<PlanetTrigger>()._isMoving = false;
                }

                sun.GetComponent<Renderer>().material.shader = sunShader1;
                sun.GetComponent<SunTrigger>()._isGlowing = false;
                _sunLight.intensity = 0;
            }
        }
    }
}
