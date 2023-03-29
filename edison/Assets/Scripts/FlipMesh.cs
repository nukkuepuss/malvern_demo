/// <summary>
/// flip a mesh
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace com.jonrummery.edison {

    public class FlipMesh : MonoBehaviour {

        private void Awake() {

            Mesh _mesh = GetComponent<MeshFilter>().mesh;

            _mesh.triangles = _mesh.triangles.Reverse().ToArray();
        }
    }
}
