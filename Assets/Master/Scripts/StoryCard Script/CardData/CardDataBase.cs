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

    //[Tooltip("Which effort is this card going to be spawn at")]
    //public BlockEffort cardSpawnAtEffort;

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
