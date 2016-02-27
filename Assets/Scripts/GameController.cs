using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject floorContainer;
    public GameObject player;

    public int maxCreateFloorNum = 6;
    // ゲーム領域内に生成しておくフロアセットの数
    public int createFloorSetCount = 5;

    public float floorStartPos = 2.0f;
    public float floorSetMargin = 3.0f;

    public float limitAliveFloor = 2.0f;
    public float floorSpeed = 0.03f;

    private Coroutine playerJumpRutine = null;
    private Coroutine floorRotationRutine = null;
    private Coroutine gameOverRutine = null;

    private FloorSet nowFloorGettingOn = null;
    private FloorGenerator floorGenerator = null;

    public bool IsPlayerJumping { get { return playerJumpRutine != null; } }
    public bool IsRorateFloor { get { return floorRotationRutine != null; } }
    public bool IsGameOver { get { return gameOverRutine != null; } }

    private float playerDefaultY = 1.0f;

    void Start ()
    {
        floorGenerator = GetComponent<FloorGenerator>();
        playerDefaultY = player.transform.position.y;
    }
	
    void Initialize()
    {
        for (int i=0; i < createFloorSetCount; ++i)
        {
            floorGenerator.CreateFloor(floorStartPos + floorSetMargin * i);
        }
    }

    void Update ()
    {
        // ゲームオーバーなら何もしない
        if (IsGameOver)
            return;
        
        // プレイヤの操作部分
        PlayerInput();

        // プレイヤのスクロール
        player.transform.position += new Vector3(-floorSpeed, 0, 0);
        /*if (nowFloorGettingOn != null)
        {
            player.transform.position = new Vector3(nowFloorGettingOn.distance, playerDefaultY, 0);
        }*/

        // ゲームオーバー判定
        if (player.transform.position.x < limitAliveFloor)
        {
            gameOverRutine = StartCoroutine(CreateGameOverRutine());
        }

        // 一定距離まで近づいたフロアセットを消滅させる
        if (floorContainer.transform.childCount > 0)
        {
            var firstChild = floorContainer.transform.GetChild(0);
            if (firstChild.GetComponent<FloorSet>().distance <= limitAliveFloor)
            {
                DestoryFloorSet(firstChild);
            }
        }

        // 足りない分のフロアを生成
        if (floorContainer.transform.childCount < createFloorSetCount)
        {
            for (int i = floorContainer.transform.childCount; i < createFloorSetCount; ++i)
            {
                floorGenerator.CreateFloor(floorStartPos + floorSetMargin * i);
            }
        }
    }

    void PlayerInput()
    {
        // 何らかの操作中は他の操作は出来ないようにする
        if (IsPlayerJumping || IsRorateFloor)
            return;
        
        // プレイヤキャラクタのジャンプ操作
        // TODO:
        bool doJump = Input.GetButton("Fire1");
        if (doJump)
        {
            playerJumpRutine = StartCoroutine(CreatePlayerJumpRutine());
        }

        // 足場の回転操作
        bool doRotation = Input.GetButton("Fire2");
        if (doRotation)
        {
            floorRotationRutine = StartCoroutine(CreateFloorRotationRutine());
        }
    }

    void DestoryFloorSet(Transform obj)
    {
        // TODO: もっと派手に
        Destroy(obj.gameObject);
    }

    IEnumerator CreatePlayerJumpRutine()
    {
        Debug.Log("PlayerJump");
        yield return new WaitForSeconds(0.5f);
        // ターゲットとなるフロアセットのスクリプトを参照しておく
        FloorSet targetFloor = null;
        foreach (Transform floorSet in floorContainer.transform)
        {
            if (floorSet.GetComponent<FloorSet>().IsNotReached)
            {
                targetFloor = floorSet.GetComponent<FloorSet>();
                break;
            }
        }
        if (targetFloor == null)
            yield break;

        // 参照スクリプトから着地点を常に計算する
        // 1秒くらいでジャンプさせる
        float passTime = 0f;
        float limitTime = 1.0f;
        var baseX = player.transform.position.x;
        float baseY = playerDefaultY;
        while ((passTime += Time.deltaTime) <= limitTime)
        {
            float per = passTime / limitTime;
            var pos = new Vector3();
            pos.x = baseX + (targetFloor.distance - baseX) * per;
            pos.y = baseY + 3f * (per < 0.5f ? per: 1.0f - per) * 2f;
            player.transform.position = pos;
            yield return null;
        }
        // 最後に合わせる
        player.transform.position = new Vector3(targetFloor.distance, baseY, 0);
        // フラグを消す
        targetFloor.IsNotReached = false;
        nowFloorGettingOn = targetFloor;

        Debug.Log("PlayerJump2");
        yield return null;

        playerJumpRutine = null;
    }

    IEnumerator CreateFloorRotationRutine()
    {
        Debug.Log("FloorRotation");
        yield return new WaitForSeconds(0.5f);

        float rotateAngle = 0f;
        float maxRotateAngle = 360f / maxCreateFloorNum * 2;

        // 1秒くらいで回転させる
        float passTime = 0f;
        float limitTime = 1.0f;
        var baseRotate = transform.rotation;
        while ((passTime += Time.deltaTime) <= limitTime)
        {
            rotateAngle = maxRotateAngle * passTime / limitTime;
            foreach (Transform floorSet in floorContainer.transform)
            {
                floorSet.rotation = baseRotate * Quaternion.Euler(0, 0, rotateAngle);
            }
            yield return null;
        }
        // 最後に合わせる
        foreach (Transform floorSet in floorContainer.transform)
        {
            floorSet.rotation = baseRotate * Quaternion.Euler(0, 0, maxRotateAngle);
        }
        transform.rotation *= Quaternion.Euler(0, 0, maxRotateAngle);

        floorRotationRutine = null;
    }

    IEnumerator CreateGameOverRutine()
    {
        Debug.Log("GameOver");

        yield return null;

        gameOverRutine = null;
    }
}
