using UnityEngine;
using System.Collections;

public class Player : Character
{
    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    //===============HH'sCode===============================

    public GameObject gameOverText, restartButton;

    //===============HH'sCode===============================

    public GameObject Timer;

    public void Start()
    {
        //Removes GameOver from the screen and the button 
        //while resetting the character at the restart of the 
        //game
        //===============HH'sCode==================
        gameOverText.SetActive(false);
        restartButton.SetActive(false);
        //================HH'sCode=================
        Timer.SetActive(true);
        ResetCharacter();
    }

    /// <summary>
    /// On collision with an object with tag CanBePickedUp this determines
    /// what should be done
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null)
            {
                bool shouldDisappear = false;
                switch (hitObject.itemType)
                {
                    case Item.ItemType.HEALTH:
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;
                    default:
                        break;
                }

                if (shouldDisappear)
                {
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }

    //adjust hit points as long as its not higher than maxHitPoints
    public bool AdjustHitPoints(int amount)
    {
        if (hitPoints.value < maxHitPoints)
        {
            hitPoints.value = hitPoints.value + amount;
            return true;
        }
        return false;
    }

    public override void ResetCharacter()
    {
        // Start the player off with the starting hit point value
        hitPoints.value = startingHitPoints;

        // Get a copy of the health bar prefab and store a reference to it
        healthBar = Instantiate(healthBarPrefab);

        // Set the healthBar's character property to this character so it can retrieve the maxHitPoints
        healthBar.character = this;
    }

    public override void KillCharacter()
    {
        // Call KillCharacter in parent(Character) class, which will destroy the player game object
        base.KillCharacter();

        // Destroy health and throws up game over screen
        // //================HH'sCode=================
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
        gameObject.SetActive(false);
        //================HH'sCode=================
        Timer.SetActive(false);
        Destroy(healthBar.gameObject);
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            // Inflict damage
            hitPoints.value = hitPoints.value - damage;

            // Player is dead; kill off game object and exit loop
            if (hitPoints.value <= 0)
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
}
