/// <summary>
/// LightShow.cs
/// strobe the light illuminating the tutorial waypoint
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class LightShow : MonoBehaviour {

        private Light _light;

        private void Start() {

            _light = GetComponent<Light>();
        }

        void Update() {

            _light.intensity = Mathf.PingPong((Time.time*2), 3)+2;
        }
    }
}
