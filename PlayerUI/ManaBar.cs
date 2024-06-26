using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Image ManabarImage;
    [SerializeField] private Image EaseManabarImage;

    [SerializeField] private float easeManabarSpeed;

    private PlayerStaff playerStaff;
    
    private void Start()
    {
        playerStaff = FindObjectOfType<PlayerStaff>();
    }

    private void Update()
    {
        if (playerStaff != null && ManabarImage.fillAmount != playerStaff._Mana)
        {
            ManabarImage.fillAmount = playerStaff._Mana / playerStaff._MaxMana;
        }

        if (playerStaff != null && ManabarImage.fillAmount != EaseManabarImage.fillAmount)
        {
            // Calculate the target fill amount
            float targetFillAmount = playerStaff._Mana / playerStaff._MaxMana;

            // Smoothly interpolate the fill amount
            EaseManabarImage.fillAmount = Mathf.Lerp(EaseManabarImage.fillAmount, targetFillAmount, Time.deltaTime * easeManabarSpeed);
        }
    }
}
