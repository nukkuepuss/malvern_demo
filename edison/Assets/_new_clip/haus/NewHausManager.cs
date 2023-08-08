/// <summary>
/// NewHausManager.cs
/// the haus selector is always visible when the house clipboard is activated
/// there's three choices, each displaying a different clipboard, plus a null clipboard
/// there's three colliders that trigger functions here
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class NewHausManager : MonoBehaviour {

        public GameObject clipHouse, clipHillfort, clipCR, clipNone, clipMenu;
        public int menuHouseMatPos, menuHillfortMatPos, menuCRMatPos;

        public NewConferenceManager handoverConferenceScript;
        public NewHouseManager handoverHouseScript;

        public GameObject hillfortModel, conferenceCentreModel;

        private Material[] _menuMats;

        void Start() {

            // grab the menu materials so that we can change the background color to show selection
            _menuMats = clipMenu.GetComponent<MeshRenderer>().materials;

            // the Conference Centre should be active to start
            DeactivateHouse();
            DeactivateHillfort();
            DeactivateNone();
            ActivateCR();
        }

        public void NewHouseTrigger() {

            // Off to On
            if (!clipHouse.activeSelf) {

                ActivateHouse();
            }

            else {

                DeactivateHouse();
                ActivateNone();
            }
        }

        public void NewHillfortTrigger() {

            // Off to On
            if (!clipHillfort.activeSelf) {

                ActivateHillfort();
            }

            else {

                DeactivateHillfort();
                ActivateNone();
            }
        }

        public void NewCRTrigger() {

            // Off to On
            if (!clipCR.activeSelf) {

                ActivateCR();
            }

            else {

                DeactivateCR();
                ActivateNone();
            }
        }

        void ActivateHouse() {

            clipHouse.SetActive(true);

            DeactivateHillfort();
            DeactivateCR();
            DeactivateNone();

            _menuMats[menuHouseMatPos].color = Color.green;
        }

        void DeactivateHouse() {

            handoverHouseScript.DestroyHausClipboard();

            _menuMats[menuHouseMatPos].color = Color.grey;
        }

        void ActivateHillfort() {

            clipHillfort.SetActive(true);

            DeactivateHouse();
            DeactivateCR();
            DeactivateNone();

            _menuMats[menuHillfortMatPos].color = Color.green;
        }

        void DeactivateHillfort() {

            clipHillfort.SetActive(false);

            hillfortModel.SetActive(false);

            _menuMats[menuHillfortMatPos].color = Color.grey;
        }

        void ActivateCR() {

            clipCR.SetActive(true);

            DeactivateHouse();
            DeactivateHillfort();
            DeactivateNone();

            conferenceCentreModel.SetActive(true);

            _menuMats[menuCRMatPos].color = Color.green;

        }

        void DeactivateCR() {

            handoverConferenceScript.DestroyConferenceClipboard();

            conferenceCentreModel.SetActive(false);

            _menuMats[menuCRMatPos].color = Color.grey;
        }

        void ActivateNone() {
            
            clipNone.SetActive(true);
        }

        void DeactivateNone() {

            clipNone.SetActive(false);
        }
    }
}
