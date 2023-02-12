using UnityEngine;

public class EdgeCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private bool isOnEdge;
    [SerializeField] private float distance;

    Vector2 down = new Vector2(-0.5f, -1f);

    private void Update()
    {
        IsOnEdge();
    }

    public bool IsOnEdge()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, down,distance,groundLayer);

        return hit.collider == null;
    }

    public void Flip()
    {
        down.x *= -1;
    }
}
