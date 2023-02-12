using UnityEngine;

[RequireComponent(typeof(Animator))]

public class EnemyDamage : Damaging
{
    private Animator animator;

    private string IsAttacking = "IsAttacking";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void applyDamage(GameObject target)
    {
        if(target.TryGetComponent(out Damagable health))
        {
            health.ApplyDamage(10);
            animator.SetTrigger(IsAttacking);
        }
    }
}