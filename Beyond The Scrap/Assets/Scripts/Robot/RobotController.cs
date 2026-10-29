using UnityEngine;

public class RobotController : MonoBehaviour
{

    private Collider2D capsuleCollider;
    public int maxHealth = 10;
    private int currentHealth;
    private Vector2 velocity;


    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        currentHealth = maxHealth;
        velocity = Vector3.zero;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
