using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // The target the camera will follow (your dragon)
    public Transform target;

    // The smooth speed with which the camera will follow the target
    public float smoothSpeed = 0.125f;

    // The offset from the target's position (controls where the camera sits relative to the dragon)
    public Vector3 offset;

    // Use LateUpdate for camera movement to ensure the target has moved first
    void LateUpdate()
    {
        if (target == null) return;

        // Calculate the desired position for the camera
        Vector3 desiredPosition = target.position + offset;

        // Ensure the camera only moves in the X and Y plane for 2D, keeping the Z constant
        // The camera should remain at its original Z position to maintain view distance
        desiredPosition.z = transform.position.z;

        // Smoothly move the camera from its current position to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
