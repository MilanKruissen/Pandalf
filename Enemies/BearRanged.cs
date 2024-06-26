using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BearRanged : BaseEnemy
{
    private UnityEngine.AI.NavMeshAgent bearRangedAgent;
    private Transform attackTarget;

    [SerializeField] private float range;
    
    [SerializeField] private float fleeRange;
    [SerializeField] private float fleeDistance;
    
    [SerializeField] private float attackRange;

    [SerializeField] private bool isFleeing;
    [SerializeField] private bool isAttacking;

    private Vector3 fleeDestination;

    [SerializeField] private Transform honeySpawnPos;
    [SerializeField] private GameObject honeyProjectile;

    [SerializeField] private Animator anim;
    
    private void Start()
    {
        base.Start();
        
        bearRangedAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        attackTarget = FindObjectOfType<PlayerMovement>().transform;
    }
    
    private new void Update()
    {
        base.Update();
        
        anim.SetFloat("Speed", bearRangedAgent.velocity.magnitude);

        if (DeathFlag)
            return;

        
        float distanceToTarget = Vector3.Distance(transform.position, attackTarget.position);

        if (isFleeing)
        {
            if (Vector3.Distance(transform.position, fleeDestination) <= 1.0f)
            {
                isFleeing = false;
                bearRangedAgent.isStopped = false; // Resume the NavMeshAgent
            }
            return; // Skip the rest of the update while fleeing
        }

        if (distanceToTarget <= fleeRange)
        {
            Flee();
        }
        else if (distanceToTarget <= attackRange)
        {
            Attack();
        }
        else
        {
            Chase();
        }
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);

        anim.SetTrigger("Hit");
    }

    protected override void Die()
    {
        base.Die();

        bearRangedAgent.isStopped = true;
        bearRangedAgent.speed = 0;

        GetComponent<CapsuleCollider>().enabled = false;

        anim.SetTrigger("Dead");
    }

    void Chase()
    {
        if (!isAttacking && !isFleeing)
        {
            bearRangedAgent.SetDestination(attackTarget.position);
        }
    }

    void Attack()
    {
        if (!isAttacking)
        {
            StartCoroutine(AttackCoroutine());
        }
    }
    
    void Flee()
    {
        isFleeing = true;
        Vector3 directionAwayFromPlayer = (transform.position - attackTarget.position).normalized;
        fleeDestination = transform.position + directionAwayFromPlayer * fleeDistance;
        bearRangedAgent.SetDestination(fleeDestination);
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        bearRangedAgent.isStopped = true;

        // Start the 360 rotation
        anim.SetTrigger("Attack");
        
        transform.LookAt(attackTarget);

        // Time until the attack 
        yield return new WaitForSeconds(0.55f);
        
        Instantiate(honeyProjectile, honeySpawnPos.transform.position, transform.rotation);

        SoundManager.PlaySound(SoundManager.Sounds.HoneyThrow, 0.3f);

        yield return new WaitForSeconds(2f);
        
        bearRangedAgent.isStopped = false;
        isAttacking = false;
    }
}
