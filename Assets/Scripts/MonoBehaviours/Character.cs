using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour
{
    public HitPoints hitPoints;
    public float maxHitPoints;
    public float startingHitPoints;

    public enum CharacterCategory
    {
        PLAYER,
        ENEMY
    }

    public CharacterCategory characterCategory;

    public virtual void KillCharacter()
    {
        // Destroy the current game object and remove it from the scene
        Destroy(gameObject);
    }

    // Set the character back to its original state
    public abstract void ResetCharacter();

    // Coroutine to inflict an amount of damage to the character over a period of time
    // interval = 0 to inflict a one-time damage hit
    // interval > 0 to continuously inflict damage at the set interval of time
    public abstract IEnumerator DamageCharacter(int damage, float interval);
}
