/// <summary>
/// TimeStep.cs
/// fix timestep at 1/90 for Quest2
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class TimeStep : MonoBehaviour {

        private void Awake() {

            OVRManager.display.displayFrequency = 90f;
        }
    }
}
