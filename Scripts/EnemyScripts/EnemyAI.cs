using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyDamage))]

public class EnemyAI : MonoBehaviour
{
    public bool isDead = false;


    [SerializeField] LayerMask layerMask;

    [SerializeField] private float visibleDistance;
    [SerializeField] private float targetUpdate;

    private Vector2 lastSeeingPosition;
    private Vector2 lastStandingPosition;

    private Vector2 lastTarget;

    private bool forget = true;
    private bool canAttack = true;
    private bool forgetting = false;
    
    private float direction = 0f;
    private float nearestPos = 1f;
    private float attackRange = 1f;

    private PlayerMovement targetMovement;
    private EnemyMovement enemyMovement;
    private EnemyDamage enemyDamage;
    private GameObject target;

    Coroutine forgettingTarget;

    private void Start()
    {
        target = FindObjectOfType<PlayerInput>().gameObject;
        targetMovement = target.GetComponent<PlayerMovement>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyDamage = GetComponent<EnemyDamage>();
    }

    private void Update()
    {
        if (isDead) return;

        SeeingTarget();

        if (InAttackRange() && canAttack)
        {
            StartCoroutine(AttackCooldown());
            enemyDamage.applyDamage(target);
        }

        if (!forget)
        {
            Vector2 currentTarget = GetTargetToMove();

            if (CanMove(currentTarget))
                MoveTo(currentTarget);

            if (enemyMovement.NeedToJump(currentTarget) && enemyMovement.CanJump())
                enemyMovement.Jump();
        }

        else
        {
            Patrol();
        }
    }

    private void MoveTo(Vector2 position)
    {
        StartCoroutine(updateDirection(position));
        if(!NearToPosition(position))
            enemyMovement.Move(direction);
    }

    private bool CanMove(Vector2 currentTarget)
    {
        return (enemyMovement.NeedToJump(currentTarget) && enemyMovement.CanJump() || !enemyMovement.NeedToJump(currentTarget)) && currentTarget != Vector2.zero;
    }

    private void Patrol()
    {
        if (direction == 0) direction = 1;

        if (enemyMovement.NeedToJump())
        {
            direction *= -1;
        }

        enemyMovement.Move(direction);
    }

    private bool SeeingTarget()
    {
        Vector2 targetPosition = target.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPosition,visibleDistance,layerMask);

        if(!hit || hit.rigidbody.gameObject!=target)
        {
            if (!forgetting) forgettingTarget = StartCoroutine(forgetTarget());
        }

        else
        {
            forgetting = false;
            forget = false;

            if(forgettingTarget!=null)
                StopCoroutine(forgettingTarget);

            if (targetMovement.IsGrounded())
                lastStandingPosition = hit.point;
            else 
                lastSeeingPosition = hit.point;

            return true;
        }


        return false;
    }

    private bool InAttackRange()
    {
        return Vector2.Distance(transform.position, target.transform.position) < attackRange;
    }

    private bool NearToPosition(Vector2 position)
    {
        return Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(position.x, 0)) < nearestPos;
    }

    private float GetDirectionTo(Vector2 position)
    {
        if (InAttackRange() || enemyMovement.NeedToJump(position) && !enemyMovement.CanJump())
        {
            return 0;
        }

        return position.x > transform.position.x ? 1 : -1;
    }

    private Vector2 GetTargetToMove()
    {
        Vector2 currentTarget = lastStandingPosition ;


        if (SeeingTarget())
        {
            currentTarget = target.transform.position;
        }

        else
        {
            if (!NearToPosition(lastStandingPosition)&& !NearToPosition(lastSeeingPosition))currentTarget = lastTarget;

            else  if(NearToPosition(lastStandingPosition)) currentTarget = lastSeeingPosition;
        }

        lastTarget = currentTarget;

        return currentTarget;
    }

    private IEnumerator updateDirection(Vector2 target)
    {
        yield return new WaitForSeconds(targetUpdate);
        direction = GetDirectionTo(target);
    }

    private IEnumerator forgetTarget()
    {
        forgetting = true;
        yield return new WaitForSeconds(10);
        forget = true;
        forgetting = false;
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(2);
        canAttack = true;
    }
}