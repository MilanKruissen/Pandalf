using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthbarImage;
    [SerializeField] private Image easeHealthbarImage;

    [SerializeField] private float easeHealthbarSpeed;
    
    private PlayerLife playerLife;
    
    private void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();
        
        if (playerLife == null)
        {
            Debug.LogError("PlayerLife component not found in the scene!");
        }

        if (healthbarImage == null)
        {
            Debug.LogError("HealthbarImage not assigned in the inspector!");
        }

        if (easeHealthbarImage == null)
        {
            Debug.LogError("EaseHealthbarImage not assigned in the inspector!");
        }
    }

    private void Update()
    {
        if (playerLife != null && healthbarImage.fillAmount != playerLife._health)
        {
            healthbarImage.fillAmount = playerLife._health / playerLife._maxHealth;
        }

        if (playerLife != null && healthbarImage.fillAmount != easeHealthbarImage.fillAmount)
        {
            // Calculate the target fill amount
            float targetFillAmount = playerLife._health / playerLife._maxHealth;

            // Smoothly interpolate the fill amount
            easeHealthbarImage.fillAmount = Mathf.Lerp(easeHealthbarImage.fillAmount, targetFillAmount, Time.deltaTime * easeHealthbarSpeed);
        }
    }
}
