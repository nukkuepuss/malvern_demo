/// <summary>
/// InstantiateManager.cs
/// provides easy access to the instantiated object
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class InstantiateManager : MonoBehaviour {

        public GameObject myObject;

        public static InstantiateManager instance;

        private void Awake() {

            if (instance==null) {

                instance = this;

            }
        }

        private void OnDestroy() {

            if (instance==this) {

                instance = null;

            }
        }
    }
}
