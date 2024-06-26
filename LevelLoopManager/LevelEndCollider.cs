using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndCollider : MonoBehaviour
{
    private BoxCollider collider;
    private bool bPlayerHitCollider = false;

    [SerializeField] private Animator levelEndAnimator;
    [SerializeField] private GameObject EquipSpellPopup;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() && !bPlayerHitCollider)
        {
            if (SceneManager.GetActiveScene().name == "Village")
            {
                if (FindObjectOfType<PlayerStaff>().hasSpellEquipped())
                {
                    FindObjectOfType<LevelManager>().FadeOut();
                    DisableLevelEndCollider();

                    bPlayerHitCollider = true;
                }
                else
                {
                    EquipSpellPopup.SetActive(true);
                    Invoke("DisablePopup", 4);
                }
            }
            else
            {
                FindObjectOfType<LevelManager>().FadeOut();
                DisableLevelEndCollider();

                bPlayerHitCollider = true;
            }
        }
    }

    private void DisablePopup()
    {
        EquipSpellPopup.SetActive(false);
    }

    public void DisableLevelEndCollider()
    {
        collider = GetComponent<BoxCollider>();
    }

    public void EnableLevelEndCollider()
    {
        collider.isTrigger = true;
        collider.enabled = true;

        if (levelEndAnimator != null)
        {
            levelEndAnimator.SetTrigger("LevelEnded");
        }
    }
}
