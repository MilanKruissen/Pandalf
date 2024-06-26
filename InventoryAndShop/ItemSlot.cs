using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isPointerOver;
    public bool isEmpty = true;

    [SerializeField] private int spellIndex;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
    }

    public void EquipSpell(SO_Spell spell)
    {
        isEmpty = false;
        FindObjectOfType<PlayerStaff>().EquipSpell(spellIndex, spell);
    }

    public void DeEquipSpell()
    {
        isEmpty = true;
        FindObjectOfType<PlayerStaff>().DeEquipSpell(spellIndex);
    }
}
