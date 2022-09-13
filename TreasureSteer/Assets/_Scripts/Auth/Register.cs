using Newtonsoft.Json.Linq;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Register : MonoBehaviour
{
    [SerializeField] private TMP_InputField fullName;
    [SerializeField] private TMP_InputField userName;
    [SerializeField] private TMP_InputField userPassword;
    [SerializeField] private TextMeshProUGUI textPassword;

    public SceneState sceneState;

    public void showPassword()
    {
        if (userPassword.contentType == TMP_InputField.ContentType.Password)
        {
            userPassword.contentType = TMP_InputField.ContentType.Standard;
            textPassword.fontSize = 20;
            textPassword.margin = new Vector4(0, 0, 0, 0);
        }
        else
        {
            userPassword.contentType = TMP_InputField.ContentType.Password;
            textPassword.fontSize = 35;
            textPassword.margin = new Vector4(0, 0, 0, -12);
        }

        userPassword.ForceLabelUpdate();
    }

    public void register()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        WWWForm data = new WWWForm();
        data.AddField("name", fullName.text);
        data.AddField("username", userName.text);
        data.AddField("password", userPassword.text);

        UnityWebRequest unityWebRequest = UnityWebRequest.Post("http://treasure-steer-api.test/api/user/register", data);

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(unityWebRequest.result);
        }
        else
        {
            string results = Encoding.Default.GetString(unityWebRequest.downloadHandler.data);
            JObject json = JObject.Parse(results);

            if (json["status"].ToString() == "ok")
            {
                sceneState.callbackLogin();
            }
        }
    }
}
