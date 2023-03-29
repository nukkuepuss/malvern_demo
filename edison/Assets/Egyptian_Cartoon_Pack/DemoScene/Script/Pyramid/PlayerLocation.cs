using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocation : MonoBehaviour {

    public Transform Player;

    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	void Update () {
        this.transform.position = new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z);
	}
}
