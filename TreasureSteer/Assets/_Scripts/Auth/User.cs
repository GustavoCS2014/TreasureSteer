using Gravitons.UI.Modal;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class User : MonoBehaviour
{
    public string data;
    public void getData()
    {
        StartCoroutine(getUser());
    }

    IEnumerator getUser()
    {
        string token = getToken();
        JObject json = new();

        UnityWebRequest unityWebRequest = UnityWebRequest.Get("http://treasure-steer-api.test/api/user");
        unityWebRequest.SetRequestHeader("Authorization", "Bearer " + token);

        yield return unityWebRequest.SendWebRequest();

        if(unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
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
                data = json["data"].ToString();

                setLevel(json["data"]["current_level"].ToString());
            }
        }
    }

    private string getToken()
    {
        return PlayerPrefs.GetString("token");
    }

    private void setLevel(string lvl)
    {
        PlayerPrefs.SetString("level", lvl);
    }
}
