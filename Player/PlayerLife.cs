using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public float _maxHealth = 100;
    public float _health;
    
    private Coroutine _damageCoroutine;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        _health -= damageAmount;

        FindObjectOfType<EffectsCamera>().StartCoroutine(FindObjectOfType<EffectsCamera>().Shaking());

        if (_health <= 0)
        {
            Die();
        }
    }
    
    public void StartLosingHealth(float damagePerSecond)
    {
        if (_damageCoroutine == null)
        {
            _damageCoroutine = StartCoroutine(LoseHealthOverTime(damagePerSecond));
        }
    }

    public void StopLosingHealth()
    {
        if (_damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);
            _damageCoroutine = null;
        }
    }

    private IEnumerator LoseHealthOverTime(float _damagePerSecond)
    {
        while (true)
        {
            TakeDamage(_damagePerSecond);
            yield return new WaitForSeconds(1f);
        }
    }

    private void Die()
    {
        Debug.Log("Dead ");

        SceneManager.LoadScene("DeathScene");

        FindObjectOfType<PlayerMovement>().speed = FindObjectOfType<PlayerMovement>().baseSpeed;
        Destroy(gameObject);
    }
}
