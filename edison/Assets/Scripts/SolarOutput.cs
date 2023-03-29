/// <summary>
/// having the solar clipboard open makes a KWh value appear on the wristband that changes with the sun's position
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.jonrummery.edison {

    public class SolarOutput : MonoBehaviour {

        public GameObject sun;
        public Text display;

        void Start() {
        }

        void Update() {

            display.text = sun.GetComponent<Transform>().position.x.ToString();



            //GetSunValue();

        }

        //void GetSunValue() {


        //}
    }
}
