using Newtonsoft.Json.Linq;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Logout : MonoBehaviour
{
    public SceneState sceneState;

    public void logout()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        string token = getToken();
        JObject json = new();

        UnityWebRequest unityWebRequest = UnityWebRequest.Post("http://treasure-steer-api.test/api/user/logout", "");
        unityWebRequest.SetRequestHeader("Authorization", "Bearer " + token);

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
            }
            catch
            {
                Debug.Log("JSON Parse Error");
                yield break;
            }

            if (json["status"].ToString() == "ok")
            {
                deleteTokens();
                sceneState.callbackLogin();
            }
        }
    }

    private string getToken()
    {
        return PlayerPrefs.GetString("token");
    }

    private void deleteTokens()
    {
        PlayerPrefs.DeleteAll();
    }
}
