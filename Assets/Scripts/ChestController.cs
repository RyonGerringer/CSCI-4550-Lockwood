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

    private Vector3 SPAWNLOCATION1, SPAWNLOCATION2, SPAWNLOCATION3, SPAWNLOCATION4;


    // Start is called before the first frame update
    void Start()
    {
        RandomSpawnChest(Random.Range(1, 5));
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
    // and this treasure chest is less than or equal to two. And, false otherwise.
    private bool InOpeningDistance()
    {

        return Vector2.Distance(PlayerController.transform.position, this.transform.position) <= 2.0f;

    }

    private void RandomSpawnChest(int randInt)
    {
        SPAWNLOCATION1 = new Vector3((float)11.46, (float)5.46, 0);
        SPAWNLOCATION2 = new Vector3((float)11.46, (float)-5.46, 0);
        SPAWNLOCATION3 = new Vector3((float)-11.46, (float)5.46, 0);
        SPAWNLOCATION4 = new Vector3((float)5.65, (float)6.35, 0);
        switch (randInt)
        {
            case 1:
                this.transform.position = SPAWNLOCATION1;
                break;
            case 2:
                this.transform.position = SPAWNLOCATION2;
                break;
            case 3:
                this.transform.position = SPAWNLOCATION3;
                break;
            case 4:
                this.transform.position = SPAWNLOCATION4;
                break;
        }
    }
}
