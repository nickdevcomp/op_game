using Unity.VisualScripting;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public Transform player;
    public Transform hintObject;
    
    [SerializeField]
    private float hintDistance = 0.6f;

    private void Update()
    {
        var distance = Vector2.Distance(player.position, transform.position);
        if (!Pause.isPaused)
            hintObject.gameObject.SetActive(distance <= hintDistance);
    }
}   