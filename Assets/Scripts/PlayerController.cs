using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{	
	public float speed;
	public float fly;
	public GUIText countText;
	public GUIText winText;
	private int count;
	private bool jumpControl;


	void Start()
	{
		jumpControl = true;
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		Vector3 movement2 = new Vector3 (0.0f, fly, 0.0f);
		if (jumpControl && !Input.GetButton("Jump")) {
			rigidbody.AddForce (movement * speed * Time.deltaTime);
		}
		if (jumpControl) {
			if (Input.GetButton("Jump"))
				rigidbody.AddForce (movement2 * Time.deltaTime);
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
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Ground") {
			Debug.Log ("Yere degdi");
			jumpControl = true;
		}
	}



	void OnCollisionExit(Collision other)
	{

		if (other.gameObject.tag == "Ground") {
			Debug.Log("Yerden ayrildi");
			jumpControl = false;
		
		}
	}

	void SetCountText()
	{
		countText.text = " Count: " + count.ToString ();
		if (count >= 12) {
			winText.text = "YOU WIN!";		
		}
	}
}

//Destroy (other.gameObject);
//gameObject.tag = "Player";
//gameObject.SetActive(false);