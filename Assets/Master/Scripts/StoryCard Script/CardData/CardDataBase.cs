using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public enum CardValue { Positive, Negative, Mystery, Neutral }

[System.Serializable]
public class CardDataBase : ScriptableObject
{
    [Header("Card Data Value : ")]

    public string cardName;

    [Multiline(3)]
    public string cardStory;

    public CardValue cardValue;

    [Space]

    [Header("Card Config : ")]

    [Tooltip("Which effort is this card going to be spawn at")]
    public BlockEffort cardSpawnAtEffort;

    [Tooltip("Certain score value that will be added to the player score if being collected")]
    public int cardScoreValue;

    [Space]

    [Header("Spawn Config : ")]
    public PlayerStatus spawnRequirement;

    public UnityEvent OnCardCollected;

}

[System.Serializable]
public class PlayerStatus
{
    public int playerScore;

    public Age playerAge;

    public EducationStage educationStage;

    public LoverStage loverStage;

    public JobData jobData;

    public void SetPlayerAge(Age nextAge) { playerAge = nextAge; }
    public void SetEducationStage(EducationStage nextEducationStage) { educationStage = nextEducationStage; }
    public void SetLoverStage(LoverStage nextLoverStage) { loverStage = nextLoverStage; }
    public void SetJobData(JobData nextJobData) 
    {
        jobData.jobType = nextJobData.jobType;
        jobData.jobLevel = nextJobData.jobLevel;
    }

}
