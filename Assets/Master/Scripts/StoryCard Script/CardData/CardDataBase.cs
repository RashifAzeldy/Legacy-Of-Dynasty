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

    public UnityEvent OnCardCollected;

    [Space]

    [Tooltip("Effect if player collected this card")]
    public CardEffect effect;


    public bool TestBool;
    [HideInInspector] public float PropFloat;


    [Header("Card Effect : ")]
    private int changeLevel;
    private float jumpBoost;
    public Stats changedStats;

    public PlayerStatusData spawnRequirement;

}

public enum CardEffect
{
    None,
    ChangeStats,
    ChangeJumpPower,
    GameOver
}

public enum Stats
{
    Education,
    Job,
    Lover
}
