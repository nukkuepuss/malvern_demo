/// <summary>
/// MyGrabber.cs
/// pick up and drop an object
/// *** pickups must be on 'pickup' layer and have rb+collider
/// local only (for now)
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class MyGrabber : MonoBehaviour {

        public LayerMask layerMask;
        public OVRInput.Button triggerGrab;
        public OVRInput.Controller controller;
        public float sphereRadius;
        [HideInInspector]
        public bool _isGrabbing;
        public MyGrabber otherHand;
        public Transform trackingSpace;

        private bool _isOtherHandGrabbing, _isHoldingTrigger;
        private GameObject _tempObject, _grabbedObject;

        private GameObject parent;

        void OnDrawGizmosSelected() {

            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, sphereRadius);
        }

        private void Update() {

            // check to see if this hand's grab button is held down
            _isHoldingTrigger = OVRInput.Get(triggerGrab);

            if (_isHoldingTrigger) {

                // return the nearest pickupable object (or null)
                _tempObject = WhatsNearest();

                // check to see if we're already in a grab state with the other hand
                _isOtherHandGrabbing = otherHand._isGrabbing;

                // if we're NOT (other hand's grabbing + this trigger held down + both hands want the same object)
                if (!(_isOtherHandGrabbing && _isHoldingTrigger && _tempObject == otherHand._grabbedObject)) {

                    // GRABBING
                    // (i) not already in a grab state, (ii) an object is in range and (iii) we're holding the grab trigger
                    if (!_isGrabbing && _tempObject != null && _isHoldingTrigger) {

                        GrabObject(_tempObject);
                    }
                }
            }
            else {

                // we're not holding down the grab trigger, so drop if we're grabbing
                if (_isGrabbing) {

                    DropObject();
                }
            }
        }

        void GrabObject(GameObject _sentObject) {

            // set a flag
            _isGrabbing = true;

            // get a reference to the object that's being grabbed
            _grabbedObject = _sentObject;

            // remember the parent of the object
            parent = _grabbedObject.transform.parent.gameObject;

            // change the object to kinematic (no physics whilst grabbing)
            _grabbedObject.GetComponent<Rigidbody>().isKinematic = true;

            // the object to grab comes under the control of the GameObject this script is attached to (ie hand)
            _grabbedObject.transform.parent = transform;
        }

        void DropObject() {

            if (_grabbedObject != null) {

                // set a flag
                _isGrabbing = false;

                // parent to original object
                _grabbedObject.transform.parent = parent.transform;

                // restore physics
                _grabbedObject.GetComponent<Rigidbody>().isKinematic = false;

                // give it some momentum based on the hand's movement
                _grabbedObject.GetComponent<Rigidbody>().velocity = (trackingSpace.rotation * OVRInput.GetLocalControllerVelocity(controller));

                // give it some rotation based on the hand's rotation
                _grabbedObject.GetComponent<Rigidbody>().angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller);

                // clear the reference
                _grabbedObject = null;
            }
        }

        public GameObject WhatsNearest() {

            // grab all the pickup objects in range (with a specified layermask)
            RaycastHit[] selection = Physics.SphereCastAll(transform.position, sphereRadius, transform.forward, 0f, layerMask);

            // whittle them down to the nearest pickupable object (or null)
            if (selection.Length != 0) {

                int _closest = 0;

                for (int i = 0; i < selection.Length; i++) {

                    if (selection[i].distance < selection[_closest].distance) {

                        _closest = i;
                    }
                }

                return selection[_closest].transform.gameObject;
            }
            else {

                return null;
            }
        }
    }
}
