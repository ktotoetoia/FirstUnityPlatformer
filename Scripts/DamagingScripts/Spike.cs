using System.Collections;
using UnityEngine;

public class Spike : Damaging
{
    [SerializeField] private bool takedDamage = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Damagable>(out Damagable damageable)&&!takedDamage)
        {
            applyDamage(collision.gameObject);
        }
    }

    IEnumerator TakedDamage()
    {
        takedDamage = true;
        yield return new WaitForSeconds(1);
        takedDamage = false;
    }

    public override void applyDamage(GameObject target)
    {
        Damagable dam = target.GetComponent<Damagable>();
        dam.ApplyDamage(10);
        StartCoroutine(TakedDamage());
    }
}
