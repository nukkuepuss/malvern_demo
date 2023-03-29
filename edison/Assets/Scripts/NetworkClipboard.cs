/// <summary>
/// functions for button presses
/// there's two buttons : connect/disconnect master network and join/leave room
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

namespace com.jonrummery.edison {

    public class NetworkClipboard : MonoBehaviourPunCallbacks {

        [Header("Green tick (in Hierarchy)")]
        public GameObject tick;

        // photon master network connection state
        private bool _isConnected;

        // some flags
        private bool _triesToConnectToMaster;
        private bool _triesToConnectToRoom;

        private void Awake() {

            // find out if we're connected and initialize
            _isConnected = PhotonNetwork.IsConnected;

            //// initialize
            //ConnectTrigger();

            Debug.Log("state : " + _isConnected.ToString());
        }

        public void ConnectTrigger() {

            if (_isConnected) {

                // going from connected to diconnected
                PhotonNetwork.Disconnect();

            }
            else {

                // going from diconnected to connected
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public void ConnectToMaster() {

            PhotonNetwork.OfflineMode = false; //true would "fake" an online connection
            PhotonNetwork.NickName = "PlayerName"; //we can use a input to change this 
            PhotonNetwork.AutomaticallySyncScene = true; //To call PhotonNetwork.LoadLevel()
            PhotonNetwork.GameVersion = "v1"; //only people with the same game version can play together

            _triesToConnectToMaster = true;

            //PhotonNetwork.ConnectToMaster(ip, port, appid); //manual connection
            PhotonNetwork.ConnectUsingSettings(); //automatic connection based on the config file
        }

        public override void OnDisconnected(DisconnectCause cause) {

            // update lobby clipboard tick
            tick.SetActive(false);

            _triesToConnectToMaster = false;
            _triesToConnectToRoom = false;
            Debug.Log(cause);
        }

        public override void OnConnectedToMaster() {

            Debug.Log("Connected to master!");

            // update lobby clipboard tick
            tick.SetActive(true);

            _triesToConnectToMaster = false;
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

            Debug.Log("NetworkClipboard : " + PhotonNetwork.IsMasterClient + " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | RoomName: " + PhotonNetwork.CurrentRoom.Name + " Region: " + PhotonNetwork.CloudRegion);

            //// hide all objects (Unity makes the Oculus display all janky when changing scenes)
            //cam.cullingMask = (1 << LayerMask.NameToLayer("Nothing"));

            ////load main scene (see Build Settings)
            //SceneManager.LoadScene(nextLevel);
        }

        public override void OnJoinRandomFailed(short returnCode, string message) {

            //no room available
            //create a room (null as a name means "does not matter")
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 5 });
        }
    }
}
