using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills
{
    public enum Skills
    {
        Fireball,
        LightningStrike,
        FireballRain
    }

    private List<Skills> unlockedSkills;

    public PlayerSkills()
    {
        unlockedSkills = new List<Skills>();
    }

    public void UnlockSkill(Skills skill)
    {
        unlockedSkills.Add(skill);
    }

    public bool IsSkillUnlocked(Skills skill)
    {
        return unlockedSkills.Contains(skill);
    }
}
