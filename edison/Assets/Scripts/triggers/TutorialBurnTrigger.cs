/// <summary>
/// change a flag in TutorialManager
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class TutorialBurnTrigger : MonoBehaviour {

        public TutorialManager managerScript;

        private void OnTriggerEnter(Collider other) {
            Debug.Log("burn-OnTriggerEnter called by " + this.gameObject.name + " due to " + other.tag);

            // (1) is it the collider we're looking for? It's on the Quest PUN Template RHand in Resources.
            if (other.tag == "bingo") {

                // set the flag
                managerScript.SetBurntFlag();

                //  we won't be needing this anymore
                Destroy(this);
            }
        }
    }
}