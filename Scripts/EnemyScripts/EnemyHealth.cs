using UnityEngine;

public class EnemyHealth : Damagable
{
    [SerializeField] private int maxHealth = 100;

    [SerializeField] private GameObject deathPrefab;

    private int health;

    private void Start()
    {
        health = maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public override void ApplyDamage(int amount)
    {
        
        if (health - amount <= 0) health = 0;
        else health -= amount;

        if (health == 0)
        {
            Instantiate(deathPrefab, transform.position,deathPrefab.transform.rotation);
            Destroy(gameObject);
        }
    }
}
