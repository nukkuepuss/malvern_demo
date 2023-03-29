/// <summary>
/// handles the mainmap clipboard
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class NewMainSetup : MonoBehaviour {

        public GameObject main, buildings, roads;
        public Texture2D mainOn, mainOff, buildingsOn, buildingsOff, roadsOn, roadsOff;
        public int mainPicPos, buildingsPicPos, roadsPicPos;
        public int mainFringePos, buildingsFringePos, roadsFringePos;
        private Material[] _mats;

        void Start() {

            // get a reference to the materials of the clipboard
            _mats = GetComponentInChildren<MeshRenderer>().materials;

            // initialize
            if (main.activeSelf) {

                ActivateMainmap();
            }
            else {

                DeactivateMainmap();
            }

            if (buildings.activeSelf) {

                ActivateBuildings();
            }
            else {

                DeactivateBuildings();
            }

            if (roads.activeSelf) {

                ActivateRoads();
            }
            else {

                DeactivateRoads();
            }
        }

        public void MainmapTrigger() {

            // if on, turn off
            if (main.activeSelf) {

                DeactivateMainmap();
            }
            // if off, turn on
            else {

                ActivateMainmap();
            }
        }

        public void BuildingsTrigger() {

            if (buildings.activeSelf) {

                DeactivateBuildings();
            }
            else {

                ActivateBuildings();
            }
        }

        public void RoadsTrigger() {

            if (roads.activeSelf) {

                DeactivateRoads();
            }
            else {

                ActivateRoads();
            }
        }

        public void ActivateMainmap() {

            main.SetActive(true);
            _mats[mainPicPos].SetTexture("_MainTex", mainOn);
            _mats[mainFringePos].color = Color.red;
        }

        public void DeactivateMainmap() {

            main.SetActive(false);
            _mats[mainPicPos].SetTexture("_MainTex", mainOff);
            _mats[mainFringePos].color = Color.white;
        }

        public void ActivateBuildings() {

            buildings.SetActive(true);
            _mats[buildingsPicPos].SetTexture("_MainTex", buildingsOn);
            _mats[buildingsFringePos].color = Color.red;
        }

        public void DeactivateBuildings() {

            buildings.SetActive(false);
            _mats[buildingsPicPos].SetTexture("_MainTex", buildingsOff);
            _mats[buildingsFringePos].color = Color.white;
        }

        public void ActivateRoads() {

            roads.SetActive(true);
            _mats[roadsPicPos].SetTexture("_MainTex", roadsOn);
            _mats[roadsFringePos].color = Color.red;
        }

        public void DeactivateRoads() {

            roads.SetActive(false);
            _mats[roadsPicPos].SetTexture("_MainTex", roadsOff);
            _mats[roadsFringePos].color = Color.white;
        }
    }
}
