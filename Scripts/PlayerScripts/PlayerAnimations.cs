using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerAnimations : MonoBehaviour
{
    
    private string isJumping = "IsJumping";
    private string isFalling = "IsFalling";
    private string isOnGround = "IsOnGround";
    private string isRunning = "IsRunning";
    private string isIdle = "IsIdle";
    private string isHurted = "IsHurted";

    private Rigidbody2D rigidbody;
    private Animator animator;

    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private GameObject Smoke;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (rigidbody.velocity.y < 0) Falling();

        if (groundCheck.IsGrounded) Grounding();
    }
    public void Jumping()
    {
        animator.SetBool(isJumping,true);
        animator.SetBool(isOnGround, false);
    }

    public void Falling()
    {
        animator.SetBool(isFalling, true);
        animator.SetBool(isJumping, false);
    }

    public void Grounding()
    {
        animator.SetBool(isOnGround, true);
        animator.SetBool(isFalling, false);
    }
    
    public void Running()
    {
        animator.SetBool(isRunning, true);
        animator.SetBool(isIdle, false);
    }

    public void Idle()
    {
        animator.SetBool(isIdle, true);
        animator.SetBool(isRunning, false);
    }

    public void Hurted()
    {
        animator.SetBool(isHurted, true);
    }

    public void DoubleJumped()
    {
        Instantiate(Smoke,transform.position,transform.rotation);
    }
}
