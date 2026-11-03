using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    public float offsetMultiplier = 200f;
    public float smoothTime = .3f;

    private Vector2 startPosition;
    private Vector3 velocity;
    private MouseInput mouseInput;

    private void Awake()
    {
        mouseInput = new MouseInput();
    }

    private void OnEnable()
    {
        mouseInput.Enable();
    }

    private void OnDisable()
    {
        mouseInput.Disable();
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        Vector2 mousePosition = mouseInput.MouseActionMap.MouseMovement.ReadValue<Vector2>();
        Vector2 offset = Camera.main.ScreenToViewportPoint(new Vector3(mousePosition.x, mousePosition.y, 0f));
        transform.position = Vector3.SmoothDamp(transform.position, startPosition + (offset * offsetMultiplier), ref velocity, smoothTime);
    }
}