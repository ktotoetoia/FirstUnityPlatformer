using UnityEngine;

public class PlayerDamage : Damaging
{
    [SerializeField] private Rigidbody2D rigidbody;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyAI enemyAI))
        {
            applyDamage(collision.gameObject);
        }
    }

    public override void applyDamage(GameObject target)
    {
        Damagable enemyHealth = target.GetComponent<Damagable>();

        enemyHealth.ApplyDamage((int)Mathf.Abs(rigidbody.velocity.y) * damage);

        rigidbody.AddForce(Vector2.up*-rigidbody.velocity.y, ForceMode2D.Impulse);
    }
}
