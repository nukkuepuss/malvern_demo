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

            if (other.tag=="bingo") {

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
