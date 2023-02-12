using UnityEngine;

public class FrontObjectCheck : MonoBehaviour
{
    public bool isTouchingWalls;
    public bool IsTouchingWalls { get{ return isTouchingWalls; } }

    [SerializeField] private LayerMask groundLayer;
    private void OnTriggerStay2D(Collider2D collision)
    {
        isTouchingWalls = collision != null && (((1 << collision.gameObject.layer) & groundLayer) != 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouchingWalls = false;
    }
}
