/// <summary>
/// destroy any objects that have fallen through the world
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class DestroyObject : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {

            Destroy(other.gameObject);
        }
    }
}
