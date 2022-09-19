using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameManager _gameManager;

    public bool explode;
    private bool activeEnabled = false;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(explode) explosion();
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

    public void explosion()
    {
        this.gameObject.GetComponentInParent<PolygonCollider2D>().enabled = false;
        this.gameObject.transform.localScale = Vector3.one * 10;
        _animator.SetBool("Triggered", true);
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
