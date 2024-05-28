using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static int FearValue = 0;
    private float dumping = 4f;
    private Vector2 offset = new (2f, 1f);
    private bool isLeft;
    
    private Transform player;
    private int lastX;

    [SerializeField] 
    private float leftLimit;
    [SerializeField] 
    private float rightLimit;
    [SerializeField] 
    private float bottomLimit;
    [SerializeField] 
    private float upperLimit;
    

    private void Start()
    {
        offset = new Vector2(Math.Abs(offset.x), offset.y);
        FindPlayer(isLeft);
    }

    private void FindPlayer(bool playerIsLeft)
    {
        player = GameObject.FindGameObjectWithTag("CameraView").transform;
        lastX = Mathf.RoundToInt(player.position.x);
        
        transform.position = playerIsLeft 
            ? new Vector3(player.position.x - offset.x, player.position.y,
            transform.position.z) 
            : new Vector3(player.position.x + offset.x, player.position.y,
            transform.position.z);
    }
    
    private void Update()
    {
        var currentX = Mathf.RoundToInt(player.position.x);
        if (currentX > lastX)
            isLeft = false;
        if (currentX < lastX)
            isLeft = true;
        lastX = Mathf.RoundToInt(player.position.x);

        var target = isLeft 
            ? new Vector3(player.position.x - offset.x, player.position.y, 
                transform.position.z) 
            : new Vector3(player.position.x + offset.x, player.position.y, 
                transform.position.z);

        var currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
        transform.position = currentPosition;
            

        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, upperLimit),
            transform.position.z
        );
        
        if (FearValue == 1) 
        {
            var camTransform = GetComponent<Transform>();
            var originPos = camTransform.localPosition;
            float shakeDur = 1f, shakeAmount = 0.1f, decreaseFact = 1.5f;

            camTransform.localPosition = originPos + UnityEngine.Random.insideUnitSphere * shakeAmount;
        }
    }
}
