using Gravitons.UI.Modal;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField userName;
    [SerializeField] private TMP_InputField userPassword;
    [SerializeField] private TextMeshProUGUI textPassword;

    [SerializeField] private TextMeshProUGUI textInfo;
    public GameObject loadScene;
    public GameObject loadBar;
    public GameObject blurScene;
    RectTransform rectTransform;

    public SceneState sceneState;

    void Start()
    {
        rectTransform = loadBar.GetComponent<RectTransform>();

        loadScene.gameObject.SetActive(false);
        blurScene.gameObject.SetActive(false);
    }

    public void showPassword()
    {
        if (userPassword.contentType == TMP_InputField.ContentType.Password)
        {
            userPassword.contentType = TMP_InputField.ContentType.Standard;
            textPassword.fontSize = 20;
            textPassword.margin = new Vector4(0, 0, 0, 0);
        } else
        {
            userPassword.contentType = TMP_InputField.ContentType.Password;
            textPassword.fontSize = 35;
            textPassword.margin = new Vector4(0, 0, 0, -12);
        }

        userPassword.ForceLabelUpdate();
    }

    private void login()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        loadScene.gameObject.SetActive(true);
        textInfo.text = "Connecting to the Server";
        yield return new WaitForSeconds(1f);
        rectTransform.sizeDelta = new Vector2(33.3f, 3f);

        WWWForm data = new WWWForm();
        data.AddField("username", userName.text);
        data.AddField("password", userPassword.text);

        UnityWebRequest unityWebRequest = UnityWebRequest.Post("http://treasure-steer-api.test/api/user/login", data);

        yield return unityWebRequest.SendWebRequest();

        if(unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(unityWebRequest.result);
        } else
        {
            textInfo.text = "Authenticating Credentials";
            rectTransform.sizeDelta = new Vector2(66.6f, 3f);
            yield return new WaitForSeconds(1f);

            string results = Encoding.Default.GetString(unityWebRequest.downloadHandler.data);
            JObject json = JObject.Parse(results);

            if (json["status"].ToString() == "ok")
            {
                textInfo.text = "Logged In";
                rectTransform.sizeDelta = new Vector2(100f, 3f);
                yield return new WaitForSeconds(1f);

                sceneState.callbackMainMenu();

                setToken(json["token"].ToString());
            } else
            {
                loadScene.gameObject.SetActive(false);
                blurScene.gameObject.SetActive(true);

                ModalManager.Show("An error has occurred", json["message"].ToString(), new[] { new ModalButton() { 
                    Text = "OK", 
                    Callback = canceledLogin
                } });
            }
        }
    }

    private void setToken(string value)
    {
        PlayerPrefs.SetString("token", value);
    }

    private void canceledLogin()
    {
        blurScene.gameObject.SetActive(false);
        rectTransform.sizeDelta = new Vector2(0f, 3f);
    }
}
