using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace EgyptCartoonPack
{


    public class Door_SceneChange : MonoBehaviour
    {

        //player will look at the Z axis of this trigger when going back to the mainScene

        public Animator _animator;

        public int level_id;
        private bool canFade = true;
        private GameManager gameManager;
        public HouseTypesEnum HouseType;


        public void Start()
        {
            gameManager = GameObject.Find("__GameManager").GetComponent("GameManager") as GameManager;
        }


        public void OnTriggerEnter(Collider Col)
        {

            if (Col.gameObject.tag == "Player" && canFade)
            {
                canFade = false;
                _animator.SetTrigger("Fade");
                if (gameManager.lastLevel == 1)
                {

                    Vector3 Pos = this.transform.position + this.transform.forward * 2;
                    Pos.y = Col.transform.position.y;
                    gameManager.LastDoor = Pos;
                    gameManager.PlayerRotation = this.transform.rotation;


                    if (this.gameObject.GetComponent<Door_SceneChange>())
                    {

                        gameManager.HouseTypeTarget = this.gameObject.GetComponent<Door_SceneChange>().HouseType;
                    }
                }
                StartCoroutine(PlayOneShot());
            }
        }



        public IEnumerator PlayOneShot()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(level_id);
        }


    }
}
