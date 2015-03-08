using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{	
	public GameObject pickUps;
	public float speed;
	public float airSpeed;
	public float fly;
	public float heightCont;
	public int jumpControl;


	public GUIText countText;
	public GUIText winText;


	private int count;
	private int pickles;
	private int loadedLvl;

	void Awake()
	{
		loadedLvl = Application.loadedLevel;
	}
	void Start()
	{

		HowManyPicks ();
		Debug.Log (pickles);
		count = 0;
		SetCountText ();
		winText.text = "";
	}
	
	void FixedUpdate(){
		
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		Vector3 jump = new Vector3 (0.0f, fly, 0.0f);

	
		RestartLevel ();
		GroundCheck ();
		if (jumpControl >= 0 ) {
				
			GetComponent<Rigidbody> ().AddForce (movement * speed * Time.deltaTime);

			if (Input.GetButton ("Jump")) {
				GetComponent<Rigidbody> ().AddForce (jump * Time.deltaTime);
				jumpControl--;

			} else {
				GetComponent<Rigidbody> ().AddForce (movement * airSpeed * Time.deltaTime);
			}
		
		
		}
	}	

	void RestartLevel(){
		if (transform.position.y < -10)
		Application.LoadLevel (loadedLvl);
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
			jumpControl = 2;
			}

	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PickUp") {
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
			gameObject.GetComponent<Renderer>().material.color = Color.red;
		}
	}
}

//Destroy (other.gameObject);
//gameObject.tag = "Player";
//gameObject.SetActive(false);