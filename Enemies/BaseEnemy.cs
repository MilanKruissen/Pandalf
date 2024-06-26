using System.Collections;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IDamageable
{
    // Health System
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _health;
    [SerializeField] protected float damage;

    [SerializeField] private GameObject damagePopUp;

    [SerializeField] private int droppedExperience;
    
    [SerializeField] private bool shouldDie = false;
    protected bool DeathFlag { get; private set; } = false;

    private EnemySpawner _myEnemySpawner;

    private bool isTakingPeriodicDamage = false;

    public int weight;

    protected void Start()
    {
        _health = _maxHealth;
    }

    protected void Update()
    {
        if (shouldDie == true)
        {
            Die();
        }
    }
    
    //Health System
    public virtual void TakeDamage(float damageAmount)
    {
        _health -= damageAmount;

        SoundManager.PlaySound(SoundManager.Sounds.HitSound, 0.12f);

        DamageIndicator indicator = Instantiate(damagePopUp, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(damageAmount);
        
        if (_health <= 0)
        {
            Die();
        }
    }

    private IEnumerator ApplyPeriodicDamage(float damageAmount, float interval, float duration)
    {
        isTakingPeriodicDamage = true;
        float elapsedTime = 0f;
        
        while (elapsedTime < duration && _health > 0)
        {
            TakeDamage(damageAmount);
            yield return new WaitForSeconds(interval);
            elapsedTime += interval;
        }
        
        isTakingPeriodicDamage = false;
    }

    public void StartPeriodicDamage(float damageAmount)
    {
        if (!isTakingPeriodicDamage)
        {
            StartCoroutine(ApplyPeriodicDamage(damageAmount, 0.5f, 4f));
        }
    }

    protected virtual void Die()
    {
        FindObjectOfType<PlayerLevelSystem>().AddExperience(droppedExperience);

        SoundManager.PlaySound(SoundManager.Sounds.KillSound, 1f);

        float randomStepSound = Random.Range(0, 6);
        float volume = 0.8f;

        if (randomStepSound == 0)
        {
            SoundManager.PlaySound(SoundManager.Sounds.GrowlLongOne, volume);
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
        else if (randomStepSound == 5)
        {
            SoundManager.PlaySound(SoundManager.Sounds.GrowlShortVife, volume);
        }

        if (_myEnemySpawner != null) _myEnemySpawner.currentEnemies.Remove(this.gameObject);
        DeathFlag = true;
    }

    // This function assigns a spawner to this enemy when it spawns so the spawner can keep track of how many enemies are still alive in the current wave
    public void SetSpawner(EnemySpawner spawner)
    {
        _myEnemySpawner = spawner;
    }
}
