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
    // Progress only Count in One Run
    public bool inOneRun;
    // If type == score, Int ScoreTarget Will Shown
    public int scoreTarget;
    // If type == collect, Act Target and int target will shown
    public CardDataBase actTarget;
    public int collectTarget;
    public bool isCompleted;
    // If type == CollectAndScoreLimit, Status will shown
    public ScoreLimit limit;
    // If type == StatusAndStatus, Status will shown
    public Status data;

    // Progress
    public int playerProgress;

    public Costume reward;
}

[System.Serializable]
public class ScoreLimit
{
    public int minScoreLimit;
    public int maxScoreLimit;
}

public enum AchievementType
{
    Collect,
    Score,
    StatusAndScore, // One Run Only !
    StatusAndStatus // One Run Only !
}

[System.Serializable]
public class Status
{
    public Age ageStatus;
    public EducationStage educationStatus;
    public JobLevel jobStatus;
    public LoverStage loverStatus;
}