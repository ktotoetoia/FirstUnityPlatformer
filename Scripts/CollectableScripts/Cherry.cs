using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Cherry : Collectable
{
    [SerializeField] private int healPower = 10;

    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collected&&collision.gameObject.TryGetComponent<Damagable>(out Damagable health))
        {
            Collect(collision.gameObject);
        }
    }

    public override void Collect(GameObject target)
    {
        Damagable targetHealth = target.GetComponent<Damagable>();
        collected = true;
        animator.SetBool(isCollected,true);
        targetHealth.Heal(healPower);
    }
}
