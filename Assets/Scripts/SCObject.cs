using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCObject : MonoBehaviour
{
    [SerializeField] StoryCard storyCard;
    public StoryCard GetStoryCard { get { return storyCard; } set { storyCard = value; } }

    [SerializeField] bool collected;
    public bool IsCollected { get { return collected; } set { collected = value; } }

    [SerializeField] Vector3 pos = new Vector3();
    public Vector3 GetBlockPos { get { return pos; } set { pos = value; } }

    [SerializeField] GameObject cardObject;
    public GameObject GetCardObject { get { return cardObject; } }

    private void Start()
    {
        cardObject = gameObject;
    }
}
