using Newtonsoft.Json.Linq;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Auth : MonoBehaviour {
    public GameObject loadBar;
    public GameObject load;
    [SerializeField] private TextMeshProUGUI textInfo;
    RectTransform rectTransform;
    RectTransform rectTransformLoad;

    public SceneState sceneState;

    // Start is called before the first frame update
    void Start() {
        textInfo.text = "Starting...";

        rectTransform = loadBar.GetComponent<RectTransform>();
        rectTransformLoad = load.GetComponent<RectTransform>();

        StartCoroutine(Validation());
    }

    IEnumerator Validation() {
        string token = getToken();
        JObject json = new();

        UnityWebRequest unityWebRequest = UnityWebRequest.Get("http://treasure-steer-api.test/api/user");
        unityWebRequest.SetRequestHeader("Authorization", "Bearer " + token);

        yield return new WaitForSeconds(0.5f);
        rectTransform.sizeDelta = new Vector2(rectTransformLoad.rect.width * 0.25f, rectTransform.rect.height);

        textInfo.text = "Validating...";

        yield return new WaitForSeconds(1f);
        rectTransform.sizeDelta = new Vector2(rectTransformLoad.rect.width * 0.30f, rectTransform.rect.height);

        if (token == "") {
            yield return new WaitForSeconds(1f);
            rectTransform.sizeDelta = new Vector2(rectTransformLoad.rect.width * 0.80f, rectTransform.rect.height);

            yield return new WaitForSeconds(1f);
            rectTransform.sizeDelta = new Vector2(rectTransformLoad.rect.width - 16f, rectTransform.rect.height);

            yield return new WaitForSeconds(1f);

            sceneState.callbackLogin();
        }
        else {
            textInfo.text = "Connecting to the Server";
            yield return new WaitForSeconds(1f);
            rectTransform.sizeDelta = new Vector2(rectTransformLoad.rect.width * 0.33f, rectTransform.rect.height);

            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError) {
                Debug.Log(unityWebRequest.result);
            }
            else {
                textInfo.text = "Authenticating Credentials";
                rectTransform.sizeDelta = new Vector2(rectTransformLoad.rect.width * 0.66f, rectTransform.rect.height);
                yield return new WaitForSeconds(1f);

                string results = Encoding.Default.GetString(unityWebRequest.downloadHandler.data);

                try {
                    json = JObject.Parse(results);
                }
                catch {
                    sceneState.callbackLogin();
                    deleteToken();

                    yield break;
                }

                if (json["status"].ToString() == "ok") {
                    textInfo.text = "Logged In";
                    rectTransform.sizeDelta = new Vector2(rectTransformLoad.rect.width - 16f, rectTransform.rect.height);
                    yield return new WaitForSeconds(1f);

                    sceneState.callbackMainMenu();
                }
            }
        }
    }

    private string getToken() {
        return PlayerPrefs.GetString("token");
    }

    private void deleteToken() {
        PlayerPrefs.DeleteKey("token");
    }
}
