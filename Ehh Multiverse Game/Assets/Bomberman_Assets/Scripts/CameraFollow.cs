using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float fixedZ = -10f;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 desiredPosition = playerTransform.position + offset;
            desiredPosition.z = fixedZ;

            // Clamp the camera's position
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}


/*using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float minimumY = -10.0F; // a sensible minimum Y value for your game
    public float fixedZ = -10f; // Set this to your preferred Z position for the camera

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 desiredPosition = playerTransform.position + offset;
            desiredPosition.y = Mathf.Max(desiredPosition.y, minimumY);
            desiredPosition.z = fixedZ; // Ensure the camera's Z position is constant

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Debugging information
            Debug.Log($"Camera Position: {transform.position}");
        }
        else
        {
            Debug.LogError("CameraFollow: Player Transform is null.");
        }
    }
}
*/