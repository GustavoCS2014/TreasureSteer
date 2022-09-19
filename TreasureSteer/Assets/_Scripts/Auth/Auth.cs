using Gravitons.UI.Modal;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class Auth : MonoBehaviour
{
    public GameObject loadBar;
    [SerializeField] private TextMeshProUGUI textInfo;
    RectTransform rectTransform;

    public SceneState sceneState;

    // Start is called before the first frame update
    void Start()
    {
        textInfo.text = "Starting...";

        rectTransform = loadBar.GetComponent<RectTransform>();

        StartCoroutine(Validation());
    }

    IEnumerator Validation()
    {
        string token = getToken();
        JObject json = new();

        UnityWebRequest unityWebRequest = UnityWebRequest.Get("http://treasure-steer-api.test/api/user");
        unityWebRequest.SetRequestHeader("Authorization", "Bearer " + token);

        yield return new WaitForSeconds(0.5f);
        rectTransform.sizeDelta = new Vector2(25f, 3f);

        textInfo.text = "Validating...";

        yield return new WaitForSeconds(1f);
        rectTransform.sizeDelta = new Vector2(25f, 3f);

        if (token == "")
        {
            yield return new WaitForSeconds(1f);
            rectTransform.sizeDelta = new Vector2(100f, 3f);

            yield return new WaitForSeconds(1f);

            sceneState.callbackLogin();
        } else
        {
            textInfo.text = "Connecting to the Server";
            yield return new WaitForSeconds(1f);
            rectTransform.sizeDelta = new Vector2(33.3f, 3f);

            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(unityWebRequest.result);
            }
            else
            {
                textInfo.text = "Authenticating Credentials";
                rectTransform.sizeDelta = new Vector2(66.6f, 3f);
                yield return new WaitForSeconds(1f);

                string results = Encoding.Default.GetString(unityWebRequest.downloadHandler.data);

                try
                {
                    json = JObject.Parse(results);
                } catch 
                {
                    sceneState.callbackLogin();
                    deleteToken();

                    yield break;
                }

                if (json["status"].ToString() == "ok")
                {
                    textInfo.text = "Logged In";
                    rectTransform.sizeDelta = new Vector2(100f, 3f);
                    yield return new WaitForSeconds(1f);

                    sceneState.callbackMainMenu();
                }
            }
        }
    }

    private string getToken()
    {
        return PlayerPrefs.GetString("token");
    }

    private void deleteToken()
    {
        PlayerPrefs.DeleteKey("token");
    }
}
