using UnityEngine;

public class RobotController : MonoBehaviour
{

    private Collider2D capsuleCollider;
    public int maxHealth = 10;
    private int currentHealth;
    private Vector2 velocity;
    private bool grounded;
    private float vertical_raycast_offset;


    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        currentHealth = maxHealth;
        velocity = Vector3.zero;
        vertical_raycast_offset = gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2.0f;
        Debug.Log(vertical_raycast_offset);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Character is on the ground
        // Cast a ray from the center bottom of character 0.0015 units down
        // Only Interacts with Objects in the Ground Layer
        RaycastHit2D groundHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - vertical_raycast_offset), Vector2.down, 0.0015f, LayerMask.GetMask("Ground"));
        if groundHit {
            grounded = true;
        } else { grounded = false }


        
    }
}
