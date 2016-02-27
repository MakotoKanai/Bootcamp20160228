using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public GameObject leg;
	int score=0;
	public UnityEngine.UI.Text ScoreLabel;
	public float TIME=45;

    public Vector3 spownPoint = new Vector3(0, 3, 0);
    public float respownHeight = -30.0f;

    public UnityEngine.UI.Text ResultLabel;

    public GameController controller;

    private GameObject graphicModel;

	// Use this for initialization
	void Start () {
        graphicModel = transform.Find("Graph").gameObject;
        SetTime();
        controller = GameObject.Find("GameController").GetComponent<GameController>();
    }
	
    public void SetTime()
    {
        TIME = 45;
    }

	// Update is called once per frame
	void Update () {
        if (!controller.IsGame || controller.IsGameOver)
            return;

		TIME -= Time.deltaTime;
		if (TIME <= 0) {
			Result ();
		} else {
			Rigidbody rigidbody = GetComponent<Rigidbody> ();
			rigidbody.AddForce (0, -1f, 0);
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
				leg.GetComponent<OnGround> ().Flag = false;
				rigidbody.velocity = new Vector3 (0, 7f, 0);
			}
		}
        // リスポン
        if (transform.position.y <= respownHeight)
            PlayerSpown();
        ScoreLabel.gameObject.SetActive(true);
    }

    public void PlayerSpown()
    {
        transform.position = spownPoint;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
		
	void OnTriggerEnter(Collider col) {
		{
			if (col.CompareTag ("Item")) {
				score += 100;
				ScoreLabel.text = "Score:" + score.ToString();
				Destroy (col.gameObject);
			}
		}
	}

	void Result(){
        ResultLabel.text = "Score: " + score.ToString();
        ScoreLabel.gameObject.SetActive(false);
        controller.GetComponent<GameController>().StartResult();
    }


}
