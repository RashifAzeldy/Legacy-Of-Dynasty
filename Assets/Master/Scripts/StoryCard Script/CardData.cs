using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum CardValue { Positive, Negative, Mystery, Neutral }

[System.Serializable]
public struct CardInfo
{
    [Header("Card Data : ")]
    public BlockEffort cardSpawnAtEffort;
    public CardValue cardValue;

    public string cardName;

    [Multiline(3)]
    public string cardStory;

    [Space]

    [Header("Card Config : ")]
    [Tooltip("The card produce certain score value if being collected by player")]
    public int cardScoreValue;

    [Tooltip("The condition where the card spawn based on player score")]
    public int cardSpawnOverScore;

    [Space]

    [Header("Spawn Requirement : ")]
    [SerializeField] Education educationStatus;
    public Education GetEducationRequirement { get { return educationStatus; } }
    [SerializeField] Career careerStatus;
    public Career GetCareerRequirement { get { return careerStatus; } }
    [SerializeField] LoverStage loveStatus;
    public LoverStage GetLoveRequirement { get { return loveStatus; } }
}

[CreateAssetMenu(menuName = "Card/Card Data", fileName = "CardData")]
public class CardData : ScriptableObject
{
    public CardInfo card;

    public int getCardScoreValue() { return card.cardScoreValue; }
}

