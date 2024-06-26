using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageInventory : MonoBehaviour
{
    [SerializeField] private List<SO_Spell> ownedSpells = new List<SO_Spell>();
    [SerializeField] private SO_Spell starterSpell;
    
    [SerializeField] private GameObject inventorySpellTemplate;
    [SerializeField] private Transform primarySpellContainer;
    [SerializeField] private Transform specialSpellContainer;

    private void Start()
    {
        if (!ownedSpells.Contains(starterSpell))
        {
            AddSpell(starterSpell);
        }
    }

    public void AddSpell(SO_Spell spell)
    {
        if (!ownedSpells.Contains(spell))
        {
            ownedSpells.Add(spell);
            Debug.Log("Spell added: " + spell.spellName);

            if (spell.isSpecial)
            {
                GameObject inventorySpellDisplay = Instantiate(inventorySpellTemplate, specialSpellContainer);
                inventorySpellDisplay.GetComponent<InventorySpellDisplay>().UpdateInformation(spell);
            }
            else
            {
                GameObject inventorySpellDisplay = Instantiate(inventorySpellTemplate, primarySpellContainer);
                inventorySpellDisplay.GetComponent<InventorySpellDisplay>().UpdateInformation(spell);
            }

        }
        else
        {
            Debug.Log("Spell already owned: " + spell.spellName);
        }
    }

    public bool HasSpell(SO_Spell spell)
    {
        return ownedSpells.Contains(spell);
    }

    private void PopulateInventoryUI()
    {
        // Clear existing icons (if any)
        foreach (Transform child in specialSpellContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (SO_Spell spell in ownedSpells)
        {
            // GameObject inventorySpellDisplay = Instantiate(inventorySpellTemplate, spellContainer);
            // inventorySpellDisplay.GetComponent<InventorySpellDisplay>().UpdateInformation(spell);
        }
    }
}
