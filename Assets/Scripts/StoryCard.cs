using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story Card", menuName = "Create Story Card", order = 1)]
public class StoryCard : ScriptableObject
{
    [SerializeField] CardType type;
    public CardType GetCardType { get { return type; } }
    [SerializeField] string action = "Write Action Here...";
    public string GetAction { get { return action; } }
    [SerializeField] int score;
    public int GetScore { get { return score; } }
    [Range(-99999, 99999)]
    [SerializeField] int scoreRequirement;
    public int GetScoreRequirement { get { return scoreRequirement; } }
    [SerializeField] List<StoryCard> cardToUnlock = new List<StoryCard>();
    public List<StoryCard> GetUnlockedCard { get { return cardToUnlock; } set { cardToUnlock = value; } }
}

public enum CardType
{
    High,
    Medium,
    Low,
    Attemp
}