using UnityEngine;
using System.Collections;

public class FloorGenerator : MonoBehaviour
{
    public GameObject floorPrefab;
    public Vector2 circleCenter;
    public int maxCreateFloorNum = 6;

    public float createFloorProbability = 0.7f;

    public Transform floorContainer;

    void Start()
    {
        //floorContainer = transform.root.Find("FloorContainer");

        // test
        CreateFloor(5f);
        CreateFloor(8f);
        CreateFloor(11f);
    }

    public void CreateFloor(float distance)
    {
        // 管理用の空オブジェクトを生成
        GameObject setObject = new GameObject("FloorSet");
        setObject.transform.parent = floorContainer;
        setObject.transform.position = new Vector3(0, 0, 0);

        var floorSetScript = setObject.AddComponent<FloorSet>();
        floorSetScript.distance = distance;
        floorSetScript.maxCreateNum = maxCreateFloorNum;

        // フロアを自動配置
        float angleMargin = Mathf.PI / maxCreateFloorNum * 2;
        for (int i = 0; i < maxCreateFloorNum; ++i)
        {
            bool isCreate = (Random.Range(0f, 1f) <= createFloorProbability);
            if (isCreate)
            {
                float angle = angleMargin * i;
                var floor = Instantiate(floorPrefab, new Vector3(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0), Quaternion.identity) as GameObject;
                floor.transform.parent = setObject.transform;
            }
        }
    }

}
