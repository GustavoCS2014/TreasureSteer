using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject heart;
    public PlayerStats playerStats;

    void Start()
    {
        createHearts();
    }

    public void createHearts()
    {
        destroyHearts();

        for (int i = 1; i <= playerStats.Health; i++)
        {
            Instantiate(heart, transform).GetComponent<RowUI>();
        }
    }

    private void destroyHearts()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Heart");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
