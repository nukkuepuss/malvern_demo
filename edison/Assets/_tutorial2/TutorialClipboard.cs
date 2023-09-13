/// <summary>
/// TutorialClipboard
/// for displaying and handling the tutorial clipboard
/// and getting back to the lobby when finished (scene0)
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.jonrummery.edison {

    public class TutorialClipboard : MonoBehaviour {

        public FadeScreen fadeScreen;

        public GameObject OVRLeft, OVRRight;
        public GameObject punLeft, punRight;

        public GameObject clip;
        public GameObject hand;

        public GameObject tick1, tick2, tick3, tick4;

        public Light leftSpotLight, rightSpotLight;

        public GameObject exitBox;

        public Texture2D rectangularOn, rectangularOff;

        public GameObject rectangularButton;
        private MeshRenderer _reactangularButtonMeshRenderer;

        private void Start() {

            _reactangularButtonMeshRenderer = rectangularButton.GetComponent<MeshRenderer>();
            _reactangularButtonMeshRenderer.material.SetTexture("_MainTex", rectangularOff);
        }

        private void Update() {

            // first tick is for holding down the right-hand-middle trigger
            // **** there's a delay here with the tick appearing **** TODO
            tick1.SetActive((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger)) == 1);

            // second tick is for when we're not touching the right-hand-index trigger (ie. success -> we're pointing)
            bool _isTouching = (OVRInput.Get(OVRInput.NearTouch.SecondaryIndexTrigger));
            tick2.SetActive(!_isTouching);

            // show green progress arrow if all 4 lights lit
            if (tick1.activeSelf && tick2.activeSelf && tick3.activeSelf && tick4.activeSelf) {

                exitBox.SetActive(true);
            }
            //else {

            //    greenArrow.SetActive(false);
            //}
        }

        public void DisplayTutorialClipboard() {

            // get rid of the controller models
            OVRLeft.SetActive(false);
            OVRRight.SetActive(false);

            // dim the spotlight (PUN hands don't look good in bright light)
            leftSpotLight.intensity = 2.4f;
            rightSpotLight.intensity = 2.4f;

            // show the PUN hands
            punLeft.SetActive(true);
            punRight.SetActive(true);

            // set up the clipboard
            clip.transform.SetParent(hand.transform, false);
            clip.SetActive(true);

            // initialize the clipboard
            exitBox.SetActive(false);
            tick1.SetActive(false);
            tick2.SetActive(false);
            tick3.SetActive(false);
            tick4.SetActive(false);
        }

        public void Clip3RectangularButton() {

            if (tick3.activeSelf) {

                tick3.SetActive(false);
                _reactangularButtonMeshRenderer.material.SetTexture("_MainTex", rectangularOff);

            }
            else {

                tick3.SetActive(true);
                _reactangularButtonMeshRenderer.material.SetTexture("_MainTex", rectangularOn);

            }
        }

        public void Clip3CircularButton() {

            if (tick4.activeSelf) {

                tick4.SetActive(false);
            }
            else {

                tick4.SetActive(true);
            }
        }

        public void Clip3GreenArrowTrigger() {


            FadeToScene();
        }

        public void FadeToScene() {

            StartCoroutine(FadeToSceneRoutine());
        }

        IEnumerator FadeToSceneRoutine() {

            fadeScreen.FadeOut();
            yield return new WaitForSeconds(fadeScreen.fadeDuration);

            SceneManager.LoadScene(0);
        }
    }
}
