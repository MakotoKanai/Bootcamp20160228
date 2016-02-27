using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public GameObject leg;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			Vector3 pos = transform.position;
			pos.x += 0.05f;
			transform.position = pos;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			Vector3 pos = transform.position;
			pos.x -= 0.05f;
			transform.position = pos;
		}
		if (Input.GetKey (KeyCode.UpArrow) && leg.GetComponent<OnGround> ().Flag) {
			Rigidbody rigidbody = GetComponent<Rigidbody> ();
			leg.GetComponent<OnGround> ().Flag = false;
			rigidbody.AddForce (0, 400, 0);
		}
		if (transform.position.y <= -10) {
			Vector3 pos = transform.position;
			pos.x = 2f;
			pos.y = 3f;
			transform.position = pos;
		}
	}
		
}
