
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public float movementSpeed = 3.0f;

    Rigidbody2D rb2d;
    Vector2 movement = new Vector2();


    Animator animator;
    string animationState = "AnimationState";

    enum CharStates
    {
        idleSouth = 1,
        walkEast = 4,
        walkWest = 5,
        walkNorth = 2,
        walkSouth = 3
    }

    // Start is called before the first frame update
    private void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        UpdateState();
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void UpdateState()
    {
        if (movement.x > 0)
            animator.SetInteger(animationState, (int)CharStates.walkEast);
        else if (movement.x < 0)
            animator.SetInteger(animationState, (int)CharStates.walkWest);
        else if (movement.y > 0)
            animator.SetInteger(animationState, (int)CharStates.walkNorth);
        else if (movement.y < 0)
            animator.SetInteger(animationState, (int)CharStates.walkSouth);
        else
            animator.SetInteger(animationState, (int)CharStates.idleSouth);
    }

    private void MoveCharacter()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        rb2d.velocity = movement * movementSpeed;

    }

}
