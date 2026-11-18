using System.Collections;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] private float slideDistance = 2f;
    [SerializeField] private float slideDuration = 1f;
    [SerializeField] private Collider2D doorCollider;

    private Vector3 closedPosition;
    private bool isOpen = false;

    void Start()
    {
        closedPosition = transform.position;
    }

    public void OpenDoor()
    {
        if (isOpen)
        {
            return;
        }

        isOpen = true;
        StartCoroutine(SlideDoor());
    }

    IEnumerator SlideDoor()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = closedPosition + Vector3.right * slideDistance;

        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            transform.position = Vector3.Lerp(
                startPosition,
                endPosition,
                elapsedTime / slideDuration
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;

        doorCollider.enabled = false;
    }
}