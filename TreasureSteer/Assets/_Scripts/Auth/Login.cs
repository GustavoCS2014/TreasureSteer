using Newtonsoft.Json.Linq;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField userName;
    [SerializeField] private TMP_InputField userPassword;
    [SerializeField] private TextMeshProUGUI textPassword;

    public GameObject loadScene;
    public GameObject loadBar;
    [SerializeField] private TextMeshProUGUI textInfo;

    public SceneState sceneState;

    float countdown = -1f;
    bool done = false;

    void Start()
    {
        loadScene.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(countdown > 0f)
        {
            countdown -= Time.deltaTime;

            RectTransform rectTransform = loadBar.GetComponent<RectTransform>();

            if(countdown > 1f) 
                rectTransform.sizeDelta = new Vector2(Mathf.RoundToInt(100 - ((countdown -1) * 25)), 3f);

            if (countdown <= 0f)
            {
                done = true;
            }

            if(countdown > 3f) textInfo.text = "Connecting to the server";
            if(countdown < 3f) textInfo.text = "Logging in";
        }

        if (done)
        {
            sceneState.callbackMainMenu();
        }
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

    public void login()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        loadScene.gameObject.SetActive(true);

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
            string results = Encoding.Default.GetString(unityWebRequest.downloadHandler.data);
            JObject json = JObject.Parse(results);

            if (json["status"].ToString() == "ok")
            {
                countdown = 5;
                setToken(json["token"].ToString());
            }
        }
    }

    public void setToken(string value)
    {
        PlayerPrefs.SetString("token", value);
    }
}
