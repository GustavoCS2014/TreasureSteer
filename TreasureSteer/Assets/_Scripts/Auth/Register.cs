using Gravitons.UI.Modal;
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

    public GameObject blurScene;

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
        JObject json = new();

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

            try
            {
                json = JObject.Parse(results);
            } catch
            {
                blurScene.gameObject.SetActive(true);

                ModalManager.Show("Error", "An error has ocurred, try again!", new[] { new ModalButton() {
                    Text = "OK",
                    Callback = canceledRegister
                } });

                yield break;
            }
            
            if (json["status"].ToString() == "ok")
            {
                blurScene.gameObject.SetActive(true);

                ModalManager.Show("Success!", json["message"].ToString(), new[] { new ModalButton() {
                    Text = "Log In",
                    Callback = successRegister
                } });
            } else
            {
                string message = "";

                foreach(string error in json["message"])
                {
                    message += error + "\n";
                }

                blurScene.gameObject.SetActive(true);

                ModalManager.Show("Error", message, new[] { new ModalButton() {
                    Text = "OK",
                    Callback = canceledRegister
                } });
            }
        }
    }

    private void successRegister()
    {
        sceneState.callbackLogin();
    }

    private void canceledRegister()
    {
        blurScene.gameObject.SetActive(false);
    }
}
