using UnityEngine;
using UnityEngine.Events;

public class TriggerDetector : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField]
    private string targetTag = "Player";

    public RobotController triggeringRobot;
    public MovableObject triggeringObject;

    [Header("Events")]
    public UnityEvent onTriggerEntered2D;
    public UnityEvent onTriggerExited2D;

    public bool playerInRange { get; private set; }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(targetTag)) {
            triggeringRobot = other.GetComponent<RobotController>();
            playerInRange = true;
            onTriggerEntered2D.Invoke();
        } else if (other.CompareTag("movable object")) {
            triggeringObject = other.GetComponent<MovableObject>();
            onTriggerEntered2D.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag(targetTag)) {
            playerInRange = false;
            onTriggerExited2D.Invoke();
            triggeringObject = null;
            triggeringRobot = null;
        }
    }
}
