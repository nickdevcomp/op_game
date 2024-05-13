using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float dumping = 1.5f;
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

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector2(Math.Abs(offset.x), offset.y);
        FindPlayer(isLeft);
    }

    public void FindPlayer(bool playerIsLeft)
    {
        player = GameObject.FindGameObjectsWithTag("Player").First().transform;
        lastX = Mathf.RoundToInt(player.position.x);
        if (playerIsLeft)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y,
                transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y,
                transform.position.z);
        } 
    }
    
    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            var currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > lastX)
                isLeft = false;
            if (currentX < lastX)
                isLeft = true;
            lastX = Mathf.RoundToInt(player.position.x);

            Vector3 target;
            if (isLeft)
            {
                target = new Vector3(player.position.x - offset.x, player.position.y,
                    transform.position.z);
            }
            else
            {
                target = new Vector3(player.position.x + offset.x, player.position.y,
                    transform.position.z);
            }

            var currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;

        }

        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, upperLimit),
            transform.position.z
        );
    }
}
