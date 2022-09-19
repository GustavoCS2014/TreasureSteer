using UnityEngine;
using UnityEngine.UI;

public class UserLevels : MonoBehaviour
{
    public SceneState sceneState;

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;

    public Button btnLvl1;
    public Button btnLvl2;
    public Button btnLvl3;

    public Sprite iconLvl1;
    public Sprite iconLvl2;
    public Sprite iconLvl3;

    private string level;

    void Start()
    {
        level = getLevel();

        showLevels();

        btnLvl1.onClick.AddListener(selectLvl1);
        btnLvl2.onClick.AddListener(selectLvl2);
        btnLvl3.onClick.AddListener(selectLvl3);
    }

    private void showLevels()
    {
        if(level == "2")
        {
            level2.GetComponent<Image>().sprite = iconLvl2;
        }

        if (level == "3")
        {
            level2.GetComponent<Image>().sprite = iconLvl2;
            level3.GetComponent<Image>().sprite = iconLvl3;
        }
    }

    private string getLevel()
    {
        return PlayerPrefs.GetString("level");
    }

    private void selectLvl1()
    {
        sceneState.callbackLvl1();
    }

    private void selectLvl2()
    {
        if(level == "2" || level == "3")
        {
            sceneState.callbackLvl2();
        }
    }

    private void selectLvl3()
    {
        if (level == "3")
        {
            sceneState.callbackLvl3();
        }
    }
}
