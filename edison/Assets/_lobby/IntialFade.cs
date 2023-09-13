/// <summary>
/// InitialFade.cs
/// fade in when scene starts
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class IntialFade : MonoBehaviour {

        public FadeScreen fadeScreen;

        private void Awake() {

            StartCoroutine(FadeTheScreen());
        }

        IEnumerator FadeTheScreen() {

            fadeScreen.FadeIn();
            yield return new WaitForSeconds(fadeScreen.fadeDuration);
        }
    }
}
