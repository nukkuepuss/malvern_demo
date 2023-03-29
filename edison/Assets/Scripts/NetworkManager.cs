//
//This script connects to PHOTON servers
//player creates a room if there is none, then joins
//There's two buttons, b0:connect and b1:join_room
//

using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.Collections;

namespace com.jonrummery.edison {

    public class NetworkManager : MonoBehaviourPunCallbacks {

        public int nextLevel;

        public LobbyTicks lobbyScript;

        public Texture2D b0_on_pic;
        public Texture2D b1_on_pic;

        public Texture2D b0_off_pic;
        public Texture2D b1_off_pic;

        public int b0_picture;
        public int b1_picture;

        public int b0_frame;
        public int b1_frame;

        public Camera cam;

        private Material[] _mats;

        bool triesToConnectToMaster = false;
        bool triesToConnectToRoom = false;
        bool b0IsOn = false;

        private void Start() {

            _mats = GetComponentInChildren<MeshRenderer>().materials;

            DeactivateB0();
            DeactivateB1();
        }

        public void B0Trigger() {

            if (b0IsOn) {

                // go from connected state to disconnected
                b0IsOn = false;
                DeactivateB0();
                PhotonNetwork.Disconnect();
            }
            else {

                //go from disconnected to connected
                if (!PhotonNetwork.IsConnected && !triesToConnectToMaster) {
                    ConnectToMaster();
                }
                if (PhotonNetwork.IsConnected && !triesToConnectToMaster && !triesToConnectToRoom) {
                    StartCoroutine(WaitFrameAndConnect());
                }
            }
        }

        public void B1Trigger() {

            if (b0IsOn) {

                // we're already connected, so join room
                ConnectToRoom();
            }
            else {

                // we're not connected, so can't join a room
                DeactivateB1();
            }
        }

        public void ActivateB0() {

            _mats[b0_picture].SetTexture("_MainTex", b0_on_pic);
            _mats[b0_frame].color = Color.red;
        }

        public void DeactivateB0() {

            _mats[b0_picture].SetTexture("_MainTex", b0_off_pic);
            _mats[b0_frame].color = Color.black;
        }

        public void ActivateB1() {

            _mats[b1_picture].SetTexture("_MainTex", b1_on_pic);
            _mats[b1_frame].color = Color.red;
        }

        public void DeactivateB1() {

            _mats[b1_picture].SetTexture("_MainTex", b1_off_pic);
            _mats[b1_frame].color = Color.black;
        }

        public void ConnectToMaster() {

            PhotonNetwork.OfflineMode = false; //true would "fake" an online connection
            PhotonNetwork.NickName = "PlayerName"; //we can use a input to change this 
            PhotonNetwork.AutomaticallySyncScene = true; //To call PhotonNetwork.LoadLevel()
            PhotonNetwork.GameVersion = "v1"; //only people with the same game version can play together

            triesToConnectToMaster = true;
            //PhotonNetwork.ConnectToMaster(ip, port, appid); //manual connection
            PhotonNetwork.ConnectUsingSettings(); //automatic connection based on the config file
        }

        public override void OnDisconnected(DisconnectCause cause) {

            base.OnDisconnected(cause);

            // update lobby clipboard tick
            lobbyScript.LostConnection();

            triesToConnectToMaster = false;
            triesToConnectToRoom = false;
            DeactivateB0();
            Debug.Log(cause);
        }

        public override void OnConnectedToMaster() {

            triesToConnectToMaster = false;
            b0IsOn = true;
            ActivateB0();

            Debug.Log("Connected to master!");
        }

        IEnumerator WaitFrameAndConnect() {

            triesToConnectToRoom = true;
            yield return new WaitForEndOfFrame();
            Debug.Log("Connecting");
            ConnectToRoom();
        }

        public void ConnectToRoom() {

            if (!PhotonNetwork.IsConnected)
                return;

            triesToConnectToRoom = true;
            //PhotonNetwork.CreateRoom("name"); //Create a specific room - Callback OnCreateRoomFailed
            //PhotonNetwork.JoinRoom("name"); //Join a specific room - Callback OnJoinRoomFailed

            PhotonNetwork.JoinRandomRoom(); // Join a random room - Callback OnJoinRandomRoomFailed
        }

        public override void OnJoinedRoom() {

            ActivateB1();

            // update lobby clipboard tick
            lobbyScript.CheckConnection();

            // hide all objects (Unity makes the Oculus display all janky when changing scenes)
            cam.cullingMask = (1 << LayerMask.NameToLayer("Nothing"));

            Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | RoomName: " + PhotonNetwork.CurrentRoom.Name + " Region: " + PhotonNetwork.CloudRegion);

            //load main scene (see Build Settings)
            SceneManager.LoadScene(nextLevel);
        }

        public override void OnJoinRandomFailed(short returnCode, string message) {

            base.OnJoinRandomFailed(returnCode, message);

            DeactivateB1();

            //no room available
            //create a room (null as a name means "does not matter")
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 5 });
        }
    }
}