/// <summary>
/// keep the mini models base above wristband
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class ConstrainBase : MonoBehaviour {

        public GameObject plinth;

        private Vector3 _originalPosition;
        private Quaternion _originalRotation, _target;

        

        void Start() {

            _originalPosition = this.gameObject.transform.position;
            _originalRotation = this.gameObject.transform.rotation;
        }

        void LateUpdate() {

           // transform.position = new Vector3 ()

            transform.position = plinth.transform.position;
            
            
            //transform.position = plinth.transform.up;

            //_target.eulerAngles.lo

            //_target.eulerAngles = new Vector3(0f, bracer.transform.position.x, 0f);

            //_now = transform.eulerAngles.;

            //_target.eulerAngles = new Vector3(_OriginalRotation.y, 0f, 0f);

            //transform.eulerAngles += _target.eulerAngles;


            //transform.eulerAngles = new Vector3(bracer.transform.position.x, bracer.transform.position.y, bracer.transform.position.z);

            //Quaternion _newRotation = new Vector3 (bracer.transform.position.y, 0f, bracer.transform.position.z);

            //transform.Rotate(_newRotation);

            //transform.eulerAngles.x = _OriginalRotation.x;

            //transform.Rotate(new Vector3(_OriginalRotation.y, 0, 0));

            //transform.rotation = Quaternion.Slerp(transform.rotation, _OriginalRotation, Time.deltaTime);



            //this.gameObject.transform.rotation.y = rot.y;

            //this.gameObject.transform.rotation.x = rot.x;
        }
    }
}
