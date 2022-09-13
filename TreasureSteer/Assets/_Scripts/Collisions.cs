using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions: MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerStats stats;
    private bool startCountdown;
    [SerializeField] private bool _teleported;
    [SerializeField] private float _timeLeft, _defaultTimer;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator BombAnimator;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        countdown();
    }

    private void countdown()
    {

        if (startCountdown)
        {
            _timeLeft -= Time.deltaTime;

            if (_timeLeft < 0) _teleported = false;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb")) {
            stats.Health -= 1;
            playerMovement.ResetSpeed();
            playerMovement.Acceleration = 0f;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Treasure")) {
            stats.Points += 10;
            gameManager.treasures.Remove(collision.gameObject);
            playerMovement.IncreaseSpeed();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Heal")) {
            stats.Health += 1;
            Destroy(collision.gameObject);
        }
        
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "TopWall" || collision.gameObject.name == "BottomWall")
        {
            if (!_teleported)
            {
                playerMovement.TeleportPlayerY();
                _timeLeft = _defaultTimer;
                _teleported = true;
            }
        }

        if (collision.gameObject.name == "LeftWall" || collision.gameObject.name == "RightWall")
        {
            if (!_teleported)
            {
                playerMovement.TeleportPlayerX();
                _timeLeft = _defaultTimer;
                _teleported = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) startCountdown = true;
    }
}
