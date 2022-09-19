using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public GameObject musicSource;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = musicSource.GetComponent<AudioSource>();

        if (getMusicState() == 1)
        {
            audioSource.Stop();
        }
    }

    void Awake()
    {
        GameObject[] musicSources = GameObject.FindGameObjectsWithTag("Music");

        if (musicSources.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private int getMusicState()
    {
        return PlayerPrefs.GetInt("music");
    }
}
