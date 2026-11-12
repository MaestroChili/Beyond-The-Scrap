using UnityEngine;
using System.Collections;

public class FenceGate : MonoBehaviour
{
    public GameObject prompt;
    public GameObject lockedPrompt;
    public bool isClosed = true;

    private Coroutine hideMessageRoutine;
    
    [SerializeField]
    private Collider2D collider;

    private Collider2D selfCollider;

    [SerializeField]
    private TriggerDetector triggerDetector;

    [SerializeField]
    private Sprite opennedGate;

    private SpriteRenderer sr;

    void Start() {
        selfCollider = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    
    void Update() {
        if (collider != null)
        {
            collider.enabled = !isClosed;
        }

        prompt.SetActive(triggerDetector.playerInRange);

        if (triggerDetector.playerInRange && isClosed) {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isClosed = false;
                // display a massage to the player saying that this door can be openned in another way
                selfCollider.enabled = false;
                ShowLockedMessage();
                sr.sprite = opennedGate;
            }
        }
    }

    private void ShowLockedMessage()
    {
        if (lockedPrompt != null)
        {
            lockedPrompt.SetActive(true);
            hideMessageRoutine = StartCoroutine(HideLockedPromptAfterDelay(3f));
        }
    }

    private IEnumerator HideLockedPromptAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        lockedPrompt.SetActive(false);
        hideMessageRoutine = null;
    }
}