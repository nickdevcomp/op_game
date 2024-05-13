using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static int sharedValue = 0;
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

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -13.7f, 6.95f),
                Mathf.Clamp(transform.position.y, -10, 10),
                transform.position.z
            );
            
            if (sharedValue == 1) 
            {
                var camTransform = GetComponent<Transform>();
                var originPos = camTransform.localPosition;
                float shakeDur = 1f, shakeAmount = 0.1f, decreaseFact = 1.5f;

                camTransform.localPosition = originPos + UnityEngine.Random.insideUnitSphere * shakeAmount;
            }
        }
        
    }
}