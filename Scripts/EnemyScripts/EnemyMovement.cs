using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private FrontCheck frontCheck;
    [SerializeField] private EdgeCheck edgeCheck;

    [SerializeField] private float jumpForce = 6;
    [SerializeField] private float speed = 8;

    private Rigidbody2D rigidbody;

    private float origSpeed;

    private bool canJump = true;
    private bool isFacingRight = false;
    private void Start()
    {
        origSpeed = speed;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SpeedUp(!IsGrounded());
        if (Mathf.Round(rigidbody.velocity.x) != 0) Flip();
    }

    private void Flip()
    {
        if (isFacingRight != rigidbody.velocity.x > 0)
        {
            isFacingRight = !isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            edgeCheck.Flip();
            frontCheck.Flip();
        }
    }

    private void SpeedUp(bool da)
    {
        speed = da ? origSpeed * 1.5f : origSpeed;
    }

    private bool IsGrounded()
    {
        return groundCheck.IsGrounded;
    }

    public void Move(float direction)
    {
        rigidbody.velocity = new Vector2(direction * speed, rigidbody.velocity.y);
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            canJump = false;

            SpeedUp(true);

            StartCoroutine(jumpCooldown());
        }
    }

    public bool NeedToJump(Vector2 target)
    {
        return NeedToJump() && target.y >= transform.position.y && IsGrounded();
    }

    public bool NeedToJump()
    {
        return frontCheck.InFrontOf() || edgeCheck.IsOnEdge();
    }

    public bool CanJump()
    {
        return groundCheck.IsGrounded && canJump;
    }
    
    private IEnumerator jumpCooldown()
    {
        yield return new WaitForSeconds(1f);
        canJump = true;
    }

}
