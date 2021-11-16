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

    public PlayerData()
    {
        completedAchievementIndex = GameManager.Instance.CheckCompletedAchievementIndex();
        achievementProgress = GameManager.Instance.CheckAchievementProgress();
        unlockedHatIndex = GameManager.Instance.CheckUnlockedHat();
        equipedHatIndex = GameManager.Instance.hatIndex;
    }
}
