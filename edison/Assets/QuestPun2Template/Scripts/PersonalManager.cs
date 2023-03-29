using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//
//For handling local objects and sending data over the network
//
namespace com.jonrummery.edison {

    public class PersonalManager : MonoBehaviourPunCallbacks {

        [SerializeField] GameObject headPrefab;
        [SerializeField] GameObject handRPrefab;
        [SerializeField] GameObject handLPrefab;
        [SerializeField] GameObject ovrCameraRig;
        [SerializeField] Transform[] spawnPoints;

        private void Awake() {

            // set 90hz for quest2
            //     OVRManager.display.displayFrequency = 90f;


            // If the game starts in Room scene, and is not connected, sends the player back to Lobby scene to connect first.
            if (!PhotonNetwork.NetworkingClient.IsConnected) {

                SceneManager.LoadScene("newLobby");
                return;
            }

            if (PhotonNetwork.LocalPlayer.ActorNumber <= spawnPoints.Length) {

                ovrCameraRig.transform.position = spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.position;
                ovrCameraRig.transform.rotation = spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.rotation;
            }
        }

        private void Start() {

            //Instantiate Head
            GameObject obj = (PhotonNetwork.Instantiate(headPrefab.name, OculusPlayer.instance.head.transform.position, OculusPlayer.instance.head.transform.rotation, 0));
            obj.GetComponent<SetColor>().SetColorRPC(PhotonNetwork.LocalPlayer.ActorNumber);
            Debug.Log("Instantiated head : " + obj.name);

            //Instantiate right hand
            obj = (PhotonNetwork.Instantiate(handRPrefab.name, OculusPlayer.instance.rightHand.transform.position, OculusPlayer.instance.rightHand.transform.rotation, 0));
            Debug.Log("Instantiated RHand : " + obj.name + " with tag :" + obj.tag);

            //Instantiate left hand
            obj = (PhotonNetwork.Instantiate(handLPrefab.name, OculusPlayer.instance.leftHand.transform.position, OculusPlayer.instance.leftHand.transform.rotation, 0));
        }

        //If disconnected from server, returns to Lobby to reconnect
        public override void OnDisconnected(DisconnectCause cause) {

            base.OnDisconnected(cause);
            SceneManager.LoadScene("newLobby");
        }

        //So we stop loading scenes if we quit app
        private void OnApplicationQuit() {

            StopAllCoroutines();
        }
    }
}
