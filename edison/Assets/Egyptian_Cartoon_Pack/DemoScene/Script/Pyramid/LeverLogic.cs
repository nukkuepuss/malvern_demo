using UnityEngine;
using System.Collections;

public class LeverLogic : MonoBehaviour {

	public Animator StatueAnimatorToEnable;
	public Animator LeverAnimator;
	public Transform Player;
	private float Distance = 3;
	public GameObject PopUp;

	public bool canActivate = false;
	private bool finish = false;

    private void Start() {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        canActivate = false;
    }


	void LateUpdate(){
		if(canActivate && Input.GetKeyUp(KeyCode.E)){
			Activate();
		}

	}
	void OnTriggerStay(){
        if ((Vector3.Distance(Player.position,this.transform.position) < Distance) && !finish){
			Show();
		}
		else{
			Hide();
		}
	}

	void Activate(){
		if(canActivate){
			finish = true;
			LeverAnimator.SetTrigger("ActivateLever");
			StatueAnimatorToEnable.SetTrigger("ActivateStatue");
			this.GetComponent<AudioSource>().Play();
			Hide ();
		}
	}

	void OnTriggerExit(){
		Hide();
    
	}


	void Show(){
		PopUp.GetComponent<Renderer>().enabled =true;
        canActivate = true;
	}

	void Hide(){
		PopUp.GetComponent<Renderer>().enabled =false;
        canActivate = false;
	}

}