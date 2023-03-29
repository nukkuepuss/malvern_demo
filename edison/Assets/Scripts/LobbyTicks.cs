/// <summary>
/// sort of a tutorial, but less so
/// place ticks on the lobby clipboard when associated actions performed -> next level
/// this forces player to perform tapping and join the network
/// hopefully some questions about networking will arise
/// maybe they'll investigate further...
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class LobbyTicks : MonoBehaviour {

        public GameObject tick1, tick2, tick3, tick4;

        [HideInInspector]
        public bool _hasConnected;
        [HideInInspector]
        public bool _hasPressedStart;

        private bool _isTouching;

        private void Start() {
            

        }

        void Update() {

            // required
            OVRInput.Update();

            // first tick is for holding down the right-hand-middle trigger
            // **** there's a delay here with the tick appearing **** TODO
            tick1.SetActive((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger)) == 1);

            // place second tick if we're not touching the right-hand-index trigger (ie. we're pointing)
            _isTouching = (OVRInput.Get(OVRInput.NearTouch.SecondaryIndexTrigger));
            tick2.SetActive(!_isTouching);

            // functions called by NetworkManager when we've joined a room or disconnected
            tick3.SetActive(_hasConnected);
            tick4.SetActive(_hasPressedStart);
        }

        public void CheckConnection() {

            _hasConnected = true;
        }

        public void LostConnection() {

            _hasConnected = false;
        }
    }
}
