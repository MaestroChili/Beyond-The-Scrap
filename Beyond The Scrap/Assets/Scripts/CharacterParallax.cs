using UnityEngine;

public class CharacterParallax : MonoBehaviour
{
    public Transform character;
    public float offsetMultiplier = 0.1f;
    public float smoothTime = .3f;

    private Vector2 startPosition;
    private Vector2 characterStartPosition;
    private Vector3 velocity;

    private void Start()
    {
        startPosition = transform.position;
        characterStartPosition = character.position;
    }

    private void Update()
    {
        Vector2 offset = (Vector2)character.position - characterStartPosition;
        transform.position = Vector3.SmoothDamp(transform.position, startPosition + (offset * offsetMultiplier), ref velocity, smoothTime);
    }
}