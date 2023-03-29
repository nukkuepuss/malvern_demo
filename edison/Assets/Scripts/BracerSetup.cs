/// <summary>
/// handles the main b1-b5 buttons and clipboards
/// 
/// 1 network
/// 2 settings
/// 3 main map
/// 4 house
/// 5 solar
/// make bracer mesh the first mesh in hierarchy (so that they're the first materials for GetComponmentInChildren to find)
/// TODO : elegantly loop the b1.2.3.4.5 functions using only 3 lines of code
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class BracerSetup : MonoBehaviour {

        //[Tooltip("Attach bracer to which hand?")]
        //public GameObject anchor;
        //[Tooltip("Fine-tune position")]
        //public Transform offset;
        //public GameObject bracerMesh;

        [Header("button 1-5 'on' pics")]
        public Texture2D b1OnPic;
        public Texture2D b2OnPic;
        public Texture2D b3OnPic;
        public Texture2D b4OnPic;
        public Texture2D b5OnPic;

        [Header("button 1-5 'off' pics")]
        public Texture2D b1OffPic;
        public Texture2D b2OffPic;
        public Texture2D b3OffPic;
        public Texture2D b4OffPic;
        public Texture2D b5OffPic;

        [Header("Which bracer mesh element corresponds to the pictures?")]
        public int b1PicElement;
        public int b2PicElement;
        public int b3PicElement;
        public int b4PicElement;
        public int b5PicElement;

        [Header("Which bracer mesh element corresponds to the frames?")]
        public int b1FrameElement;
        public int b2FrameElement;
        public int b3FrameElement;
        public int b4FrameElement;
        public int b5FrameElement;

        [Header("Clipboards")]
        public GameObject clipNetwork;
        public GameObject clipSettings;
        public GameObject clipMain;
        public GameObject clipHouse;
        public GameObject clipSolar;

        private Material[] _mats;

        //private void Awake() {

        //    // parent the bracer to a hand
        //    this.transform.SetParent(anchor.transform);

        //    // adjust bracer position relative to hand
        //    this.transform.position = new Vector3(offset.transform.position.x,
        //                                            offset.transform.position.y,
        //                                            offset.transform.position.z);
        //    this.transform.rotation = offset.transform.rotation;
        //}

        private void Start() {

            _mats = GetComponentInChildren<MeshRenderer>().materials;

            DeactivateB1();
            DeactivateB2();
            DeactivateB3();
            DeactivateB4();
            DeactivateB5();
        }

        public void B1Trigger() {

            // is the button already active? ie, is the clipboard active?
            if (clipNetwork.activeSelf) {
                DeactivateB1();
            }
            else {
                ActivateB1();
                DeactivateB2();
                DeactivateB3();
                DeactivateB4();
                DeactivateB5();
            }
        }

        public void B2Trigger() {

            // is the button already active? ie, is the clipboard active?
            if (clipSettings.activeSelf) {
                DeactivateB2();
            }
            else {
                ActivateB2();
                DeactivateB1();
                DeactivateB3();
                DeactivateB4();
                DeactivateB5();
            }
        }

        public void B3Trigger() {

            // is the button already active? ie, is the clipboard active?
            if (clipMain.activeSelf) {
                DeactivateB3();
            } else {
                ActivateB3();
                DeactivateB1();
                DeactivateB2();
                DeactivateB4();
                DeactivateB5();
            }
        }

        public void B4Trigger() {

            // is the button already active? ie, is the clipboard active?
            if (clipHouse.activeSelf) {
                DeactivateB4();
            }
            else {
                ActivateB4();
                DeactivateB1();
                DeactivateB2();
                DeactivateB3();
                DeactivateB5();
            }

        }

        public void B5Trigger() {

            // is the button already active? ie, is the clipboard active?
            if (clipSolar.activeSelf) {
                DeactivateB5();
            }
            else {
                ActivateB5();
                DeactivateB1();
                DeactivateB2();
                DeactivateB3();
                DeactivateB4();
            }
        }

        void ActivateB1() {

            _mats[b1PicElement].SetTexture("_MainTex", b1OnPic);
            _mats[b1FrameElement].color = Color.red;
            clipNetwork.SetActive(true);
        }

        void DeactivateB1() {

            _mats[b1PicElement].SetTexture("_MainTex", b1OffPic);
            _mats[b1FrameElement].color = Color.white;
            clipNetwork.SetActive(false);
        }

        void ActivateB2() {

            _mats[b2PicElement].SetTexture("_MainTex", b2OnPic);
            _mats[b2FrameElement].color = Color.red;
            clipSettings.SetActive(true);
        }

        void DeactivateB2() {

            _mats[b2PicElement].SetTexture("_MainTex", b2OffPic);
            _mats[b2FrameElement].color = Color.white;
            clipSettings.SetActive(false);
        }

        void ActivateB3() {

            _mats[b3PicElement].SetTexture("_MainTex", b3OnPic);
            _mats[b3FrameElement].color = Color.red;
            clipMain.SetActive(true);
        }

        void DeactivateB3() {

            _mats[b3PicElement].SetTexture("_MainTex", b3OffPic);
            _mats[b3FrameElement].color = Color.black;
            clipMain.SetActive(false);
        }

        void ActivateB4() {

            _mats[b4PicElement].SetTexture("_MainTex", b4OnPic);
            _mats[b4FrameElement].color = Color.red;
            clipHouse.SetActive(true);
        }

        void DeactivateB4() {

            _mats[b4PicElement].SetTexture("_MainTex", b4OffPic);
            _mats[b4FrameElement].color = Color.black;
            clipHouse.SetActive(false);
        }

        void ActivateB5() {

            _mats[b5PicElement].SetTexture("_MainTex", b5OnPic);
            _mats[b5FrameElement].color = Color.red;
            clipSolar.SetActive(true);
        }

        void DeactivateB5() {

            _mats[b5PicElement].SetTexture("_MainTex", b5OffPic);
            _mats[b5FrameElement].color = Color.black;
            clipSolar.SetActive(false);
        }
    }
}
