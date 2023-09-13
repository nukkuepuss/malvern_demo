/// <summary>
/// OutOfBounds.cs
/// return anything that wanders off too far from the tutorial zone
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class OutOfBounds : MonoBehaviour {

        public GameObject player;
        public Transform restartTransform;
        public GameObject resetBanner;

        private void Awake() {

            resetBanner.SetActive(false);
        }

        private void OnTriggerEnter(Collider other) {

            if (other.tag == "bingo") {

                StartCoroutine(DisplayResetBanner());
            }
        }

        IEnumerator DisplayResetBanner() {

            resetBanner.SetActive(true);

            player.transform.position = restartTransform.position;
            player.transform.rotation = restartTransform.rotation;

            yield return new WaitForSeconds(2);

            resetBanner.SetActive(false);

            //yield return null;
        }
    }
}
