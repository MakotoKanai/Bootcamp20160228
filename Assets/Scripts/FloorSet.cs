using UnityEngine;
using System.Collections;

public class FloorSet : MonoBehaviour
{
    public GameObject gameController;

    public int maxCreateNum;
    public float distance;
	Vector3 currentposition;
	Vector3 diff;
    public bool IsNotReached { get; set; }

    public GameController controller;

    void Start()
    {
        IsNotReached = true;

        gameController = GameObject.Find("GameController");
        controller = gameController.GetComponent<GameController>();
    }

	void Update ()
    {
        // 中心に近づける
        // TODO: 設定から値を取ってくる
        distance -= controller.floorSpeed;
        
        // 子オブジェクトの位置調整
        float angleMargin = Mathf.PI / maxCreateNum * 2;
        foreach (Transform child in transform)
        {
            int i = child.name[0] - '0';
            float angle = angleMargin * i;
            child.position = transform.rotation * new Vector3(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0);
            child.rotation = Quaternion.Euler(0, 0, transform.parent.rotation.eulerAngles.z);
			/*if (currentposition == null)
				diff = Vector3.zero;
			else {
				diff = child.position - currentposition;
			}
			Debug.Log ("diff:"+diff);
				
			currentposition = child.position;*/
        }
    }
}
