using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealScript : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameManager _gameManager;

    private bool activeEnabled = false;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (_gameManager.win) DisableCollider();
        if (_gameManager.lose) DisableCollider();
        if (_gameManager.pause)
        {
            activeEnabled = true;
            DisableCollider();
        }

        if (!_gameManager.pause && activeEnabled)
        {
            activeEnabled = false;
            EnableCollider();
        }
    }

    private void DisableCollider()
    {
        this.GetComponentInParent<PolygonCollider2D>().enabled = false;
    }

    private void EnableCollider()
    {
        this.GetComponentInParent<PolygonCollider2D>().enabled = true;
    }
}
