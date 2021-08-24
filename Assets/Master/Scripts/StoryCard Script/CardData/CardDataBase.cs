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

    public PlayerStatusRequirement spawnRequirement;

    public BlockEffort cardSpawnAtEffort;

    [Tooltip("The card produce certain score value if being collected by player")]
    public int cardScoreValue;

    [Tooltip("The condition where the card spawn based on player score")]
    public int cardSpawnOverScore;

    public virtual bool CheckSpawnRequirement() { return false; }

    public UnityAction OnCardCollected;

}

[System.Serializable]
public class PlayerStatusRequirement
{
    [Header("Requirement Config : ")]
    public int scoreMinimum;
    public Age playerAgeMinimum;
    public EducationStage educationStageMinimum;
    public LoverStage loverStageMinimum;
    public bool isEmployed = true;


    
}
