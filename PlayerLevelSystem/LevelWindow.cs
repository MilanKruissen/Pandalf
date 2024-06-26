using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image experienceBarImage;
    private PlayerLevelSystem levelSystem;

    public void AddExperienceButton()
    {
        levelSystem.AddExperience(45);
    }

    private void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBarImage.fillAmount = experienceNormalized;
    }

    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = (levelNumber + 1).ToString();
    }

    public void SetLevelSystem(PlayerLevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        SetLevelNumber(levelSystem.GetCurrentLevel());
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevelNumber(levelSystem.GetCurrentLevel());
    }
}
