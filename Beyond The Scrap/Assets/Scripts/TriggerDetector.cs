using UnityEngine;
using UnityEngine.Events;

public class TriggerDetector : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField]
    private string targetTag = "Player";

    [Header("Events")]
    public UnityEvent onTriggerEntered2D;
    public UnityEvent onTriggerExited2D;

    public bool playerInRange { get; private set; }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(targetTag)) {
            playerInRange = true;
            onTriggerEntered2D.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag(targetTag)) {
            playerInRange = false;
            onTriggerExited2D.Invoke();
        }
    }
}
