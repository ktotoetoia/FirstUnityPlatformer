using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    [SerializeField]private bool isGrounded;
    public bool IsGrounded { get{ return isGrounded; } }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = collision != null && (((1<<collision.gameObject.layer) & groundLayer) != 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
