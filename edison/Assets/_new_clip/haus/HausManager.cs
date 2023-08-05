/// <summary>
/// HausManager.cs
/// put this script on each haus clipboard to switch between them
/// *** truly horrible code? Can it be simplified?
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class HausManager : MonoBehaviour {

        public GameObject clipHouse, clipHillfort, clip_CR;

        public ClipHausManager handoverScriptHaus;
        public ClipHillfortManager handoverScriptHillfort;
        public ClipConferenceManager handoverScriptConference;

        private void Awake() {

            MySharedData.lastActiveClipboard = MySharedData.hausClipboards.conference;
        }

        private void OnEnable() {

            switch (MySharedData.lastActiveClipboard) {

                case MySharedData.hausClipboards.house:
                    HouseTrigger();
                    break;

                case MySharedData.hausClipboards.hillfort:
                    HillfortTrigger();
                    break;

                case MySharedData.hausClipboards.conference:
                    ConferenceTrigger();
                    break;

                default:
                    break;
            }
        }

        private void OnDisable() {

            if (clipHouse.activeSelf) {

                MySharedData.lastActiveClipboard = MySharedData.hausClipboards.house;
            }

            else if (clipHillfort.activeSelf) {

                MySharedData.lastActiveClipboard = MySharedData.hausClipboards.hillfort;
            }

            else {

                MySharedData.lastActiveClipboard = MySharedData.hausClipboards.conference;
            }
        }

        public void HouseTrigger() {

            if (!clipHouse.activeSelf) {

                handoverScriptHillfort.DestroyHillfortClipboard();
                handoverScriptConference.DestroyConferenceClipboard();

                clipHouse.SetActive(true);
                clipHillfort.SetActive(false);
                clip_CR.SetActive(false);
            }
        }

        public void HillfortTrigger() {

            if (!clipHillfort.activeSelf) {

                handoverScriptHaus.DestroyHausClipboard();
                handoverScriptConference.DestroyConferenceClipboard();

                clipHouse.SetActive(false);
                clipHillfort.SetActive(true);
                clip_CR.SetActive(false);
            }
        }

        public void ConferenceTrigger() {

            if (!clip_CR.activeSelf) {

                handoverScriptHaus.DestroyHausClipboard();
                handoverScriptHillfort.DestroyHillfortClipboard();

                clipHouse.SetActive(false);
                clipHillfort.SetActive(false);
                clip_CR.SetActive(true);
            }
        }
    }
}
