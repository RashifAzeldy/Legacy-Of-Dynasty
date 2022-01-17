using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementDetails : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI achievementName;
    [SerializeField] TextMeshProUGUI achievementDescription;
    [SerializeField] TextMeshProUGUI achievementProgress;
    [SerializeField] TextMeshProUGUI achievementTarget;
    [SerializeField] Slider progressSlider;

    public void SetDetails(AchievementScriptableObj achievement)
    {
        achievementName.text = achievement.achievementName;
        achievementDescription.text = achievement.description;
        achievementProgress.text = achievement.playerProgress.ToString();
        if (achievement.type == AchievementType.Score)
        {
            progressSlider.maxValue = achievement.scoreTarget;
            achievementTarget.text = achievement.scoreTarget.ToString();
        }
        else if (achievement.type == AchievementType.Collect)
        {
            progressSlider.maxValue = achievement.collectTarget;
            achievementTarget.text = achievement.collectTarget.ToString();
        }
        else if (achievement.type == AchievementType.StatusAndScore ||
            achievement.type == AchievementType.StatusAndStatus)
        {
            progressSlider.maxValue = 1;
            achievementTarget.text = progressSlider.maxValue.ToString();
        }
        if (achievement.isCompleted)
        {
            switch (achievement.type)
            {
                case AchievementType.Collect:
                    progressSlider.value = achievement.collectTarget;
                    break;
                case AchievementType.Score:
                    progressSlider.value = achievement.scoreTarget;
                    break;
                case AchievementType.StatusAndScore:
                    progressSlider.value = 1;
                    break;
                case AchievementType.StatusAndStatus:
                    progressSlider.value = 1;
                    break;
            }
            achievementProgress.text = achievement.scoreTarget.ToString();
        }
        else
        {
            progressSlider.value = achievement.playerProgress;
        }

        if (achievement.playerProgress < 0)
        {
            achievement.playerProgress = 0;
            achievementProgress.text = "0";
        }
    }
}
