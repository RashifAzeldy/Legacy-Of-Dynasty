using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "Achivement")]
public class AchievementScriptableObj : ScriptableObject
{
    public string achievementName;
    [Multiline(3)]
    public string description;
    public AchievementType type;
    // If type == score, Int ScoreTarget Will Shown
    public int scoreTarget;
    // If type == collect, Act Target and int target will shown
    public CardDataBase actTarget;
    public int collectTarget;
}

public enum AchievementType
{
    Collect,
    Score
}