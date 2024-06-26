using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BearHeavy : BaseEnemy
{
    private NavMeshAgent bearHeavyAgent;
    private Transform attackTarget;
    private BoxCollider attackCollisionBox;

    [SerializeField] private Animator anim;
    [SerializeField] private float attackRange;

    private bool isAttacking;

    private new void Start()
    {
        base.Start();

        bearHeavyAgent = GetComponent<NavMeshAgent>();
        attackTarget = FindObjectOfType<PlayerMovement>().transform;
        attackCollisionBox = GetComponent<BoxCollider>();
    }

    private new void Update()
    {
        base.Update();

        anim.SetFloat("Speed", bearHeavyAgent.velocity.magnitude);

        // Check if the enemy died
        if (DeathFlag)
            return;

        // Make the enemy follow the player and attack if it gets close enough
        if (attackTarget != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, attackTarget.position);

            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                StartCoroutine(AttackCoroutine());
                return;
            }
            else if (distanceToPlayer > attackRange && !isAttacking)
            {
                bearHeavyAgent.SetDestination(attackTarget.position);
            }

            if (isAttacking)
            {
                transform.LookAt(attackTarget);
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        bearHeavyAgent.isStopped = true;

        anim.SetTrigger("Attack");

        float randomStepSound = Random.Range(0, 5);
        float volume = 0.8f;

        if (randomStepSound == 0)
        {
            SoundManager.PlaySound(SoundManager.Sounds.GrowlShortVife, volume);
        }
        else if (randomStepSound == 1)
        {
            SoundManager.PlaySound(SoundManager.Sounds.GrowlShortOne, volume);
        }
        else if (randomStepSound == 2)
        {
            SoundManager.PlaySound(SoundManager.Sounds.GrowlShortTwo, volume);
        }
        else if (randomStepSound == 3)
        {
            SoundManager.PlaySound(SoundManager.Sounds.GrowlShortThree, volume);
        }
        else if (randomStepSound == 4)
        {
            SoundManager.PlaySound(SoundManager.Sounds.GrowlShortFour, volume);
        }

        // Time before enabling the attack collision
        yield return new WaitForSeconds(0.4f);
        EnableAttackCollision();

        // Time until disabling the attack collision
        yield return new WaitForSeconds(0.1f);
        DisableAttackCollision();

        yield return new WaitForSeconds(0.6f);

        bearHeavyAgent.isStopped = false;
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerLife>())
        {
            other.GetComponent<PlayerLife>().TakeDamage(damage);

            DisableAttackCollision();

            Debug.Log("Player is hit!");
        }
    }

    private void EnableAttackCollision()
    {
        attackCollisionBox.enabled = true;
    }

    private void DisableAttackCollision()
    {
        attackCollisionBox.enabled = false;
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);

        anim.SetTrigger("Hit");
    }

    protected override void Die()
    {
        base.Die();

        bearHeavyAgent.isStopped = true;
        bearHeavyAgent.speed = 0;
        attackCollisionBox.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        anim.SetTrigger("Dead");
    }
}
