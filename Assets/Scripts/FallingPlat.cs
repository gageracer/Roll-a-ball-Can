﻿using UnityEngine;
using System.Collections;

public class FallingPlat : MonoBehaviour {

	public float reactionTime = 5.0f;

	private float timeStamp;
	private bool move;


	// Use this for initialization
	void Start () {
		move = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (move && timeStamp <= Time.time) {
			GetComponent<Rigidbody> ().useGravity = true;
			GetComponent<Rigidbody> ().isKinematic = false;
			move = !move;
		}


	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player") {
			move = true;
			timeStamp = Time.time + reactionTime;

		}
	}

}
