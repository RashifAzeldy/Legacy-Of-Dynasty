using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<int> completedAchievementIndex = new List<int>();
    public List<int> achievementProgress = new List<int>();

    #region Hat Index
    public List<int> unlockedHatIndex = new List<int>();
    public int equipedHatIndex;
    #endregion

    public PlayerData(List<int> completedAchievementIndex, List<int> achievementProgress, List<int> unlockedHatIndex, int equipedHatIndex)
    {
        this.completedAchievementIndex = completedAchievementIndex;
        this.achievementProgress = achievementProgress;
        this.unlockedHatIndex = unlockedHatIndex;
        this.equipedHatIndex = equipedHatIndex;
    }
}
