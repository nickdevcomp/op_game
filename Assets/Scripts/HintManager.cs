using UnityEngine;

public class HintManager : MonoBehaviour
{
    public Transform player;
    public Transform hintObject;
    public float hintDistance = 0.5f;

    void Update()
    {
        var distance = Vector2.Distance(player.position, transform.position);
        if (distance <= hintDistance)
        {
            hintObject.gameObject.SetActive(true);
        }
        else
        {
            hintObject.gameObject.SetActive(false);
        }
    }
}