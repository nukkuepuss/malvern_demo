/// <summary>
/// fix clipboard to hand
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class ClipboardManager : MonoBehaviour {

        public GameObject anchor;
        public Transform offset;

        private void Start() {

            // parent the bracer to a hand
            this.transform.SetParent(anchor.transform);

            // adjust bracer position relative to hand
            this.transform.position = new Vector3(offset.transform.position.x,
                                                    offset.transform.position.y,
                                                    offset.transform.position.z);
            this.transform.rotation = offset.transform.rotation;
        }
    }
}
