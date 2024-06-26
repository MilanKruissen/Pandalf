using System;
using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private int level;
    private int experience;
    private int experienceToLevelUp;

    public int skillPoints;

    [SerializeField] private GameObject LevelUpPopup;

    public PlayerLevelSystem()
    {
        level = 0;
        experience = 0;
        experienceToLevelUp = 100;
    }

    private void Awake()
    {
        FindObjectOfType<LevelWindow>().SetLevelSystem(this);
    }

    public void AddExperience(int amount)
    {
        experience += amount;

        if (experience >= experienceToLevelUp)
        {
            LevelUp();
        }

        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }

    private void LevelUp()
    {
        SoundManager.PlaySound(SoundManager.Sounds.LevelUp, 0.6f);

        level++;
        skillPoints += 1;
        experience -= experienceToLevelUp;

        EnableLevelUpPopup();

        Invoke("DisableLevelUpPopup", 3);

        if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
    }

    public int GetCurrentLevel()
    {
        return level;
    }

    public float GetExperienceNormalized()
    {
        return (float)experience / experienceToLevelUp;
    }

    private void EnableLevelUpPopup()
    {
        LevelUpPopup.SetActive(true);
    }

    private void DisableLevelUpPopup()
    {
        LevelUpPopup.SetActive(false);
    }
}
