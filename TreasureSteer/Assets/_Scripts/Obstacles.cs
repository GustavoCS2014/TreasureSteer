using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public int health = 100, points = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemie"))
        {
            if(health > 0) health -= 15;

            if(health <= 0) Destroy(gameObject);
            else Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Chest"))
        {
            Destroy(collision.gameObject);

            points += 10;
        }

        if (collision.gameObject.CompareTag("ChestHealth"))
        {
            Destroy(collision.gameObject);

            if(health < 100) health += 5;

        }
    }
}
