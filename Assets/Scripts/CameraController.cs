using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static int FearValue = 0;
    private float dumping = 4f;
    private Vector2 offset = new (4f, 1f);
    private bool isLeft;
    
    private Transform player;
    private int lastX;
    public static float ShakeAmount;

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

        // review(30.06.2024): Можно еще так было, кмк более читаемо
        var direction = playerIsLeft ? Vector3.left : Vector3.right;
        transform.position = player.position + direction * offset.x;
        
        transform.position = playerIsLeft 
            ? new Vector3(player.position.x - offset.x, player.position.y,
            transform.position.z) 
            : new Vector3(player.position.x + offset.x, player.position.y,
            transform.position.z);
    }
    
    private void Update()
    {
        ShakeAmount = PlayerController.ShakeAmount * (PlayerController.BrokeFearTime - PlayerController.ReflectStartTime) / PlayerController.BrokeFearTime;
        var currentX = Mathf.RoundToInt(player.position.x);
        if (currentX > lastX)
            isLeft = false;
        if (currentX < lastX)
            isLeft = true;
        lastX = Mathf.RoundToInt(player.position.x);

        // review(30.06.2024): Дублируется логика
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

            var shakeVector = UnityEngine.Random.insideUnitSphere * ShakeAmount;
            shakeVector.z = 0;
            
            camTransform.localPosition = originPos + shakeVector;
        }
    }
}
