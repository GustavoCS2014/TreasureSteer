using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public User user;
    public GameObject settingsModal;

    [SerializeField] private TextMeshProUGUI fullName;
    [SerializeField] private TextMeshProUGUI username;
    [SerializeField] private TextMeshProUGUI score;

    public GameObject btnMusicState;
    public GameObject musicSource;
    private AudioSource audioSource;
    public Sprite stateOn;
    public Sprite stateOff;

    // Start is called before the first frame update
    void Start()
    {
        user.getData();
        settingsModal.gameObject.SetActive(false);
    }

    public void openModal()
    {
        settingsModal.gameObject.SetActive(true);

        setData();
    }

    public void closeModal()
    {
        settingsModal.gameObject.SetActive(false);
    }

    private void setData()
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

        fullName.text = json["name"].ToString();
        username.text = json["username"].ToString();
        score.text = json["points"].ToString();
    }

    public void musicManager()
    {
        musicSource = GameObject.FindWithTag("Music");
        audioSource = musicSource.GetComponent<AudioSource>();

        int state = getMusicState();

        if(state == 1)
        {
            btnMusicState.GetComponent<Image>().sprite = stateOn;
            audioSource.Play();
            setMusicState(0);
        } else
        {
            btnMusicState.GetComponent<Image>().sprite = stateOff;
            audioSource.Stop();
            setMusicState(1);
        }
    }

    private int getMusicState()
    {
        return PlayerPrefs.GetInt("music");
    }

    private void setMusicState(int state)
    {
        PlayerPrefs.SetInt("music", state);
    }

    public void exit()
    {
        Application.Quit();
    }
}
