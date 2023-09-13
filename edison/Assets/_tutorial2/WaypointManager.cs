/// <summary>
/// WaypointManager.cs
/// instantiate waypoints and show helpful information to get the Player through the first (movement) part of the tutorial
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class WaypointManager : MonoBehaviour {

        public GameObject waypoint;
        public Transform[] markers;
        public GameObject[] arcs;
        public GameObject[] arrows;
        public GameObject[] boxes;
        public GameObject leftHand, rightHand;
        public Transform boxOffsetLeft, boxOffsetRight;

        //[HideInInspector]
        //public Material arrowMat;

        private GameObject _waypoint;
        private int _nextWaypoint;
        private int _currentWaypoint;

        public AudioSource clip;

        [Range(0, 1)]
        public float hapticFrequency;
        [Range(0, 1)]
        public float hapticAmplitude;

        private void Start() {

            ShowWaypoint();
        }

        private void Update() {

            if (_currentWaypoint!=MySharedData.nextWaypoint) {

                RemoveWaypoint();
                ShowWaypoint();
                ProvideReinforcement();
            }
        }

        void ShowWaypoint() {

            _nextWaypoint = MySharedData.nextWaypoint;

            _currentWaypoint = _nextWaypoint;

            // we don't have to Instantiate() here, but it makes a nice change from SetActive()
            _waypoint = Instantiate(waypoint, markers[_currentWaypoint].transform.position, markers[_currentWaypoint].transform.rotation);

            arcs[_currentWaypoint].SetActive(true);

            arrows[_currentWaypoint].SetActive(true);

            // activate text box...
            boxes[_currentWaypoint].SetActive(true);

            // ... and parent to a hand (with an offset)
            if (boxes[_currentWaypoint].GetComponent<BoxHand>().whichHand==BoxHand.handChoice.left) {

                boxes[_currentWaypoint].transform.position = boxOffsetLeft.transform.position;
                boxes[_currentWaypoint].transform.rotation = boxOffsetLeft.transform.rotation;
                boxes[_currentWaypoint].transform.localScale = boxOffsetLeft.transform.localScale;

                boxes[_currentWaypoint].transform.SetParent(leftHand.transform, false);
            }
            else {

                boxes[_currentWaypoint].transform.position = boxOffsetRight.transform.position;
                boxes[_currentWaypoint].transform.rotation = boxOffsetRight.transform.rotation;
                boxes[_currentWaypoint].transform.localScale = boxOffsetRight.transform.localScale;

                boxes[_currentWaypoint].transform.SetParent(rightHand.transform, false);
            }
        }

        void RemoveWaypoint() {

            Destroy(_waypoint);

            arcs[_currentWaypoint].SetActive(false);

            boxes[_currentWaypoint].SetActive(false);
        }

        public void ProvideReinforcement() {

            StartCoroutine("HapticFeedback");

            clip.Play();
        }

        IEnumerator HapticFeedback() {

            // set haptics on and off
            OVRInput.SetControllerVibration(hapticFrequency, hapticAmplitude, OVRInput.Controller.LTouch);
            yield return new WaitForSeconds(0.1f);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
    }
}
