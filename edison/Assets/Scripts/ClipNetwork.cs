/// <summary>
/// networking clipboard logic
/// button presses call functions in this script
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

namespace com.jonrummery.edison {

    public class ClipNetwork : MonoBehaviourPunCallbacks {

        //public PersonalManager networkScript;

        [Header("Connect button picture model")]
        public GameObject connectButtonPic;
        private Material _connectButtonPicMat;

        [Header("Join button picture model")]
        public GameObject joinButtonPic;
        private Material _joinButtonPicMat;

        [Header("Materials with pics for buttons")]
        public Texture2D connectPic;
        public Texture2D disconnectPic;
        public Texture2D joinPic;
        public Texture2D leavePic;

        [Header("Ticks (from Hierarchy)")]
        public GameObject connectedTick;
        public GameObject joinedTick;

        [Header("Text slots (from Hierarchy)")]
        public Text roomNameText;
        public Text playerCountText;

        // internal flags for PUN master network connection state
        private bool _connected;
        private bool _joined;
        bool triesToConnectToMaster = false;
        bool triesToConnectToRoom = false;

        private void Start() {

            // grab the material element to change the button picture
            _connectButtonPicMat = connectButtonPic.GetComponent<MeshRenderer>().material;
            _joinButtonPicMat = joinButtonPic.GetComponent<MeshRenderer>().material;

            // set an initial state
            UpdateClipboard();
        }

        public void UpdateClipboard() {

            UpdateButtons();
            UpdateTicks();
            UpdateText();
        }

        // called from long-pressing the connect/disconnect button
        public void ConnectTrigger() {

            if (PhotonNetwork.IsConnected) {

                // connected to disconnected
                PhotonNetwork.Disconnect();
            }
            else {

                // disconnected to connected
                if (!PhotonNetwork.IsConnected && !triesToConnectToMaster) {
                    ConnectToMaster();
                }
                if (PhotonNetwork.IsConnected && !triesToConnectToMaster && !triesToConnectToRoom) {
                    StartCoroutine(WaitFrameAndConnect());
                }
            }
        }

        // called from long-pressing the join/leave room button
        public void JoinTrigger() {

            if ((PhotonNetwork.IsConnected) && (!PhotonNetwork.InRoom)) {

                ConnectToRoom();
            }
            else {

                PhotonNetwork.LeaveRoom();
            }

            UpdateClipboard();
        }

        void UpdateButtons() {

            _connectButtonPicMat.mainTexture = PhotonNetwork.IsConnected == true ? disconnectPic : connectPic;
            _joinButtonPicMat.mainTexture = PhotonNetwork.InRoom == true ? leavePic : joinPic;
        }

        void UpdateTicks() {

            // grab the states
            _connected = PhotonNetwork.IsConnected;
            _joined = PhotonNetwork.InRoom;

            // how do I make these ternary operators?
            if (_connected) {

                connectedTick.SetActive(true);
            }
            else {
                connectedTick.SetActive(false);
            }

            if (_joined) {

                joinedTick.SetActive(true);
            }
            else {

                joinedTick.SetActive(false);
            }
        }

        void UpdateText() {

            if (PhotonNetwork.InRoom) {

                roomNameText.text = PhotonNetwork.CurrentRoom.Name;
                playerCountText.text = PhotonNetwork.CountOfPlayersInRooms.ToString();
            }
            else {
                roomNameText.text = "not in a room";
                playerCountText.text = "null";
            }
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

        public override void OnConnectedToMaster() {

            triesToConnectToMaster = false;

            UpdateClipboard();
        }

        public override void OnDisconnected(DisconnectCause cause) {

            triesToConnectToMaster = false;
            triesToConnectToRoom = false;

            UpdateClipboard();
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

            UpdateClipboard();

            //networkScript.InstantiatePlayer();

            Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | RoomName: " + PhotonNetwork.CurrentRoom.Name + " Region: " + PhotonNetwork.CloudRegion);
        }

        public override void OnJoinRandomFailed(short returnCode, string message) {

            //create a room
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 5 });
        }
    }
}
