using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VillageUI : MonoBehaviour
{
    [SerializeField] private GameObject UIContainer;
    [SerializeField] private GameObject characterWindow;
    [SerializeField] private GameObject shopWindow;

    [SerializeField] private GameObject playerUI;

    public TextMeshProUGUI skillPointsAmountTxt;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CloseUI();
        }
    }

    public void CharacterBtn()
    {
        SoundManager.PlaySound(SoundManager.Sounds.ButtonClicked, 0.8f);

        characterWindow.SetActive((true));
        shopWindow.SetActive(false);
    }

    public void ShopBtn()
    {
        SoundManager.PlaySound(SoundManager.Sounds.ButtonClicked, 0.8f);

        characterWindow.SetActive((false));
        shopWindow.SetActive(true);
    }
    
    public void CloseBtn()
    {
        SoundManager.PlaySound(SoundManager.Sounds.ButtonClicked, 0.8f);

        CloseUI();
    }

    private void CloseUI()
    {
        FindObjectOfType<PlayerStaff>().canShoot = true;
        
        UIContainer.SetActive(false);
        playerUI.SetActive(true);

        FindObjectOfType<AbilityIcons>().UpdateAbilityIcons();
    }

    public void OpenVillageUI()
    {
        UIContainer.SetActive(true);
        playerUI.SetActive(false);

        skillPointsAmountTxt.text = FindObjectOfType<PlayerLevelSystem>().skillPoints.ToString();
    }
}
