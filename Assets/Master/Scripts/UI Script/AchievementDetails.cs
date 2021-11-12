using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementDetails : MonoBehaviour
{
    [SerializeField] AchievementScriptableObj achievement;
    public AchievementScriptableObj SetAchivementDetails { get { return achievement; } set { achievement = value; } }

    [SerializeField] TextMeshProUGUI achievementName;
    [SerializeField] TextMeshProUGUI achievementDescription;
    [SerializeField] TextMeshProUGUI achievementProgress;
    [SerializeField] TextMeshProUGUI achievementTarget;

    private void Start()
    {
        if (achievement != null)
        {
            achievementName.text = achievement.achievementName;
            achievementDescription.text = achievement.description;
            achievementProgress.text = achievement.playerProgress.ToString();
            if (achievement.type == AchievementType.Collect)
            {
                achievementTarget.text = achievement.scoreTarget.ToString();
            }
            else if (achievement.type == AchievementType.Score)
            {
                achievementTarget.text = achievement.collectTarget.ToString();
            }
        }
    }
}
