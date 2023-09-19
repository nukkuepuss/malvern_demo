/// <summary>
/// SnapSettings.cs
/// two buttons... one setting (snap/smooth rotation)
/// + init clipboard
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class SnapSettings : MonoBehaviour {

        public GameObject button1, button2, clipboard;
        public int clipPicElement;
        public Texture2D clipPic;

        // how far should the button travel when pressed/depressed
        public float buttonMovement;

        public Material onMat, offMat;

        [HideInInspector]
        public int activeButton = 1;

        [Header("Movment script")]
        public MovePlayer movementScript;

        // internal flag
        private int _lastPressedButton;

        private Material[] _clipMats;

        void Start() {

            // set the clipboard picture
            _clipMats = clipboard.GetComponent<MeshRenderer>().materials;
            _clipMats[clipPicElement].mainTexture = clipPic;

            // set a flag
            _lastPressedButton = activeButton;

            // set the initial button position state
            // we need to move the button that's initially on up a bit, as it's about to be pressed and is assumed to be in the off position (which it's not)
            button1.transform.Translate(0f, buttonMovement, 0f);

            // ...and press it so that it has an initial state (snap rotation)
            PressButton1();
        }

        void Update() {

            // has a button been pressed that isn't already pressed?
            if (activeButton!=_lastPressedButton) {

                // move the buttons and change their color
                if (activeButton==1) {

                    PressButton1();
                }
                else {
                    PressButton2();
                }

                // update the flag
                _lastPressedButton = activeButton;
            }
        }

        void PressButton1() {

            button1.transform.Translate(0f, -buttonMovement, 0f);
            button2.transform.Translate(0f, buttonMovement, 0f);

            // NB I've tried for hours to get a reference to the buttons mesh renderer and use that here,
            // but the button drifts away with that implementation. It's very strange.
            button1.GetComponent<MeshRenderer>().material = onMat;
            button2.GetComponent<MeshRenderer>().material = offMat;

            movementScript.movementChoice = MovePlayer.movementStyles.snap;
        }

        void PressButton2() {

            button1.transform.Translate(0f, buttonMovement, 0f);
            button2.transform.Translate(0f, -buttonMovement, 0f);

            button1.GetComponent<MeshRenderer>().material = offMat;
            button2.GetComponent<MeshRenderer>().material = onMat;

            movementScript.movementChoice = MovePlayer.movementStyles.smooth;
        }
    }
}
