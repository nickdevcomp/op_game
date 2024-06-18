using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class OldLiftExitE : MonoBehaviour
{
    public Transform player;
    public Transform hintObject;
    public float hintDistance;
    private void Update()
    {
        if (Inventory.Key == 1)
        {
            var distance = Vector2.Distance(player.position, transform.position);
            if (!Pause.isPaused)
                hintObject.gameObject.SetActive(distance <= hintDistance);
        }
    }
}
