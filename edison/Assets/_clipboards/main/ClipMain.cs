/// <summary>
/// handles the mainmap clipboard
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class ClipMain : MonoBehaviour {

        public GameObject main, stub, world_stub, buildings, roads;
        public Texture2D mainOn, mainOff, stubOn, stubOff, buildingsOn, buildingsOff, roadsOn, roadsOff;

        public int mainPicPos, stubPicPos, buildingsPicPos, roadsPicPos;
        public int mainFringePos, stubFringePos, buildingsFringePos, roadsFringePos;

        public float stubSpeed;

        private Material[] _mats;

        private Vector3 _originalStubPosition;
        public Vector3 _targetStubPosition;

        private bool _stubExtend;
        private bool _stubRetract;
        private bool _destroyStubOnRetract;
        private bool _mainWasOn;

        void Start() {

            // get a reference to the materials of the clipboard
            _mats = GetComponentInChildren<MeshRenderer>().materials;

            // store the stub's original position
            _originalStubPosition = stub.transform.position;

            // initialize layers
            InitLayers();
        }

        private void Update() {

            if (_stubExtend) {

                stub.transform.position = Vector3.MoveTowards(stub.transform.position, _targetStubPosition, stubSpeed * Time.deltaTime);

                if (stub.transform.position == _targetStubPosition) {

                    _stubExtend = false;
                }
            }

            if (_stubRetract) {

                stub.transform.position = Vector3.MoveTowards(stub.transform.position, _originalStubPosition, stubSpeed * Time.deltaTime);

                if (_destroyStubOnRetract && stub.transform.position == _originalStubPosition) {

                    world_stub.SetActive(false);
                    main.SetActive(true);
                    DeactivateStubButton();
                    stub.SetActive(false);
                    _stubRetract = false;
                }
            }
        }

        public void MainmapTrigger() {

            // no stub to worry about?
            if (!stub.activeSelf) {

                // is the mainmap already on?
                if (main.activeSelf) {

                    // mainmap ON to OFF
                    DeactivateMainButton();
                    main.SetActive(false);
                }

                // no map is showing
                else {

                    // mainmap OFF to ON
                    ActivateMainButton();
                    main.SetActive(true);
                }
            }

            // the stub is active
            else {

                // is the worldstub active?
                if (world_stub.activeSelf) {

                    // stop showing worldstub and animate the stub downwards
                    DeactivateMainButton();
                    world_stub.SetActive(false);
                    _stubRetract = true;
                }

                // the stub is on it's own
                else {

                    // swap mainmap for worldstub and animate the stub upwards
                    ActivateMainButton();
                    main.SetActive(false);
                    world_stub.SetActive(true);
                    _stubExtend = true;
                    _stubRetract = false;
                    _destroyStubOnRetract = false;
                }
            }
        }

        public void StubTrigger() {

            // is the stub not showing?
            if (!stub.activeSelf) {

                // is the mainmap showing?
                if (main.activeSelf) {

                    // swap mainmap for worldstub, show the stub and animate it upwards
                    main.SetActive(false);
                    world_stub.SetActive(true);
                    ActivateStubButton();
                    stub.SetActive(true);
                    _stubRetract = false;
                    _stubExtend = true;
                }

                // map is showing, so just show the stub without animation
                else {

                    ActivateStubButton();
                    stub.SetActive(true);
                }
            }

            // stub is showing
            else {

                // is worldstub showing?
                if (world_stub.activeSelf) {

                    // this is a special case : the stub is extended and the worldstub is showing.
                    // We want the stub to retract into worldmap and worldmap to morph into mainmap
                    // so set a flag and do the logic for swapping worldstub with mainmap and deactivating stub in FixedUpdate
                    _destroyStubOnRetract = true;
                    _stubExtend = false;
                    _stubRetract = true;
                }

                // no map is showing
                else {

                    DeactivateStubButton();
                    stub.SetActive(false);
                }
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

        // activating and initializing buttons

        public void ActivateMainButton() {

            main.SetActive(true);
            _mats[mainPicPos].SetTexture("_MainTex", mainOn);
            _mats[mainFringePos].color = Color.red;
        }

        public void DeactivateMainButton() {

            main.SetActive(false);
            _mats[mainPicPos].SetTexture("_MainTex", mainOff);
            _mats[mainFringePos].color = Color.white;
        }

        public void ActivateStubButton() {

            _mats[stubPicPos].SetTexture("_MainTex", stubOn);
            _mats[stubFringePos].color = Color.red;
            stub.SetActive(true);
        }

        public void DeactivateStubButton() {

            _mats[stubPicPos].SetTexture("_MainTex", stubOff);
            _mats[stubFringePos].color = Color.white;
            stub.SetActive(false);
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

        public void InitLayers() {

            if (main.activeSelf) {

                ActivateMainButton();
            }
            else {

                DeactivateMainButton();
            }

            if (stub.activeSelf) {

                ActivateStubButton();
            }
            else {

                DeactivateStubButton();
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
    }
}