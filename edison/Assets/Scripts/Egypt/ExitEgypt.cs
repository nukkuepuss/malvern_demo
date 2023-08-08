/// <summary>
/// emergency escape from Egypt
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class ExitEgypt : MonoBehaviour {

        private bool _actionButton1;

        void Start() {
        }

        void Update() {

            _actionButton1 = (OVRInput.Get(OVRInput.Button.One));

            if (_actionButton1) {

                StartCoroutine("WaitABit");

            }
        }

        IEnumerator WaitABit() {

            yield return new WaitForSeconds(5);

            if (_actionButton1) {



            }
        }
    }
}
