using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BearMelee : BaseEnemy
{
    private NavMeshAgent bearMeleeAgent;
    private Transform attackTarget;
    private BoxCollider attackCollisionBox;

    [SerializeField] private Animator anim;

    [SerializeField] private float attackRange;
    [SerializeField] private float jumpBackForce = 0.8f;
    [SerializeField] private float circlingDistance = 2f;
    [SerializeField] private float circlingChance = 0.3f;

    private bool isAttacking;
    private bool isChasingPlayer = true;

    private new void Start()
    {
        base.Start();

        bearMeleeAgent = GetComponent<NavMeshAgent>();
        attackTarget = FindObjectOfType<PlayerMovement>().transform;
        attackCollisionBox = GetComponent<BoxCollider>();

        StartCoroutine(UpdateDestinationCoroutine());
    }

    private new void Update()
    {
        base.Update();

        anim.SetFloat("Speed", bearMeleeAgent.velocity.magnitude);
        
        // Check if the enemy died
        if (DeathFlag)
            return;

        // Make the enemy follow the player and attack if it gets close enough
        if (attackTarget != null && !isAttacking)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, attackTarget.position);
            if (distanceToPlayer <= attackRange)
            {
                StartCoroutine(AttackCoroutine());
                return;
            }
        }

        if (isChasingPlayer && !isAttacking)
        {
            bearMeleeAgent.SetDestination(attackTarget.position);
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

        bearMeleeAgent.isStopped = true;
        bearMeleeAgent.speed = 0;
        attackCollisionBox.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        anim.SetTrigger("Dead");
    }

    private IEnumerator UpdateDestinationCoroutine()
    {
        while (!DeathFlag)
        {
            float randomChance = Random.value;

            // Check if the bear will circle or chase
            if (randomChance < circlingChance)
            {
                isChasingPlayer = false;

                // Calculate a random destination around the bear
                Vector3 randomOffset = Random.insideUnitCircle * circlingDistance;
                Vector3 newDestination = transform.position + new Vector3(randomOffset.x, 0f, randomOffset.y);
                bearMeleeAgent.SetDestination(newDestination);
            }
            else
            {
                isChasingPlayer = true;
            }

            yield return new WaitForSeconds(1.5f); // Update destination every 2 seconds
        }
    }

    

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        bearMeleeAgent.isStopped = true;

        // Start the 360 rotation
        // StartCoroutine(Rotate360());
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
        yield return new WaitForSeconds(0.2f);
        EnableAttackCollision();

        // Time until disabling the attack collision
        yield return new WaitForSeconds(0.2f);
        DisableAttackCollision();

        yield return StartCoroutine(JumpBack());

        bearMeleeAgent.isStopped = false;
        isAttacking = false;

        // Resume updating destination after attacking
        if (isChasingPlayer)
        {
            bearMeleeAgent.SetDestination(attackTarget.position);
        }
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

    private IEnumerator JumpBack()
    {
        anim.SetTrigger("JumpBack");
        
        Vector3 startPosition = transform.position;
        Vector3 jumpDirection = -transform.forward * jumpBackForce;
        Vector3 endPosition = startPosition + jumpDirection;
        float jumpDuration = 0.4f;
        float elapsed = 0f;

        while (elapsed < jumpDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / jumpDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;

        // Optional delay before moving again
        yield return new WaitForSeconds(0.05f);
    }
}