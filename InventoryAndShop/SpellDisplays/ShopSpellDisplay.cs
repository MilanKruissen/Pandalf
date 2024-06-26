using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSpellDisplay : MonoBehaviour
{
    public SO_Spell spell;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI spellName;
    [SerializeField] private TextMeshProUGUI description;

    [SerializeField] private TextMeshProUGUI price;

    [SerializeField] private Image selectedBorder;

    [SerializeField] private GameObject priceDisplay;

    private void Start()
    {
        UpdateSpellInformation();
    }

    public void UpdateSpellInformation()
    {
        icon.sprite = spell.icon;
        spellName.text = spell.spellName;
        description.text = spell.description;
        price.text = spell.price.ToString();
    }

    public void SelectSkill()
    {
        SoundManager.PlaySound(SoundManager.Sounds.ButtonClicked, 0.8f);

        ShopSpellDisplay[] allSpellDisplays = FindObjectsOfType<ShopSpellDisplay>();

        foreach (ShopSpellDisplay spellDisplay in allSpellDisplays)
        {
            spellDisplay.DeSelectSkill();
        }

        selectedBorder.enabled = true;

        FindObjectOfType<UI_InfoTabShop>().SkillIsSelected(spell);
    }

    public void DeSelectSkill()
    {
        selectedBorder.enabled = false;
    }
}
