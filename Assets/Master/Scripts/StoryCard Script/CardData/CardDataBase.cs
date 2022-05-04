using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [Tooltip("Is This a Random Card ?")]
    public bool random;
    public List<CardDataBase> randomCards = new List<CardDataBase>();

    [Tooltip("Certain score value that will be added to the player score if being collected")]
    public int cardScoreValue;
    [Tooltip("Is This Card Make Player Dead if Stacked ?")]
    public bool stackable;
    public int stackCount;
    [Tooltip("Is This Card Spawn Limited by Scores ?")]
    public bool scoreSpawn;
    public List<int> scoresLimit = new List<int>();
    public int spawnableCount;

    [Space]

    [Header("Spawn Config : ")]
    public PlayerStatusData spawnRequirement;
    public ScoreCheck scoreCondition;

    public UnityEvent OnCardCollected;

    #region Effects
    public CardEffect effect;
    // Changed Stats
    public Stats changedStats;
    public int changeLevel;
    public float time;
    public string causingDeath;
    #endregion
}

public enum ScoreCheck
{
    MoreThan, // >
    LessThan, // <
    Equal // ==
}
public enum CardEffect
{
    None,
    ChangeStats,
    CantJump,
    Dead
}

public enum Stats
{
    Education,
    Job,
    Lover
}

public enum Skills
{
    Language,
    Science,
    Music,
    Athletic

}
