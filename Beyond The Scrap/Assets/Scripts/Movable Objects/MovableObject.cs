using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private TriggerDetector triggerDetector;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        prompt.SetActive(triggerDetector.playerInRange);

        if (triggerDetector.playerInRange) {
            // display the interaction menu
            if (Input.GetKeyDown(KeyCode.E))
            {
                push();
            }
        }
    }

    public void push() {
        //RobotController robot = triggerDetector.triggeringRobot.GetComponent<RobotController>();
        // get the robots facing direction?
        // give the ball horizontal velocity in the direction the robot is facing.
        Debug.Log("pushed");
    }
}
