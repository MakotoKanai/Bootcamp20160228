using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public GameObject leg;

    public Vector3 spownPoint = new Vector3(0, 3, 0);
    public float respownHeight = -30.0f;

    private GameObject graphicModel;

	// Use this for initialization
	void Start () {
        graphicModel = transform.Find("Graph").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			Vector3 pos = transform.position;
			pos.x += 0.05f;
			transform.position = pos;
            graphicModel.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey (KeyCode.LeftArrow)) {
			Vector3 pos = transform.position;
			pos.x -= 0.05f;
			transform.position = pos;
            graphicModel.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKey (KeyCode.UpArrow) && leg.GetComponent<OnGround> ().Flag) {
			Rigidbody rigidbody = GetComponent<Rigidbody> ();
			leg.GetComponent<OnGround> ().Flag = false;
			rigidbody.AddForce (0, 400, 0);
		}
        // リスポン
        if (transform.position.y <= respownHeight)
            PlayerSpown();
    }
	
    public void PlayerSpown()
    {
        transform.position = spownPoint;
    }
    	
}
