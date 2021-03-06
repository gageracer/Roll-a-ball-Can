﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerMultiController : NetworkBehaviour 
{	
	public GameObject pickUps;
	public float speed;
	public float airSpeed;
	public float fly;
	public float heightCont;
	
	
	
	public GUIText countText;
	public GUIText winText;

	private Vector3 startPos;
	private float redSpeed = 1000;
	private int count;
	private int pickles;
	private int loadedLvl;
	private int dblJump = 0;
	public bool jumpControl;
	private bool bluebuff = false;
	private bool redBuff = false;
	
	
	void Awake()
	{
		loadedLvl = Application.loadedLevel;
	}
	void Start()
	{	
		startPos = transform.position;
		WhichPlayer ();
		HowManyPicks ();
		Debug.Log (pickles);
		count = 0;
		SetCountText ();
		winText.text = "";
	}
	
	void Update(){
		
			if (!isLocalPlayer)
				return;
		
		GroundCheck ();
		GreenBuff ();
		BlueBuff ();
		Movements ();
		RestartLevel ();
	}
	/*void Update(){

		RestartLevel ();

	}*/

	/*void OnNetworkInstantiate(NetworkMessageInfo info) {
		NetworkView nView = GetComponent<NetworkView> ();
		if (nView.isMine) {
			Camera.main.GetComponent<CameraControllerMultiplayer>().player = gameObject;
			Debug.Log ("New object instanted by me");
		} else {
			Debug.Log ("New object instantiated by " + info.sender);
		}
	}*/

	[ClientRpc]
	void RpcRespawn(){
	
	if (isLocalPlayer) {

			transform.position = startPos;
		
		}
	}

	[Command]
	void CmdRespawn(){
		
		if (isLocalPlayer) {
			
			transform.position = startPos;
			
		}
	}


	public override void OnStartLocalPlayer()
	{
		Camera.main.GetComponent<CameraControllerMultiplayer>().player = gameObject;
	}

	
	void WhichPlayer(){
		
		if (GameObject.FindWithTag ("HostPlayer") == null)
			this.tag = "HostPlayer";
		else 
			this.tag = "ClientPlayer";
	}
	
	
	void Movements(){
		
		
		
		float moveHorizontal = Input.GetAxis ("Horizontal_P1");
		float moveVertical = Input.GetAxis ("Vertical_P1");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		Vector3 jump = new Vector3 (0.0f, fly, 0.0f);
		
		
		
		if (jumpControl) {
			GetComponent<Rigidbody> ().AddForce (movement * speed * Time.deltaTime);
			
			if (Input.GetButtonDown ("Jump_P1")) {
				GetComponent<Rigidbody> ().AddForce (jump * Time.deltaTime);
				jumpControl = !jumpControl;
			}
		} else {
			GetComponent<Rigidbody> ().AddForce (movement * airSpeed * Time.deltaTime);//For going x and z axis in air
			//GetComponent<Rigidbody> ().AddForce (jump * 0.02f * Time.deltaTime);//For free falling with velocity?
			if (dblJump > 0) {
				if (Input.GetButtonDown ("Jump_P1")) {
					
					GetComponent<Rigidbody> ().AddForce (jump * Time.deltaTime);
					dblJump--;
				}
			} else if (bluebuff) {
				if (Input.GetButton ("Jump_P1")) {
					GetComponent<Rigidbody> ().AddForce (jump * 0.02f * Time.deltaTime);
				}
			}  
		}
		if (redBuff) {
			GetComponent<Rigidbody> ().AddForce (movement * redSpeed * Time.deltaTime,ForceMode.Impulse);
			redBuff = !redBuff;
		}
	}
	
	void BlueBuff (){
		
		if (GetComponent<Rigidbody> ().velocity.y < 0 && gameObject.GetComponent<Renderer> ().material.color == Color.blue) {
			
			bluebuff = true;
			
		} else 
			bluebuff = false;
		
	}
	
	void GreenBuff(){
		if (gameObject.GetComponent<Renderer> ().material.color == Color.green && jumpControl)
			dblJump = 1;
		else if (gameObject.GetComponent<Renderer> ().material.color != Color.green)
			dblJump = 0;
	}
	
	void RestartLevel(){
		if (transform.position.y < -10 || Input.GetKeyDown(KeyCode.R))
			RpcRespawn();
		//	CmdRespawn();
	}
	
	void HowManyPicks(){
		
		pickles = pickUps.transform.childCount;
	}
	
	void GroundCheck()
	{
		RaycastHit hit;
		Ray isGround = new Ray (transform.position, Vector3.down);
		if(Physics.Raycast(isGround,out hit, heightCont))
		{
			jumpControl = true;
		}
		
	}
	void OnTriggerEnter(Collider other)
	{		
		if (other.gameObject.tag == "PickUpRed") {
			redBuff = true;
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		} else if (other.gameObject.tag == "PickUpGreen") {
			dblJump = 2;
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		} else if (other.gameObject.tag == "PickUpBlue") {
			
			gameObject.GetComponent<Renderer> ().material.color = Color.blue;
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		}
	}
	
	void SetCountText()
	{
		countText.text = " Count: " + count.ToString ();
		if (count == pickles) {
			winText.text = "YOU WIN!";
			
		}
	}
}

//Destroy (other.gameObject);
//gameObject.tag = "Player";
//gameObject.SetActive(false);