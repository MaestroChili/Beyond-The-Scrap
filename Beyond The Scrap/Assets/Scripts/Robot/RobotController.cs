using UnityEngine;

public class RobotController : MonoBehaviour
{

    // Character Stats
    public float acceleration = 2f;
    public float verticalSpeed = 20f;
    public float horizontalSpeed = 20f;
    public int maxHealth = 10;
    private int currentHealth;
    public float jumpForce = 16f;
    public float jumpBufferCounter;
    public float coyoteCounter;
    public float coyoteTime = 0.1f;       // grace after leaving ground
    public float jumpBufferTime = 0.1f;   // grace before landing
    public float gravity = 10f;
    public bool hasLeftGround = false;
    
    // Input thingy
    public InputActions inputActions;
    public float idleTime;


    // Collider Settings
    private Collider2D capsuleCollider;
    private Rigidbody2D rb;
    public bool grounded;


    public Vector2 velocity;
    public RobotState currentState;

    private Animator anim;
    private SpriteRenderer sr;


    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>(); // Get it automatically
        currentHealth = maxHealth;
        velocity = Vector3.zero;
        inputActions = new InputActions();

        anim = GetComponent<Animator>();
    sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SwitchState(new Idle(this));
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Character is on the ground
        // Cast a ray from the center bottom of character 0.0015 units down
        // Only Interacts with Objects in the Ground Layer
        grounded = Physics2D.Raycast(capsuleCollider.bounds.center, Vector2.down, capsuleCollider.bounds.extents.y + 0.015f, LayerMask.GetMask("Ground"));


        if (inputActions.Player.Jump.WasPressedThisFrame()) {
            jumpBufferCounter = jumpBufferTime;
        } else {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (grounded) {
            coyoteCounter = coyoteTime;
        } else {
            coyoteCounter -= Time.deltaTime;
        }

        // Inside RobotController.cs Update()
        if (!(currentState is RobotDialogueState))
        {
            if (!grounded || velocity.y > 0) {
                velocity.y -= gravity * Time.deltaTime;
            } 
            else if (velocity.y < 0) {
                velocity.y = 0;
            }
        }          
    }

    private void FixedUpdate() {
        transform.Translate(velocity * Time.deltaTime);

        // Run the update function in the current state
        if (currentState != null) {
            currentState.Update();

            StopAtWall();

            anim.SetFloat("speed", Mathf.Abs(velocity.x));

            if (velocity.x > 0.1f) sr.flipX = false;
            else if (velocity.x < -0.1f) sr.flipX = true;
        }    
    }


    private void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
        inputActions.Disable();
    }


    // SwitchState(nextState)
    // Parameters:  newState switch to this state
    public void SwitchState(RobotState nextState)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }
        currentState = nextState;
        if (currentState != null)
        {
            gameObject.name = $"Robot - {currentState.GetType().Name}";
            currentState.OnStateEnter();
        }
    }

    private void StopAtWall() {
        // Stop horizontal velocity if we're pushing into a wall in the direction we're facing
        float facing = sr.flipX ? -1f : 1f;
        bool movingIntoWall = Mathf.Abs(velocity.x) > 0.01f && Mathf.Sign(velocity.x) == facing;

        if (movingIntoWall) {
            Vector2 boxSize = new Vector2(capsuleCollider.bounds.size.x - 0.05f, capsuleCollider.bounds.size.y * 0.8f);
            RaycastHit2D wallHit = Physics2D.BoxCast(capsuleCollider.bounds.center, boxSize, 0f, new Vector2(facing, 0), Mathf.Abs(velocity.x) * Time.deltaTime + 0.06f, LayerMask.GetMask("Ground"));
            if (wallHit.collider) {
                velocity.x = 0;
            }
        }
    }

    public bool IsInDialogueState()
    {
        return currentState is RobotDialogueState;
    }
}

