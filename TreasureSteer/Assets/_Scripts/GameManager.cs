using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public User user;

    [SerializeField] private PlayerStats stats;
    public List<GameObject> treasures;

    [SerializeField] private TextMeshProUGUI finalScore;
    [SerializeField] private TextMeshProUGUI score;
    public GameObject winModal;
    public GameObject loseModal;
    public GameObject pauseModal;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public Sprite starFull;
    public Sprite starFullLarge;
    public AudioSource glow;

    public GameObject musicSource;
    private AudioSource audioSource;
    public GameObject musicSourceGameplay;
    private AudioSource audioSourceGameplay;

    private string level;

    private int health;
    public bool win = false;
    public bool lose = false;
    public bool pause = false;

    private void Awake()
    {
        treasures.AddRange(GameObject.FindGameObjectsWithTag("Treasure"));
        winModal.SetActive(false);
        loseModal.SetActive(false);
        pauseModal.SetActive(false);

        musicSource = GameObject.FindWithTag("Music");
        audioSource = musicSource.GetComponent<AudioSource>();
        musicSourceGameplay = GameObject.FindWithTag("MusicGameplay");
        audioSourceGameplay = musicSourceGameplay.GetComponent<AudioSource>();
    }

    private void Start()
    {
        user.getData();
        level = getLevel();
        health = stats.Health;

        stopMusicUi();
        playMusicGameplay();
    }

    private void Update()
    {
        if (stats.Health < 1 && !lose)
        {
            lose = true;
            GameOver();
        }

        if (treasures.Count < 1 && !win)
        {
            win = true;
            Win();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pause)
            {
                pauseModal.SetActive(false);

                pause = false;
            } else
            {
                pauseModal.SetActive(true);

                pause = true;
            }  
        }
    }

    private void Win()
    {
       Debug.Log("comin");
       winModal.SetActive(true);

       StartCoroutine(fullStars());
    }

    private void GameOver()
    {
        loseModal.SetActive(true);

        score.text = stats.Points.ToString();
    }

    IEnumerator fullStars()
    {
        if(stats.Health > health)
        {
            finalScore.text = stats.Points.ToString();
            StartCoroutine(sumScore(stats.Points + (int)Math.Round(stats.Points * 1.5f)));

            yield return new WaitForSeconds(0.5f);

            star1.GetComponent<Image>().sprite = starFull;
            glow.Play();
            yield return new WaitForSeconds(0.5f);

            star3.GetComponent<Image>().sprite = starFull;
            glow.Play();
            yield return new WaitForSeconds(0.5f);

            star2.GetComponent<Image>().sprite = starFullLarge;
            glow.Play();
        }

        if (stats.Health < health)
        {
            finalScore.text = stats.Points.ToString();
            StartCoroutine(sumScore(stats.Points + (int)Math.Round(stats.Points * .2f)));

            yield return new WaitForSeconds(0.5f);
            star1.GetComponent<Image>().sprite = starFull;
            glow.Play();
        } 
        
        if(stats.Health == health)
        {
            finalScore.text = stats.Points.ToString();
            StartCoroutine(sumScore(stats.Points + (int)Math.Round(stats.Points * .5f)));

            yield return new WaitForSeconds(0.5f);

            star1.GetComponent<Image>().sprite = starFull;
            glow.Play();
            yield return new WaitForSeconds(0.5f);

            star3.GetComponent<Image>().sprite = starFull;
            glow.Play();
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator sumScore(int total)
    {
        for(int i = stats.Points; i <= total; i++)
        {
            finalScore.text = i.ToString();
            yield return new WaitForSeconds(0.01f);
        }

        stats.Points = total;

        StartCoroutine(updateProgress());
    }

    IEnumerator updateProgress()
    {
        int id = getId();
        int score = getScore();

        WWWForm data = new WWWForm();
        data.AddField("id", id);
        data.AddField("current_level", int.Parse(level) != 3 ? (int.Parse(level) + 1) : 3);
        data.AddField("points", score + stats.Points);

        UnityWebRequest unityWebRequest = UnityWebRequest.Post("http://treasure-steer-api.test/api/user/edit", data);

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(unityWebRequest.result);
        }
        else
        {
            string results = Encoding.Default.GetString(unityWebRequest.downloadHandler.data);
            JObject json = JObject.Parse(results);

            if (json["status"].ToString() != "ok")
            {
                Debug.Log("Update Error!");
            }
        }
    }

    private string getLevel()
    {
        return PlayerPrefs.GetString("level");
    }

    private int getId()
    {
        JObject json = getData();

        return int.Parse(json["id"].ToString());
    }

    private int getScore()
    {
        JObject json = getData();

        return int.Parse(json["points"].ToString());
    }

    private JObject getData()
    {
        JObject json = new();

        try
        {
            json = JObject.Parse(user.data);
        }
        catch
        {
            Debug.Log("JSON Parse Error");
        }

        return json;
    }

    public void resume()
    {
        pauseModal.SetActive(false);
        pause = false;
    }

    public void playMusicUi()
    {
        audioSource.Play();
    }

    public void stopMusicUi()
    {
        audioSource.Stop();
    }

    public void playMusicGameplay()
    {
        audioSourceGameplay.Play();
    }

    public void stopMusicGameplay()
    {
        audioSourceGameplay.Stop();
    }
}
