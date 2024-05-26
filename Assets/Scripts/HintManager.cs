using UnityEngine;

public class HintManager : MonoBehaviour
{
    public Transform player;
    public Transform hintObject;
    private readonly float hintDistance = 0.6f;

    private void Update()
    {
        var distance = Vector2.Distance(player.position, transform.position);
        hintObject.gameObject.SetActive(distance <= hintDistance);
    }
}   