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
        SelectLevel
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

    public void callbackGameplay()
    {
        sceneSwitcher(Scene.Gameplay);
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
        }
    }
}
