using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStartButton : MonoBehaviour
{
    public GameObject buttonObject;

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

        float passTime = 0f;
        while ((passTime += Time.deltaTime) < 2.0f)
        {
            float per = (Mathf.Cos(Mathf.PI * passTime * 2) + 1.0f) / 4.0f + 0.5f;
            buttonImage.color = new Color(1.0f, 1.0f * per, 1.0f * per);
            yield return null;
        }

        buttonImage.color = new Color();

        gameObject.SetActive(false);
        rutine = null;
        yield break;
    }
}
