using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace EgyptCartoonPack
{
    public enum HouseTypesEnum
    {
        BigHouse, MedHouse, SmallHouse
    }

    public class GameManager : MonoBehaviour
    {
        public Vector3 LastDoor;
        public Quaternion PlayerRotation;
        public Quaternion RotationToApply;
        //houses
        public GameObject BigHouseSpawn;
        public GameObject MedHouseSpawn;
        public GameObject SmallHouseSpawn;
        public HouseTypesEnum HouseTypeTarget;
        //exterior
        public GameObject VillageSpawn;
        public GameObject TempleSpawn;
        public GameObject PyramidSpawn;

        private bool firstSpawn;

        [HideInInspector]
        public int lastLevel = 0;

        void Start()
        {
            DontDestroyOnLoad(transform.gameObject);
            SceneManager.LoadScene(1);
            SceneManager.sceneLoaded += OnSceneLoaded;
            firstSpawn = true;
        }

        // 1 = exterior 2 = house 3 = pyramide 4 = temple

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            GameObject player = FindPlayer();
            if(firstSpawn == true) {
                VillageSpawn = GameObject.FindGameObjectWithTag("Spawn");
                player.transform.position = VillageSpawn.transform.position;
                LastDoor = VillageSpawn.transform.position;
                firstSpawn = false;
            }
            if (scene.buildIndex == 1)
            {

                player.transform.rotation = PlayerRotation;
                player.transform.position = LastDoor;


                lastLevel = 1;
            }

            if (scene.buildIndex == 2) {

                this.GetComponent<AudioSource>().volume = 0.3f;

                if (this.HouseTypeTarget == HouseTypesEnum.BigHouse) {
                    BigHouseSpawn = GameObject.Find("Big_House_Spawn");
                    player.transform.position = BigHouseSpawn.transform.position;
                    player.transform.rotation = BigHouseSpawn.transform.rotation;
                } else if (this.HouseTypeTarget == HouseTypesEnum.MedHouse) {
                    MedHouseSpawn = GameObject.Find("Med_House_Spawn");
                    player.transform.position = MedHouseSpawn.transform.position;
                    player.transform.rotation = MedHouseSpawn.transform.rotation;
                } else if (this.HouseTypeTarget == HouseTypesEnum.SmallHouse) { 
                    SmallHouseSpawn = GameObject.Find("Small_House_Spawn");
                    player.transform.position = SmallHouseSpawn.transform.position;
                    player.transform.rotation = SmallHouseSpawn.transform.rotation;
                }
                    lastLevel = 2;
                }

            if (scene.buildIndex == 3)
            {

                PyramidSpawn = GameObject.FindGameObjectWithTag("Spawn");
                player.transform.position = PyramidSpawn.transform.position;
                player.transform.rotation = PyramidSpawn.transform.rotation;

                lastLevel = 3;
            }

            if (scene.buildIndex == 4)
            {
                TempleSpawn = GameObject.FindGameObjectWithTag("Spawn");
                player.transform.position = TempleSpawn.transform.position;
                player.transform.rotation = TempleSpawn.transform.rotation;
                lastLevel = 4;
            }
        }
 
        public GameObject FindPlayer()
        {
            GameObject o;

            //THIS NEED TO BE YOUR PLAYER
            o = GameObject.FindGameObjectWithTag("Player");

            return o;
        }
    }
   
}
