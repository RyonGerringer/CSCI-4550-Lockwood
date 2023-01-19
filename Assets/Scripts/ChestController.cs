using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{

    // Reference to TimerController; Allows us to pause the timer.
    public TimerController TimeController;

    // List of available sprite states. Set in inspector.
    public Sprite[] SpriteStates;

    // Player reference.
    public GameObject PlayerController;

    // Toggleable object to handle state toggling for treasure chest.
    private Toggleable ToggleObject;

    // SpriteRenderer reference. Used to change sprite when the chest is toggled.
    private SpriteRenderer RendererObject;

    // Start is called before the first frame update
    void Start()
    {

        this.ToggleObject = ScriptableObject.CreateInstance("Toggleable") as Toggleable;
        this.RendererObject = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && InOpeningDistance())
        {

            ToggleObject.Toggle();

            if (ToggleObject.GetState() == Toggleable.STATE_CLOSED)
            {

                RendererObject.sprite = SpriteStates[1];
                TimeController.Pause();

            }
        }
    }

    // InOpeningDistance() returns true if the distance between the player
    // and this treasure chest is less than or equal to one. And, false otherwise.
    private bool InOpeningDistance()
    {

        return Vector2.Distance(PlayerController.transform.position, this.transform.position) <= 1.0f;

    }
}
