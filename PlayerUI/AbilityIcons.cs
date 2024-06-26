using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIcons : MonoBehaviour
{
    [SerializeField] private Image abilityOne;
    [SerializeField] private Image abilityTwo;
    [SerializeField] private Image abilityThree;

    [SerializeField] private Image abilityOneCooldown;
    [SerializeField] private Image abilityTwoCooldown;
    [SerializeField] private Image abilityThreeCooldown;


    private PlayerStaff playerStaff;

    private void Start()
    {
        playerStaff = FindObjectOfType<PlayerStaff>();

        abilityOneCooldown.enabled = false;
        abilityTwoCooldown.enabled = false;
        abilityThreeCooldown.enabled = false;

    }

    public void UpdateAbilityIcons()
    {
        if (playerStaff._PrimarySpell != null)
        {
            abilityOne.sprite = playerStaff._PrimarySpell.icon;
            abilityOneCooldown.sprite = playerStaff._PrimarySpell.icon;
            abilityOneCooldown.enabled = true;
            abilityOne.enabled = true;
        }
        else
        {
            abilityOne.sprite = null;
            abilityOneCooldown.sprite = null;
            abilityOneCooldown.enabled = false;
            abilityOne.enabled = false;
        }

        if (playerStaff._SecondarySpell != null)
        {
            abilityTwo.sprite = playerStaff._SecondarySpell.icon;
            abilityTwoCooldown.sprite = playerStaff._SecondarySpell.icon;
            abilityTwoCooldown.enabled = true;
            abilityTwo.enabled = true;
        }
        else
        {
            abilityTwo.sprite = null;
            abilityTwoCooldown.sprite = null;
            abilityTwoCooldown.enabled = false;
            abilityTwo.enabled = false;
        }

        if (playerStaff._ThirdSpell != null)
        {
            abilityThree.sprite = playerStaff._ThirdSpell.icon;
            abilityThreeCooldown.sprite = playerStaff._ThirdSpell.icon;
            abilityThreeCooldown.enabled = true;
            abilityThree.enabled = true;
        }  
        else
        {
            abilityThree.sprite = null;
            abilityThreeCooldown.sprite = null;
            abilityThreeCooldown.enabled = false;
            abilityThree.enabled = false;
        }
    }

    private void Update()
    {
        abilityOneCooldown.fillAmount = playerStaff._spellCooldownsTimers[0] / playerStaff._spellCooldowns[0];
        abilityTwoCooldown.fillAmount = playerStaff._spellCooldownsTimers[1] / playerStaff._spellCooldowns[1];
        abilityThreeCooldown.fillAmount = playerStaff._spellCooldownsTimers[2] / playerStaff._spellCooldowns[2];
    }
}
