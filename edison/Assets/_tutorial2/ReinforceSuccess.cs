/// <summary>
/// ReinforceSuccess.cs
/// provides haptics and aural feedback on successful waypoint navigation
/// moved to seperate script as waypoint is destroyed on enter
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class ReinforceSuccess : MonoBehaviour {

        public AudioSource clip;

        [Range(0, 1)]
        public float hapticFrequency;
        [Range(0, 1)]
        public float hapticAmplitude;

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
