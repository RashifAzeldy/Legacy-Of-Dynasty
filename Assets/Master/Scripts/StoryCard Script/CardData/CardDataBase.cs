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

    //[Tooltip("Which effort is this card going to be spawn at")]
    //public BlockEffort cardSpawnAtEffort;

    [Tooltip("Certain score value that will be added to the player score if being collected")]
    public int cardScoreValue;

    [Space]

    [Header("Spawn Config : ")]
    public PlayerStatusData spawnRequirement;

    public UnityEvent OnCardCollected;

}

[System.Serializable]
public class PlayerStatusData
{
    [SerializeField] private int playerScore;

    [SerializeField] private Age playerAge;

    [SerializeField] private EducationStage educationStage;

    [SerializeField] private LoverStage loverStage;

    [SerializeField] private JobData jobData;

    public int PlayerScore { get => playerScore; set => playerScore = value; }
    public Age PlayerAge { get => playerAge; set => playerAge = value; }
    public EducationStage EducationStage { get => educationStage; set => educationStage = value; }
    public LoverStage LoverStage { get => loverStage; set => loverStage = value; }
    public JobData JobData { get => jobData; set => jobData = value; }

}
