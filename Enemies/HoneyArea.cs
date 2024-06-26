using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyArea : MonoBehaviour
{
    public float delayBeforeMoveDown = 3f; 
    public float moveDownDuration = 1f; 
    public float moveDownSpeed = 2f;

    private void Start()
    {
        StartCoroutine(MoveDownAndDestroy());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            playerMovement.speed = 100;
            
            other.GetComponent<PlayerLife>().StartLosingHealth(10.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            playerMovement.speed = playerMovement.baseSpeed;
            
            other.GetComponent<PlayerLife>().StopLosingHealth();
        }
    }

    private IEnumerator MoveDownAndDestroy()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeMoveDown);
    
        // Move down for the specified duration
        float elapsedTime = 0f;
        while (elapsedTime < moveDownDuration)
        {
            transform.position += Vector3.down * moveDownSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
