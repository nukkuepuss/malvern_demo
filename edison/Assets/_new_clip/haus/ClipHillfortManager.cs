/// <summary>
/// ClipHillfortManager.cs
/// handles the Hillfort clipboard; currently no buttons to deal with
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class ClipHillfortManager : MonoBehaviour {

        public GameObject thisClipboard;
        public GameObject thisModel;

        private void OnEnable() {

            thisClipboard.SetActive(true);
            thisModel.SetActive(true);
        }

        public void DestroyHillfortClipboard() {

            MySharedData.lastActiveClipboard = MySharedData.hausClipboards.hillfort;

            thisClipboard.SetActive(false);
            thisModel.SetActive(false);
        }
    }
}
