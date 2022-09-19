using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneState : MonoBehaviour
{
    public enum Scene
    {
        Login,
        Register,
        Gameplay,
        MainMenu,
        SelectLevel,
        Scoreboard,
        Lvl1,
        Lvl2,
        Lvl3
    }

    public void callbackRegister()
    {
        sceneSwitcher(Scene.Register);
    }

    public void callbackLogin()
    {
        sceneSwitcher(Scene.Login);
    }

    public void callbackMainMenu()
    {
        sceneSwitcher(Scene.MainMenu);
    }

    public void callbackSelectLevel()
    {
        sceneSwitcher(Scene.SelectLevel);
    }

    public void callbackScoreboard()
    {
        sceneSwitcher(Scene.Scoreboard);
    }

    public void callbackLvl1()
    {
        sceneSwitcher(Scene.Lvl1);
    }

    public void callbackLvl2()
    {
        sceneSwitcher(Scene.Lvl2);
    }

    public void callbackLvl3()
    {
        sceneSwitcher(Scene.Lvl3);
    }

    public void sceneSwitcher(Scene scene)
    {
        switch (scene)
        {
            case Scene.Login:
                SceneManager.LoadScene("Login");
                break;

            case Scene.Register:
                SceneManager.LoadScene("Register");
                break;

            case Scene.Gameplay:
                SceneManager.LoadScene("Gameplay");
                break;

            case Scene.MainMenu:
                SceneManager.LoadScene("MainMenu");
                break;

            case Scene.SelectLevel:
                SceneManager.LoadScene("SelectLevel");
                break;

            case Scene.Scoreboard:
                SceneManager.LoadScene("Scoreboard");
                break;

            case Scene.Lvl1:
                SceneManager.LoadScene("Lvl1");
                break;

            case Scene.Lvl2:
                SceneManager.LoadScene("Lvl2");
                break;

            case Scene.Lvl3:
                SceneManager.LoadScene("Lvl3");
                break;
        }
    }
}
