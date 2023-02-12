using UnityEngine;

public class FrontCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    private Vector2 frontPosition = new Vector2(-1, 0);

    public bool InFrontOf()
    {
        Debug.DrawRay(transform.position,frontPosition);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, frontPosition, 1.5f, groundLayer);

        return hit.collider != null;
    }

    public void Flip()
    {
        frontPosition.x *= -1;
    }
}
