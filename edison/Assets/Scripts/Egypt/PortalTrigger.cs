/// <summary>
/// PortalTrigger.cs
/// enter collider on portal to progress
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

namespace com.jonrummery.edison {

    public class PortalTrigger : MonoBehaviour {

        public FadeScreen fadeScreen;

        public int nextLevel;
        //public Camera cam;

        private void OnTriggerEnter(Collider other) {

            if (other.tag == "Player") {

                Debug.Log("--------------------success");

                // hide all objects (Unity makes the Oculus display all janky when changing scenes)
                //cam.cullingMask = (1 << LayerMask.NameToLayer("Nothing"));

                // load main scene (see Build Settings)
                //SceneManager.LoadScene(nextLevel);

                // back to Malvern scene with fade
                FadeToScene();
            }
        }

        public void FadeToScene() {

            StartCoroutine(FadeToSceneRoutine());
        }

        IEnumerator FadeToSceneRoutine() {

            fadeScreen.FadeOut();
            yield return new WaitForSeconds(fadeScreen.fadeDuration);

            SceneManager.LoadScene(nextLevel);
        }
    }
}
