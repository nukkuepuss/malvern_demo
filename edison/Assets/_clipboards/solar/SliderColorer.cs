/// <summary>
/// SliderColorer.cs
/// change the color of the slider when successfully activated
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class SliderColorer : MonoBehaviour {

        public Material onMat, offMat;

        private MeshRenderer _mesh;

        private void Start() {

            _mesh = GetComponent<MeshRenderer>();
            _mesh.material = offMat;
        }

        private void OnTriggerEnter(Collider other) {

            // (1) is it the collider we're looking for? It's on the Quest PUN Template RHand in Resources.
            if ((other.tag == "bingo") &&

                // (2) is the right-hand middle-finger trigger held down?
                ((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 1)) &&

                // (3) finally, check the index finger trigger isn't held down...
                (!OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))) {

                _mesh.material = onMat;
            }
        }

        private void OnTriggerExit(Collider other) {

            if (other.tag=="bingo") {

                _mesh.material = offMat;
            }
        }
    }
}
