using Newtonsoft.Json.Linq;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreGlobal : MonoBehaviour
{
    public RowUI rowUi;

    void Start()
    {
        StartCoroutine(getScores());
    }

    IEnumerator getScores()
    {
        JObject json = new();
        int i = 1;

        UnityWebRequest unityWebRequest = UnityWebRequest.Get("http://treasure-steer-api.test/api/score/global");

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

            foreach(var user in json["data"])
            {
                var row = Instantiate(rowUi, transform).GetComponent<RowUI>();
                row.rank.text = i.ToString();
                row.username.text = user["username"].ToString();
                row.points.text = user["points"].ToString();

                yield return new WaitForSeconds(.5f);

                i++;
            }
        }
    }
}
