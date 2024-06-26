using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_InfoTabShop : MonoBehaviour
{
    [SerializeField] private GameObject noSkillSelectedScreen;
    [SerializeField] private GameObject skillInfoScreen;

    // Skill info screen information
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI spellName;
    [SerializeField] private TextMeshProUGUI description;

    [SerializeField] private TextMeshProUGUI manaCost;
    [SerializeField] private TextMeshProUGUI cooldown;

    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI splashDamage;
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI range;

    private SO_Spell spell;

    [SerializeField] private GameObject buyBtn;

    private void Start()
    {
        noSkillSelectedScreen.SetActive(true);
        skillInfoScreen.SetActive(false);
    }

    public void SkillIsSelected(SO_Spell selectedSpell)
    {
        spell = selectedSpell;

        UpdateInfoTabInformation();
    }

    private void UpdateInfoTabInformation()
    {
        skillInfoScreen.SetActive(true);
        noSkillSelectedScreen.SetActive(false);

        icon.sprite = spell.icon;
        spellName.text = spell.spellName;
        description.text = spell.description;

        manaCost.text = spell.manaCost.ToString();
        cooldown.text = spell.cooldown.ToString();
        damage.text = spell.Damage.ToString();
        splashDamage.text = spell.SplashDamage.ToString();
        speed.text = spell.Speed.ToString();
        range.text = spell.Range.ToString();

        if (!FindObjectOfType<VillageInventory>().HasSpell(spell))
        {
            EnableBuyButton();
        }
        else
        {
            DisableBuyButton();
        }
    }

    public void BuyBtn()
    {
        if (!FindObjectOfType<VillageInventory>().HasSpell(spell) && spell.price <= FindObjectOfType<PlayerLevelSystem>().skillPoints)
        {
            SoundManager.PlaySound(SoundManager.Sounds.BuySound, 0.5f);

            FindObjectOfType<VillageInventory>().AddSpell(spell);

            FindObjectOfType<PlayerLevelSystem>().skillPoints -= spell.price;

            FindObjectOfType<VillageUI>().skillPointsAmountTxt.text = FindObjectOfType<PlayerLevelSystem>().skillPoints.ToString();

            foreach (ShopSpellDisplay shopSpellDisplay in FindObjectsOfType<ShopSpellDisplay>())
            {
                if (shopSpellDisplay.spell == spell)
                {
                    Destroy(shopSpellDisplay.gameObject);
                }
            }
            
            UpdateInfoTabInformation();
        }
    }

    private void DisableBuyButton()
    {
        buyBtn.SetActive(false);
    }

    private void EnableBuyButton()
    {
        buyBtn.SetActive(true);
    }
}
