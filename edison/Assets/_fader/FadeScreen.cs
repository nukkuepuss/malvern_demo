/// <summary>
/// FadeScreen.cs
/// to fade the screen between scene changes
/// (from https://www.youtube.com/watch?v=JCyJ26cIM0Y)
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class FadeScreen : MonoBehaviour {

        public float fadeDuration = 2;
        public Color fadeColor;
        private Renderer rend;
        public bool fadeOnStart = true;

        void Start() {

            rend = GetComponent<Renderer>();

            if (fadeOnStart) {

                FadeIn();
            }
        }

        public void FadeIn() {

            Fade(1, 0);
        }

        public void FadeOut() {

            Fade(0, 1);
        }

        public void Fade(float alphaIn, float alphaOut) {

            StartCoroutine(FadeRoutine(alphaIn, alphaOut));
        }

        public IEnumerator FadeRoutine(float alphaIn, float alphaOut) {

            float timer = 0;

            while (timer <= fadeDuration) {

                Color newColor = fadeColor;

                newColor.a = Mathf.Lerp(alphaIn, alphaOut, (timer/fadeDuration));

                rend.material.SetColor("_Color", newColor);

                timer += Time.deltaTime;
                yield return null;
            }

            // make sure alphaOut is last thing seen
            Color newColor2 = fadeColor;
            newColor2.a = alphaOut;
            rend.material.SetColor("_Color", newColor2);
        }
    }
}
