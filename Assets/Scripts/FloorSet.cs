using UnityEngine;
using System.Collections;

public class FloorSet : MonoBehaviour
{
    GameObject gameController;

    public int maxCreateNum;
    public float distance;

    // Update is called once per frame
	void Update ()
    {
        // 中心に近づける
        // TODO: 設定から値を取ってくる
        distance -= 0.01f;
        
        // 子オブジェクトの位置調整
        int i = 0;
        float angleMargin = Mathf.PI / maxCreateNum * 2;
        foreach (Transform child in transform)
        {
            float angle = angleMargin * i;
            child.position = transform.rotation * new Vector3(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0);
            ++i;
        }
    }
}
