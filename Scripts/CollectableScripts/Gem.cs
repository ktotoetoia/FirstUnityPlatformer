using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Gem : Collectable 
{
    [SerializeField] private int gemCost;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collected && collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth health))
        {
            Collect(collision.gameObject);
        }
    }

    public override void Collect(GameObject obj)
    {
        PlayerEconomy playerEconomy = obj.GetComponent<PlayerEconomy>();

        animator.SetBool(isCollected, true);
        collected = true;
        playerEconomy.AddGem(gemCost);
    }
}
