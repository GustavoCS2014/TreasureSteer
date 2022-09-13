using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    public List<GameObject> treasures;

    private void Awake()
    {
        treasures.AddRange(GameObject.FindGameObjectsWithTag("Treasure"));
    }

    private void Update()
    {
        if (stats.Health < 1) GameOver();
        if (treasures.Count < 1) Win();
    }

    private void Win()
    {
        Debug.LogWarning("WIN");
        Time.timeScale = 0f;
    }

    private void GameOver()
    {
        Debug.LogWarning("GAME OVER");
        Time.timeScale = 0f;
    }
}
