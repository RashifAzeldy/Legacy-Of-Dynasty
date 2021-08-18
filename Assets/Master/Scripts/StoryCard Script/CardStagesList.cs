using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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