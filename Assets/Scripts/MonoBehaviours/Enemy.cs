using UnityEngine;
using System.Collections;

public class Enemy : Character
{
    float hitPoints;

    // Amount of damage the enemy will inflict when it runs into the player
    public int damageStrength;

    // Reference to a running coroutine
    Coroutine damageCoroutine;

    private void OnEnable()
    {
        ResetCharacter();
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        // Continuously inflict damage until the loop breaks
        while (true)
        {
            // Inflict damage
            hitPoints = hitPoints - damage;

            // Player is dead; kill off game object and exit loop
            if (hitPoints <= 0)
            {
                KillCharacter();
                break;
            }

            if (interval > 0)
            {
                // Wait a specified amount of seconds and inflict more damage
                yield return new WaitForSeconds(interval);
            }
            else
            {
                // Interval = 0; inflict one-time damage and exit loop
                break;
            }
        }
    }

    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
    }

    // Called by the Unity engine whenever the current enemy object's Collider2D makes contact with another object's Collider2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // See if the enemy has collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get a reference to the colliding player object
            Player player = collision.gameObject.GetComponent<Player>();

            // If coroutine is not currently executing
            if (damageCoroutine == null)
            {
                // Start the coroutine to inflict damage to the player every 1 second
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
            }
        }
    }

    // Called by the Unity engine whenever the current enemy object stops touching another object's Collider2D
    private void OnCollisionExit2D(Collision2D collision)
    {
        // See if the enemy has just stopped colliding with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // If coroutine is currently executing
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }
}