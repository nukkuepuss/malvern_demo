/// <summary>
/// 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace com.jonrummery.edison {

    public class NetworkDisconnect : MonoBehaviourPunCallbacks {

        public Texture2D b0OnPic;
 //       public Texture2D b0OffPic;
        public int b0Element;
        public int b0Frame;

        public Camera cam;

        private Material[] _mats;

        private void Start() {

            // store button materials
            _mats = GetComponentInChildren<MeshRenderer>().materials;

            // set initial button pic and frame
            _mats[b0Element].SetTexture("_MainTex", b0OnPic);
            _mats[b0Frame].color=Color.red;
        }

        public void B0Trigger() {

            PhotonNetwork.Disconnect();

            // hide all objects (Unity makes the Oculus display all janky when changing scenes)
            cam.cullingMask = (1 << LayerMask.NameToLayer("Nothing"));

            SceneManager.LoadScene(0); //go to the lobby

            //_mats[b0Element].SetTexture("_MainTex", b0OffPic);
            //_mats[b0Frame].color = Color.black;

        }
    }
}
