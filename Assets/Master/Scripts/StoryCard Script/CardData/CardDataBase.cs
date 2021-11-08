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

    [Space]

    [Header("Spawn Config : ")]
    public PlayerStatusData spawnRequirement;

    public UnityEvent OnCardCollected;

    #region Effects
    public CardEffect effect;
    // Changed Stats
    public Stats changedStats;
    public int changeLevel;
    // Can't Jump
    public float time;
    // Dead
    public string causingDeath;
    #endregion
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
