/// <summary>
/// WaypointController.cs
/// Handles the instantiation of waypoints for tutorial
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class WaypointController : MonoBehaviour {

        public GameObject firstWayPoint;

        private void Start() {

            firstWayPoint.SetActive(true);
        }
    }
}
