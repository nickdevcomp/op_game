using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 0.125f; 
    public Vector3 offset; 

    void FixedUpdate()
    {
        if (target != null)
        {
            var desiredPosition = target.position + offset; 
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); 
            transform.position = smoothedPosition;

            // review(24.05.2024): Мне тут райдер советует выделить переменную var position = transform.position, ее поменять и присвоить transform.position = position;
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -13.7f, 6.95f),
                Mathf.Clamp(transform.position.y, -10, 10),
                transform.position.z
            );
        }
    }
}