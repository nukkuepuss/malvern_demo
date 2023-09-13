/// <summary>
/// MyNetworkScript.cs
/// connects to PHOTON servers and creates a room if there is none, then automatically joins
/// adapted from Photon PUN example script
/// </summary>
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.Collections;

namespace com.jonrummery.edison {

    public class MyNetworkScript : MonoBehaviourPunCallbacks {

        public int malvernBuildIndex;
        public FadeScreen fadeScreen;

        bool _triesToConnectToMaster = false;
        bool _triesToConnectToRoom = false;

        bool _hasClickedToStart = false;

        private void Update() {

            if (_hasClickedToStart) {

                if (!PhotonNetwork.IsConnected && !_triesToConnectToMaster) {

                    ConnectToMaster();
                }

                if (PhotonNetwork.IsConnected && !_triesToConnectToMaster && !_triesToConnectToRoom) {

                    StartCoroutine(WaitFrameAndConnect());
                }
            }
        }

        public void StartNetworking() {

            _hasClickedToStart = true;
        }

        public void ConnectToMaster() {

            PhotonNetwork.OfflineMode = true; //true would "fake" an online connection
            PhotonNetwork.NickName = "Nukkue"; //we can use a input to change this 
            PhotonNetwork.AutomaticallySyncScene = true; //To call PhotonNetwork.LoadLevel()
            PhotonNetwork.GameVersion = "v2"; //only people with the same game version can play together

            _triesToConnectToMaster = true;

            //PhotonNetwork.ConnectToMaster(ip, port, appid); //manual connection

            PhotonNetwork.ConnectUsingSettings(); //automatic connection based on the config file
        }

        public override void OnDisconnected(DisconnectCause cause) {

            _triesToConnectToMaster = false;
            _triesToConnectToRoom = false;
            Debug.Log(cause);
        }

        public override void OnConnectedToMaster() {

            _triesToConnectToMaster = false;
            Debug.Log("Connected to master!");
        }

        IEnumerator WaitFrameAndConnect() {

            _triesToConnectToRoom = true;
            yield return new WaitForEndOfFrame();
            Debug.Log("Connecting");
            ConnectToRoom();
        }

        public void ConnectToRoom() {

            if (!PhotonNetwork.IsConnected)
                return;

            _triesToConnectToRoom = true;
            //PhotonNetwork.CreateRoom("name"); //Create a specific room - Callback OnCreateRoomFailed
            //PhotonNetwork.JoinRoom("name"); //Join a specific room - Callback OnJoinRoomFailed

            PhotonNetwork.JoinRandomRoom(); // Join a random room - Callback OnJoinRandomRoomFailed
        }

        public override void OnJoinedRoom() {

            //Go to next scene after joining the room
            base.OnJoinedRoom();
            Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | RoomName: " + PhotonNetwork.CurrentRoom.Name + " Region: " + PhotonNetwork.CloudRegion);

            FadeToScene();

            //SceneManager.LoadScene(nextLevel); // <<-- Guns. Lots of guns.
        }

        public override void OnJoinRandomFailed(short returnCode, string message) {

            base.OnJoinRandomFailed(returnCode, message);
            //no room available
            //create a room (null as a name means "does not matter")
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 5 });
        }

        public void FadeToScene() {

            StartCoroutine(FadeToSceneRoutine());
        }

        IEnumerator FadeToSceneRoutine() {

            fadeScreen.FadeOut();

            yield return new WaitForSeconds(fadeScreen.fadeDuration);

            SceneManager.LoadScene(malvernBuildIndex);
        }
    }
}