using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions: MonoBehaviour
{
    public int health = 100, points = 0;

    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            if (health > 0) health -= 15;

            if (health <= 0) Destroy(gameObject);
            else Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Treasure"))
        {
            Destroy(collision.gameObject);

            points += 10;
        }

        if (collision.gameObject.CompareTag("Heal"))
        {
            Destroy(collision.gameObject);

            if (health < 100) health += 5;

        }

        if(collision.gameObject.name == "TopWall" || collision.gameObject.name == "BottomWall") 
        {
            playerMovement.TeleportPlayerY();
        }

        if (collision.gameObject.name == "LeftWall" || collision.gameObject.name == "RightWall")
        {
            playerMovement.TeleportPlayerX();
        }
    }   
}
