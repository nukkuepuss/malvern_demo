/// <summary>
/// ControllerManager.cs
/// checks that both L and R controllers are active
/// (the right controller doesn't register correctly if the left controller isn't active)
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class ControllerManager : MonoBehaviour {

        public GameObject controllerBanner;

        private bool _isDisplaying;

        private void Update() {
            
            if (OVRInput.GetActiveController() != OVRInput.Controller.Touch) {

                controllerBanner.SetActive(true);
                _isDisplaying = true;
            }

            if (_isDisplaying) {

                if (OVRInput.GetActiveController()==OVRInput.Controller.Touch) {

                    controllerBanner.SetActive(false);
                    _isDisplaying = false;
                }
            }
        }
    }
}
