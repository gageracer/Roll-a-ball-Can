using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{	
	public GameObject pickUps;
	public float speed;
	public float airSpeed;
	public float fly;
	public float heightCont;
	private bool redBuff = false;


	public GUIText countText;
	public GUIText winText;

	private float normalSpeed = 500;
	private float redSpeed = 1000;
	private int count;
	private int pickles;
	private int loadedLvl;
	private bool dblJump = false;
	private bool jumpControl;
	private bool bluebuff = false;



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
		
		RestartLevel ();
		GroundCheck ();
		GreenBuff ();
		BlueBuff ();
		Movements ();

	}

	void Movements(){

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		Vector3 jump = new Vector3 (0.0f, fly, 0.0f);



		if (jumpControl) {
			GetComponent<Rigidbody> ().AddForce (movement * speed * Time.deltaTime);

			if (Input.GetButtonDown ("Jump")) {
				GetComponent<Rigidbody> ().AddForce (jump * Time.deltaTime);
				jumpControl = !jumpControl;
			}
		} else {
			GetComponent<Rigidbody> ().AddForce (movement * airSpeed * Time.deltaTime);
			if (dblJump) {
				if (Input.GetButtonDown ("Jump")) {

					GetComponent<Rigidbody> ().AddForce (jump * Time.deltaTime);
					dblJump = !dblJump;
				}
			} else if (bluebuff) {
				if (Input.GetButton ("Jump")) {
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
			dblJump = true;
		else if (gameObject.GetComponent<Renderer> ().material.color != Color.green)
			dblJump = false;
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
			speed = normalSpeed;
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		} else if (other.gameObject.tag == "PickUpBlue") {
			speed = normalSpeed;
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