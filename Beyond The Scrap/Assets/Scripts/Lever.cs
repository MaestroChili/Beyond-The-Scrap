using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Lever : MonoBehaviour
{
    [Header("Animation")]
    [Tooltip("Frames in order from OFF to ON. Two frames works fine.")]
    [SerializeField] private Sprite[] frames;
    [SerializeField] private float frameTime = 0.08f;

    [Header("Events")]
    [Tooltip("Hook SlidingDoor.OpenDoor up here.")]
    public UnityEvent onActivated;

    private SpriteRenderer sr;
    private bool activated;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (frames != null && frames.Length > 0)
            sr.sprite = frames[0];
    }

    public void Activate()
    {
        if (activated) return;
        activated = true;
        StartCoroutine(FlipRoutine());
    }

    private IEnumerator FlipRoutine()
    {
        if (frames != null)
        {
            for (int i = 1; i < frames.Length; i++)
            {
                sr.sprite = frames[i];
                yield return new WaitForSeconds(frameTime);
            }
        }

        onActivated.Invoke();
    }
}