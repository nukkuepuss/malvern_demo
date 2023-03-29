/// <summary>
/// button1_trigger -> script (BracerSetup)
/// TODO : make class scriptable
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    //[RequireComponent(typeof(MeshCollider))]

    public class Button1Trigger : MonoBehaviour {

        [Tooltip("Script with trigger action script")]
        public BracerSetup bracerScript;
        [Range(0, 1)]
        public float hapticFrequency;
        [Range(0, 1)]
        public float hapticAmplitude;

        //private MeshCollider _myMeshCollider;

        //private void Awake() {

        //    _myMeshCollider = GetComponent<MeshCollider>();
        //    _myMeshCollider.GetComponent<MeshCollider>().isTrigger = true;
        //    _myMeshCollider.GetComponent<MeshCollider>().convex = true;
        //}

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
                bracerScript.B1Trigger();
            }
        }

        IEnumerator HapticFeedback() {
            // set haptics on and off
            OVRInput.SetControllerVibration(hapticFrequency, hapticAmplitude, OVRInput.Controller.LTouch);
            yield return new WaitForSeconds(0.1f);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
    }
}
