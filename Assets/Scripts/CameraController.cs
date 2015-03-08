using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public GameObject player;
	public float smooth = 2;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
	
		offset = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {


		transform.position = player.transform.position + offset;
		CameraMovement ();
	


	}
	void CameraMovement()
	{	
		Quaternion forwardX = Quaternion.Euler (30, 0, 0);
		Quaternion backX = Quaternion.Euler(60,0,0);
		Quaternion rightY = Quaternion.Euler (45,15,0);
		Quaternion leftY = Quaternion.Euler (45,-15,0);
		Quaternion normalX = Quaternion.Euler (45,0,0);
		Quaternion normalY = Quaternion.Euler (45,0,0);

		int X = (int) Input.GetAxis ("Vertical");
		int Y = (int) Input.GetAxis ("Horizontal");
		switch (X) {
		case 1:
			transform.rotation = Quaternion.Slerp (transform.rotation, forwardX, smooth * Time.deltaTime);
			break;
		case -1:
			transform.rotation = Quaternion.Slerp (transform.rotation, backX, smooth * Time.deltaTime);
			break;
		default:
			Quaternion current = transform.rotation;
			if (current.x != normalX.x)
				transform.rotation = Quaternion.Slerp (current, normalX, smooth * Time.deltaTime);
			break;
		}
		switch (Y) {
		case 1:
			transform.rotation = Quaternion.Slerp (transform.rotation,rightY,smooth*Time.deltaTime );
			break;
		case -1:
			transform.rotation = Quaternion.Slerp (transform.rotation,leftY,smooth*Time.deltaTime );
			break;
		default:
			Quaternion current = transform.rotation;
			if (current.y != normalY.y)
				transform.rotation = Quaternion.Slerp (current, normalY, smooth * Time.deltaTime);
			break;
		}
		/*if (Input.GetAxis ("Vertical") == 1) {

			transform.rotation = Quaternion.Slerp (transform.rotation, forwardX, smooth * Time.deltaTime);
		} else if (Input.GetAxis ("Vertical") == -1) {

			transform.rotation = Quaternion.Slerp (transform.rotation, backX, smooth * Time.deltaTime);
		} else
			transform.rotation.x = Quaternion.Slerp (transform.rotation, normalX, smooth * Time.deltaTime);

		if (Input.GetAxis ("Horizontal") == 1) {
		
			transform.rotation = Quaternion.Slerp (transform.rotation,rightY,smooth*Time.deltaTime );
		}else if (Input.GetAxis ("Horizontal") == -1) {
		
			transform.rotation = Quaternion.Slerp (transform.rotation,leftY,smooth*Time.deltaTime );
		}else
			transform.rotation = Quaternion.Slerp (transform.rotation, normalY, smooth * Time.deltaTime);
			*/

	}
}
