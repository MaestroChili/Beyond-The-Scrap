using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // Drag your Robot here
    public float smoothSpeed = 5f;  // How smoothly the camera catches up
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Keep camera back on Z axis

    public float lookDistance = 3f; // How far the camera can shift toward the mouse

    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // LateUpdate runs after physics and player movement are fully done for the frame
    void LateUpdate()
    {
        if (target == null) return;

        // Mouse position relative to screen center: (-1, -1) at bottom-left, (1, 1) at top-right
        Vector3 viewport = cam.ScreenToViewportPoint(Input.mousePosition);
        Vector3 mouseDirection = new Vector3((viewport.x - 0.5f) * 2f, (viewport.y - 0.5f) * 2f, 0f);
        mouseDirection = Vector3.ClampMagnitude(mouseDirection, 1f);

        // How far to push the camera toward the mouse
        Vector3 lookOffset = mouseDirection * lookDistance;

        // Find the position the camera should be at
        Vector3 targetPosition = target.position + offset + lookOffset;

        // Smoothly interpolate (lerp) between current camera position and target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Apply the smooth position
        transform.position = smoothedPosition;
    }
}