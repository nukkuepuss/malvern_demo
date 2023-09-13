/// <summary>
/// WayPoint.cs
/// collision detection for passage through a waypoint
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class WayPoint : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {

            if (other.tag == "bingo") {

                MySharedData.nextWaypoint++;
            }
        }
    }
}