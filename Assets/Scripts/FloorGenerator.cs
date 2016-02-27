using UnityEngine;
using System.Collections;

public class FloorGenerator : MonoBehaviour
{
    public GameObject floorPrefab;
    public Vector2 circleCenter;

    public float createFloorProbability = 0.7f;

    public Transform floorContainer;
    public GameController gameController;

    IEnumerator Start()
    {
        //floorContainer = transform.root.Find("FloorContainer");

        yield return null;
        gameController = this.GetComponent<GameController>();

        yield return null;
    }

    public void CreateFloor(float distance)
    {
        // 管理用の空オブジェクトを生成
        GameObject setObject = new GameObject("FloorSet");
        setObject.transform.parent = floorContainer;
        setObject.transform.position = new Vector3(0, 0, 0);

        var floorSetScript = setObject.AddComponent<FloorSet>();
        floorSetScript.distance = distance;
        floorSetScript.maxCreateNum = gameController.maxCreateFloorNum;

        // フロアを自動配置
        float angleMargin = Mathf.PI / gameController.maxCreateFloorNum * 2;
        for (int i = 0; i < gameController.maxCreateFloorNum; ++i)
        {
            bool isCreate = (Random.Range(0f, 1f) <= createFloorProbability);
            if (isCreate)
            {
                float angle = angleMargin * i;
                var floor = Instantiate(floorPrefab, new Vector3(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0), Quaternion.identity) as GameObject;
                floor.transform.parent = setObject.transform;
                floor.name = string.Format("{0}_Floor", i);
            }
        }
    }

}
