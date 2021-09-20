using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCardCollector : MonoBehaviour
{
    [SerializeField] List<CardDataBase> collectedCard = new List<CardDataBase>();
    public List<CardDataBase> GetCollectedStoryCard { get => collectedCard; set => collectedCard = value; }
}
