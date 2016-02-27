using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStartButton : MonoBehaviour
{
    public GameObject buttonObject;
    public GameController controller;

    public float waitTime = 1.0f;
    public float blinkMargin = 0.25f;

    private Coroutine rutine = null;
    
    public void ButtonPush()
    {
        if (rutine == null)
        {
            rutine = StartCoroutine(CreateGameStartRutine());
        }
    }

    IEnumerator CreateGameStartRutine()
    {
        var buttonImage = buttonObject.GetComponent<UnityEngine.UI.Image>();
        controller = GameObject.Find("GameController").GetComponent<GameController>();

        float passTime = 0f;
        while ((passTime += Time.deltaTime) < waitTime)
        {
            float per = (Mathf.Cos(Mathf.PI * passTime / blinkMargin * 2) + 1.0f) / 4.0f + 0.5f;
            buttonImage.color = new Color(1.0f, 1.0f * per, 1.0f * per);
            yield return null;
        }

        buttonImage.color = new Color(1f, 1f, 1f);

        controller.GameStart();

        gameObject.SetActive(false);
        rutine = null;
        yield break;
    }
}
