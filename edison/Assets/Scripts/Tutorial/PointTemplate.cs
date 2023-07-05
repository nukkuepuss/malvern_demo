/// <summary>
/// Sequencer for tutorial waypoints
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class PointTemplate : MonoBehaviour {

        public bool isLast;
        public GameObject nextWaypoint;

        private void OnDisable() {

            if (!isLast) {

                nextWaypoint.SetActive(true);
            }
            else {

                Debug.Log("------------END TUTORIAL");
            }
        }
    }
}
