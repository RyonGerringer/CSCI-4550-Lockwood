
using UnityEngine;

public class Ammo : MonoBehaviour
{
    //Amount of damage done to enemy
    public int damageDone;

    //Called if another object interacts with collider for ammo gameobject
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // See if the enemy has collided with the player
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get a reference to the colliding player object
            Enemy Enemy = collision.gameObject.GetComponent<Enemy>();

            // If coroutine is not currently executing
            StartCoroutine(Enemy.DamageCharacter(damageDone, 1.0f));
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Collision"))
        {
            // Delete the ammo object
            gameObject.SetActive(false);
        }
    }
}
