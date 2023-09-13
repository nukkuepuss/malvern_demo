/// <summary>
/// InitManager.cs
/// Initialize the tutorial (so that it can be repeated)
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class InitManager : MonoBehaviour {

        public FadeScreen fadeScreen;

        public GameObject controllerBanner;

        private void Awake() {

            StartCoroutine(FadeTheScreen());

            MySharedData.nextWaypoint = 0;
            controllerBanner.SetActive(false);
        }

        IEnumerator FadeTheScreen() {

            fadeScreen.FadeIn();
            yield return new WaitForSeconds(fadeScreen.fadeDuration);
        }
    }
}
