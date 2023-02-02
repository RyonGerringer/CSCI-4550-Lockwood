using UnityEngine;

public class newEnemyController : MonoBehaviour
{

    public float movementSpeed = 3.0f;
    public float viewRange = 10.0f;
    public float attackRange = 2.0f;
    private GameObject player;

    Rigidbody2D rb2d;
    Vector2 movement = new Vector2();


    Animator animator;
    string animationState = "AnimationState";

    enum CharStates
    {
        idle = 1,
        chase = 2,
        attack = 3
    }

    // Start is called before the first frame update
    private void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    private void Update()
    {
        UpdateState();
    }
    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void UpdateState()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Check if the player is within the enemy's view range
        if (distanceToPlayer <= viewRange)
        {
            // Set the state to chase
            animator.SetInteger(animationState, (int)CharStates.chase);
            // Move the enemy towards the player
            movement = (player.transform.position - transform.position).normalized * movementSpeed;

            // Check if the player is within attack range
            if (distanceToPlayer <= attackRange)
            {
                // Set the state to attack
                animator.SetInteger(animationState, (int)CharStates.attack);

            }
        }
        else
        {
            // Set the state to idle
            animator.SetInteger(animationState, (int)CharStates.idle);

        }
    }

    private void MoveCharacter()
    {
        rb2d.velocity = movement;

        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    

}