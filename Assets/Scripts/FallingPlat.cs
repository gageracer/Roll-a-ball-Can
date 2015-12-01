using UnityEngine;
using System.Collections;

public class FallingPlat : MonoBehaviour {

	public float reactionTime = 5.0f;
	public float restoreTime = 10.0f;

	private float dieStamp;
	private float timeStamp;
	private bool move;
	private bool die;

	// Use this for initialization
	void Start () {
		move = false;
		die = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (move && timeStamp <= Time.time) {

			gameObject.SetActive (false);
			//GetComponent<Rigidbody> ().useGravity = true;
			//GetComponent<Rigidbody> ().isKinematic = false;
			move = !move;
			die = true;
		}
		if (dieStamp <= Time.time){
			gameObject.SetActive (true);
			//	die = !die;
		}


	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player") {
			move = true;
			timeStamp = Time.time + reactionTime;
			dieStamp = Time.time + restoreTime;
		}
	}

}
