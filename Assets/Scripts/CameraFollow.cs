using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 0.125f; 
    public Vector3 offset; 

    public void Update()
    {
        if (target != null)
        {
            var desiredPosition = target.position + offset; 
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); 
            transform.position = smoothedPosition;

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -14.05f, 6.95f),
                Mathf.Clamp(transform.position.y, -10, 10),
                transform.position.z
            );
        }
        
    }
}