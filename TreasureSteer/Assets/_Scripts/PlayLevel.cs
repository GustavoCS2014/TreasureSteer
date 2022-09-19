using UnityEngine;

public class PlayLevel : MonoBehaviour
{
    public User user;
    public SceneState sceneState;

    private void Start()
    {
        user.getData();
    }

    public void playCurrent()
    {
        string level = getLevel();

        switch(level)
        {
            case "1":
                sceneState.callbackLvl1();
                break;

            case "2":
                sceneState.callbackLvl2();
                break;

            case "3":
                sceneState.callbackLvl3();
                break;
        }
    }

    private string getLevel()
    {
        return PlayerPrefs.GetString("level");
    }
}
