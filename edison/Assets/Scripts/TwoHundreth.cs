/// <summary>
/// TwoHundreth.cs
/// Creates Exhibition2 by showing 200th miniatures
/// which mirror the state of the main models
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class TwoHundreth : MonoBehaviour {

        public GameObject world, buildings, ways;
        public GameObject p1, p2, p3, p4;

        public GameObject world200, worldStub, buildings200, ways200;
        public GameObject p1mini, p2mini, p3mini, p4mini;

        void Update() {

            world200.SetActive(world.activeSelf || worldStub.activeSelf);
            buildings200.SetActive(buildings.activeSelf);
            ways200.SetActive(ways.activeSelf);
            p1mini.SetActive(p1.activeSelf);
            p2mini.SetActive(p2.activeSelf);
            p3mini.SetActive(p3.activeSelf);
            p4mini.SetActive(p4.activeSelf);
        }
    }
}
