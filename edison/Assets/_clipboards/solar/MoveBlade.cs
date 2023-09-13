/// <summary>
/// MoveBlade.cs
/// attach to a blade with rb
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class MoveBlade : MonoBehaviour {

        private float _speed;
        private float _randomFactor;
        private Rigidbody _rb;

        private void Awake() {

            _rb = GetComponent<Rigidbody>();

            // calculate a random factor so that the turbines aren't all spinning at the same speed
            _randomFactor = Random.Range(0.7f, 1.6f);

            Debug.Log("Random = " + _randomFactor);
        }

        private void Update() {

            // calculate speed based on wind speed slider
            _speed = ((MySharedData.windSliderPos / 2) * _randomFactor);

            // move the 'x' value due to Blender import nonsense
            _rb.angularVelocity = new Vector3(-_speed, 0f, 0f);
        }
    }
}
