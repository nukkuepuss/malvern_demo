/// <summary>
/// hold hand up to burners and push thumbstick forward for a burn.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class BalloonRide : MonoBehaviour {

        public GameObject player;

        public AudioSource clip;
        public GameObject burn;

        private GameObject _rightHand;

        private Vector3 _movement;

        public float speed;

        [Header("Handover script")]
        public TutorialManager tutorialManager;

        [Tooltip("Lower the value for movement along unwanted axis to register")]
        [Range(0, 1)]
        public float stickTolerance;

        [HideInInspector]
        public Vector2 _rightStickInput;

        private bool _burnCol;
        private bool _isPlaying = false;

        private void Start() {

            burn.SetActive(false);

            // get a reference to the hand/controller used for forward direction
            // TODO expose string for left-handed users
            _rightHand = GameObject.FindGameObjectWithTag("RightHand");
        }

        private void Update() {

            _rightStickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

            if (_burnCol && (_rightStickInput.y > 0 && _rightStickInput.x < stickTolerance)) {

                if (!_isPlaying) {

                    burn.SetActive(true);
                    tutorialManager.SetBurntFlag();
                    clip.Play();
                    _isPlaying = true;

                    // move forward/backward relative to right hand's position
                    _movement = (_rightHand.transform.forward * speed * _rightStickInput.y * Time.deltaTime);
                    player.transform.position += _movement;
                }
            }
            else {

                burn.SetActive(false);
                clip.Stop();
                _isPlaying = false;
            }
        }

        private void OnTriggerEnter(Collider other) {

            _burnCol = true;
        }

        private void OnTriggerExit(Collider other) {

            _burnCol = false;
        }
    }
}