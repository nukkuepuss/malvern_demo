/// <summary>
/// GalleryManager.cs
/// manager for the balloon gallery panel
/// scroll through pictures and jump to their locations
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class GalleryManager : MonoBehaviour {

        public GameObject player;
        public GameObject panel;

        public Texture2D[] pics;
        public Texture2D[] textBoxes;
        public Transform[] locations;

        public int panelPicElement, panelTextElement;

        private Material[] panelMats;

        private int current, last;

        private void Start() {

            // intialize variables
            current = 0;
            last = pics.Length-1;

            panelMats = panel.GetComponent<MeshRenderer>().materials;

            ChangePic();
        }

        void ChangePic() {

            panelMats[panelPicElement].mainTexture = pics[current];
            panelMats[panelTextElement].mainTexture = textBoxes[current];
        }

        public void LeftArrowPress() {

            current--;

            // wrap around to end
            if (current<0) {

                current = last;
            }

            ChangePic();
        }

        public void RightArrowPress() {

            current++;

            //wrap around to start
            if (current>last) {

                current = 0;
            }

            ChangePic();
        }

        public void Jump() {

            player.transform.position = locations[current].transform.position;
            player.transform.rotation = locations[current].transform.rotation;
        }
    }
}
