/// <summary>
/// PressMe.cs
/// trigger for button press to instantiate a networked object
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace com.jonrummery.edison {

    public class PressMe : MonoBehaviour {

        public GameObject refObject;

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
                Debug.Log("tap...");

                GameObject obj = (PhotonNetwork.Instantiate(refObject.name, InstantiateManager.instance.myObject.transform.position, InstantiateManager.instance.myObject.transform.rotation, 0));
            }
        }
    }
}
