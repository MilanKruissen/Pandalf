using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySpellDisplay : MonoBehaviour
{
    [SerializeField] private Image icon;

    [SerializeField] private Image selectedBorder;

    public SO_Spell spell;

    public void UpdateInformation(SO_Spell spell)
    {
        this.spell = spell;
        icon.sprite = spell.icon;
    }

    public void SelectSkill()
    {
        SoundManager.PlaySound(SoundManager.Sounds.ButtonClicked, 0.8f);

        InventorySpellDisplay[] allSpellDisplays = FindObjectsOfType<InventorySpellDisplay>();

        foreach (InventorySpellDisplay spellDisplay in allSpellDisplays)
        {
            spellDisplay.DeSelectSkill();
        }

        selectedBorder.enabled = true;

        FindObjectOfType<SpellInfoWindow>().SkillIsSelected(spell);
    }

    public void DeSelectSkill()
    {
        selectedBorder.enabled = false;
    }
}
