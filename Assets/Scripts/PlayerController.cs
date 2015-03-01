using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{	
	public float speed;
	public float fly;
	public float heightCont;
	public GUIText countText;
	public GUIText winText;
	private int count;
	private int jumpControl;



	void Start()
	{



		count = 0;
		SetCountText ();
		winText.text = "";
	}
	
	void FixedUpdate()
	{
		Debug.Log (jumpControl);
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		Vector3 movement2 = new Vector3 (0.0f, fly, 0.0f);
		GroundCheck ();
		if (jumpControl==1) {
			rigidbody.AddForce (movement * speed * Time.deltaTime);
			if (Input.GetButton("Jump")){
				rigidbody.AddForce (movement2 * Time.deltaTime);
				jumpControl = 0;

			}
		}

	}

	void GroundCheck()
	{
		RaycastHit hit;
		Ray isGround = new Ray (transform.position, Vector3.down);
		if(Physics.Raycast(isGround,out hit, heightCont))
		    {
			jumpControl = 1;
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
		if (count >= 12) {
			winText.text = "YOU WIN!";
			gameObject.renderer.material.color = Color.red;
		}
	}
}

//Destroy (other.gameObject);
//gameObject.tag = "Player";
//gameObject.SetActive(false);