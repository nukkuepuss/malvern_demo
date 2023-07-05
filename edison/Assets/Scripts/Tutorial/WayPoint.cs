/// <summary>
/// WayPoint.cs
/// collider for detecting passage through a waypoint
/// with aural and haptic success reinforcement (placed elsewhere as the collider's object is destroyed before we want the reinforcement)
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class WayPoint : MonoBehaviour {

        public GameObject wayPointParent;
        public ReinforceSuccess nextScript;

        private void OnTriggerEnter(Collider other) {

            Debug.Log("----------------------waypoint!");

            nextScript.ProvideReinforcement();

            Destroy(wayPointParent);
        }
    }
}