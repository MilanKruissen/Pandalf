using System;
using System.Collections;
using UnityEngine;

public class TrainingDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject damagePopUp;

    [SerializeField] private float wiggleDuration = 0.5f; // Total duration of the wiggle effect
    [SerializeField] private float initialWiggleIntensity = 10f; // Initial intensity of the wiggle

    private bool isTakingPeriodicDamage = false;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void TakeDamage(float damageAmount)
    {
        SoundManager.PlaySound(SoundManager.Sounds.HitSound, 0.12f);

        DamageIndicator indicator = Instantiate(damagePopUp, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(damageAmount);

        StartCoroutine(Wiggle());
    }

    private IEnumerator ApplyPeriodicDamage(float damageAmount, float interval, float duration)
    {
        isTakingPeriodicDamage = true;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
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

    private IEnumerator Wiggle()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < wiggleDuration)
        {
            elapsedTime += Time.deltaTime;
            float intensity = Mathf.Lerp(initialWiggleIntensity, 0, elapsedTime / wiggleDuration);
            Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * intensity;
            transform.position = originalPosition + new Vector3(randomOffset.x, randomOffset.y, 0);
            yield return null;
        }

        transform.position = originalPosition; 
    }
}
