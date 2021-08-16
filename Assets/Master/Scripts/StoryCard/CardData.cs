using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum CardValue { Positive, Negative, Mystery, Neutral }

[System.Serializable]
public struct CardInfo
{
    [Header("Card Data : ")]
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

}

[CreateAssetMenu(menuName = "Card/Card Data", fileName = "CardData")]
public class CardData : ScriptableObject
{

    public CardInfo cardData;

    public int getCardScoreValue() { return cardData.cardScoreValue; }


}

[System.Serializable]
public class CardHolder
{
    public CardData[] cardDataList;
}

[CreateAssetMenu(menuName = "Card/Card List", fileName = "Card List")]
public class CardStagesList : ScriptableObject
{
    public CardHolder[] cardStagesHolder;
}
