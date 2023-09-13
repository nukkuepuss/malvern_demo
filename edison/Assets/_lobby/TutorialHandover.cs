/// <summary>
/// TutorialHandover.cs
/// provides a method for starting the tutorial
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.jonrummery.edison {

    public class TutorialHandover : MonoBehaviour {

        public int tutorialBuildIndex;

        public FadeScreen fadeScreen;

        //public void StartTutorial() {

        //    SceneManager.LoadScene(tutorialBuildIndex);
        //}

        public void FadeToScene() {

            StartCoroutine(FadeToSceneRoutine());
        }

        IEnumerator FadeToSceneRoutine() {

            fadeScreen.FadeOut();
            yield return new WaitForSeconds(fadeScreen.fadeDuration);

            SceneManager.LoadScene(tutorialBuildIndex);
        }
    }
}
