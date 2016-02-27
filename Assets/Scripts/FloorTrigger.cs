using UnityEngine;
using System.Collections;

public class FloorTrigger : MonoBehaviour
{
    public GameController gameController;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter()
    {
        if (gameController != null)
        {
            Debug.Log("OnFloor");
            // 回さないために登録
            gameController.nowFloorGettingOn = transform.parent.parent.GetComponent<FloorSet>();
        }
    }
}
