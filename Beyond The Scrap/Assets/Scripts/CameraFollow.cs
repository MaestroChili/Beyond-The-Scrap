using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // Drag your Robot here
    public float smoothSpeed = 5f;  // How smoothly the camera catches up
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Keep camera back on Z axis

    // LateUpdate runs after physics and player movement are fully done for the frame
    void LateUpdate()
    {
        if (target == null) return;

        // Find the position the camera should be at
        Vector3 targetPosition = target.position + offset;

        // Smoothly interpolate (lerp) between current camera position and target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Apply the smooth position
        transform.position = smoothedPosition;
    }
}