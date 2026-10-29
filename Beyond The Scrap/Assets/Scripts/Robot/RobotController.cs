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
    public float coyoteTime = 0.1f;       // grace after leaving ground
    public float jumpBufferTime = 0.1f;   // grace before landing
    
    // Input thingy
    public InputActions inputActions;


    // Collider Settings
    private Collider2D capsuleCollider;
    private bool grounded;
    private float vertical_raycast_offset;


    public Vector2 velocity;
    public RobotState currentState;


    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        currentHealth = maxHealth;
        velocity = Vector3.zero;
        vertical_raycast_offset = gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2.0f;
        inputActions = new InputActions();
    }

    private void Start()
    {
        SwitchState(new Walking(this));
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Character is on the ground
        // Cast a ray from the center bottom of character 0.0015 units down
        // Only Interacts with Objects in the Ground Layer
        RaycastHit2D groundHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - vertical_raycast_offset), Vector2.down, 0.0015f, LayerMask.GetMask("Ground"));
        if (groundHit) {
            grounded = true;
        } else { grounded = false; }

        // Run the update function in the current state
        if (currentState != null) {
            currentState.Update();
        }

        // Apply position update based on current velocity every frame
        //Debug.Log(velocity);
        transform.Translate(velocity * Time.deltaTime);

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
}
