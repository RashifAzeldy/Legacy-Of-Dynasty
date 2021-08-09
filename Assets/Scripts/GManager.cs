using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GManager : MonoBehaviour
{
    [SerializeField] GameObject positiveBlockPrefab;
    [SerializeField] GameObject neutralBlockPrefab;
    [SerializeField] GameObject negativeBlockPrefab;
    [SerializeField] GameObject attempBlockPrefab;

    [SerializeField] Transform positiveBlocksParent;
    [SerializeField] Transform neutralBlocksParent;
    [SerializeField] Transform negativeBlocksParent;

    [SerializeField] List<SCObject> blockList = new List<SCObject>();
    public List<SCObject> GetBlockList { get { return blockList; } set { blockList = value; } }
    [SerializeField] List<StoryCard> unlockedPositiveSC = new List<StoryCard>();
    [SerializeField] List<StoryCard> unlockedNeutralSC = new List<StoryCard>();
    [SerializeField] List<StoryCard> unlockedNegativeSC = new List<StoryCard>();
    [SerializeField] List<StoryCard> unlockedAttempSC = new List<StoryCard>();

    [SerializeField] float blockSpeed = 1f;

    [SerializeField] TextMeshProUGUI scoreText;

    PlayerController playerController;
    SCObject collectedStoryCard;
    public Vector3 RandomizedPos(float minX, float maxX)
    {
        Vector3 result;
        float xResult;
        xResult = Random.Range(minX, maxX);
        result = new Vector3(xResult, 0, 0);

        return result;
    }

    public Vector3 LastBlockPos(List<SCObject> list)
    {
        Vector3 result = Vector3.zero;
        float farX = 0;
        foreach (SCObject item in list)
        {
            if (list.IndexOf(item) != list.Count - 1)
            {
                if (item.GetBlockPos.x > list[list.IndexOf(item) + 1].GetComponent<SCObject>().GetBlockPos.x)
                {
                    farX = item.GetBlockPos.x;
                }
                else
                {
                    farX = list[list.IndexOf(item) + 1].GetBlockPos.x;
                }
            }
        }
        result = new Vector3(farX, 0, 0);

        return result;
    }

    Vector3 positiveStartPos;
    Vector3 positivePos;
    Vector3 neutralStartPos;
    Vector3 neutralPos;
    Vector3 negativeStarPos;
    Vector3 negativePos;
    Vector3 spawnPos;

    bool pause;
    public bool isPaused { get { return pause; } set { pause = value; } }

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        for (int i = 0; i < 5; i++)
        {
            // Add Good Desscission when The Game Started
            GameObject high;
            if (positiveStartPos == Vector3.zero)
            {
                high = Instantiate(positiveBlockPrefab, RandomizedPos(7.25f, 15f), Quaternion.identity,
                positiveBlocksParent);
                RandomChance(high, CardType.High);
                positiveStartPos = high.transform.position;
                positivePos = high.transform.position;
                AddBasicComponent(high, 0);
            }
            else
            {
                high = Instantiate(positiveBlockPrefab, RandomizedPos(7.25f, 15f) + new Vector3(positivePos.x, 0, 0),
                Quaternion.identity, positiveBlocksParent);
                RandomChance(high, CardType.High);
                AddBasicComponent(high, 0);
                positivePos = high.transform.position;
            }

            high.GetComponent<SCObject>().GetBlockPos = positivePos;
            blockList.Add(high.GetComponent<SCObject>());

            // Add Neutral Desscission when The Game Started
            GameObject medium;

            if (neutralStartPos == Vector3.zero)
            {
                medium = Instantiate(neutralBlockPrefab, RandomizedPos(7.25f, 15f),
                Quaternion.identity, neutralBlocksParent);
                RandomChance(medium, CardType.Medium);
                neutralStartPos = medium.transform.position;
                neutralPos = medium.transform.position;
                AddBasicComponent(medium, 1);
            }
            else
            {
                medium = Instantiate(neutralBlockPrefab, RandomizedPos(7.25f, 15f) + new Vector3(neutralPos.x, 0, 0),
                Quaternion.identity, neutralBlocksParent);
                RandomChance(medium, CardType.Medium);
                AddBasicComponent(medium, 1);
                neutralPos = medium.transform.position;
            }
            medium.GetComponent<SCObject>().GetBlockPos = neutralPos;
            blockList.Add(medium.GetComponent<SCObject>());

            // Add Bad Desscission when The Game Started
            GameObject low;

            if (negativeStarPos == Vector3.zero)
            {
                low = Instantiate(negativeBlockPrefab, RandomizedPos(7.25f, 15f), Quaternion.identity, negativeBlocksParent);
                RandomChance(low, CardType.Low);
                negativeStarPos = low.transform.position;
                negativePos = low.transform.position;
                AddBasicComponent(low, 2);
            }
            else
            {
                low = Instantiate(negativeBlockPrefab, RandomizedPos(7.25f, 15f) + new Vector3(negativePos.x, 0, 0),
                Quaternion.identity, negativeBlocksParent);
                RandomChance(low, CardType.Low);
                AddBasicComponent(low, 2);
                negativePos = low.transform.position;
            }
            low.GetComponent<SCObject>().GetBlockPos = negativePos;
            blockList.Add(low.GetComponent<SCObject>());
        }

        spawnPos = LastBlockPos(blockList);
    }
    void Update()
    {
        if (playerController.GetCollectedStoryObject != null)
        {
            collectedStoryCard = playerController.GetCollectedStoryObject;
        }

        foreach (SCObject item in blockList)
        {
            if (item.gameObject.transform.position.x <= -3.5f)
            {
                TeleportBlock(item.GetCardObject, new Vector3(spawnPos.x, Random.Range(4.5f, -4f), 0));
                item.GetBlockPos = item.GetCardObject.transform.position;
            }
        }

        if (blockList.Count > 0 && !pause)
        {
            foreach (SCObject item in blockList)
            {
                item.GetCardObject.transform.Translate(-blockSpeed, 0, 0);
            }
        }
    }
    public void UpdateScoreText(GameObject descObj)
    {
        if (descObj.GetComponent<SCObject>().GetStoryCard != null && descObj.GetComponent<SCObject>().IsCollected)
        {
            scoreText.text = "" + playerController.GetPlayerScore;
            descObj.GetComponent<SCObject>().IsCollected = false;
        }
    }

    void RandomChance(GameObject block, CardType type)
    {
        if (type == CardType.High)
        {
            int goodChance = Random.Range(0, 100);
            if (goodChance >= 0 && goodChance <= 80)
            {
                block.transform.position = new Vector3(block.transform.position.x, Random.Range(1f, 5.7f), 0);
            }
            else if (goodChance > 80 && goodChance < 95)
            {
                block.transform.position = new Vector3(block.transform.position.x, Random.Range(-4.1f, 0.9f), 0);
            }
            else
            {
                block.transform.position = new Vector3(block.transform.position.x, Random.Range(-8.6f, -3.9f), 0);
            }
        }
        else if (type == CardType.Medium)
        {
            int neutralChance = Random.Range(0, 100);
            if (neutralChance >= 0 && neutralChance <= 80)
            {
                block.transform.position = new Vector3(block.transform.position.x, Random.Range(-4.1f, 0.9f), 0);
            }
            else if (neutralChance > 80 && neutralChance < 90)
            {
                block.transform.position = new Vector3(block.transform.position.x, Random.Range(1f, 5.7f), 0);
            }
            else
            {
                block.transform.position = new Vector3(block.transform.position.x, Random.Range(-8.6f, -3.9f), 0);
            }
        }
        else if (type == CardType.Low)
        {
            int badChance = Random.Range(0, 100);
            if (badChance >= 0 && badChance <= 90)
            {
                block.transform.position = new Vector3(block.transform.position.x, Random.Range(-8.6f, -3.9f), 0);
            }
            else if (badChance > 90 && badChance < 97)
            {
                block.transform.position = new Vector3(block.transform.position.x, Random.Range(-4.1f, 0.9f), 0);
            }
            else
            {
                block.transform.position = new Vector3(block.transform.position.x, Random.Range(1f, 5.7f), 0);
            }
        }
    }

    // 0 = Good
    // 1 = Neutral
    // 2 = Bad
    // 3 = Attemp
    public void AddBasicComponent(GameObject go, int descIndex)
    {
        if (go.GetComponent<SCObject>() == null)
        {
            go.AddComponent<SCObject>();
        }
        switch (descIndex)
        {
            case 0:
                go.GetComponent<SCObject>().GetStoryCard = unlockedPositiveSC[Random.Range(0,
                unlockedPositiveSC.Count)];
                break;

            case 1:
                go.GetComponent<SCObject>().GetStoryCard = unlockedNeutralSC[Random.Range(0,
                unlockedNeutralSC.Count)];
                break;

            case 2:
                go.GetComponent<SCObject>().GetStoryCard = unlockedNegativeSC[Random.Range(0,
                unlockedNegativeSC.Count)];
                break;

            case 3:
                go.GetComponent<SCObject>().GetStoryCard = unlockedAttempSC[Random.Range(0,
                unlockedAttempSC.Count)];
                break;
        }
        go.GetComponent<Rigidbody2D>().freezeRotation = true;
        go.GetComponent<Rigidbody2D>().isKinematic = true;
        go.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void DisableCollected(GameObject collectedObj, StoryCard descission, int index)
    {
        TeleportBlock(blockList[index].GetCardObject, new Vector3(spawnPos.x, Random.Range(4.5f, -4f)));
        ChangeDescission(collectedObj.GetComponent<SCObject>().GetStoryCard.GetCardType,
        collectedObj.GetComponent<SCObject>().GetStoryCard, collectedObj);
        blockList[index].GetComponent<SCObject>().GetBlockPos = collectedObj.transform.position;
        //CheckNearestObject(blockList, collectedObj);
        playerController.PlayerUpdateScore = true;
    }

    public void TeleportBlock(GameObject block, Vector3 pos)
    {
        block.transform.position = pos;
        block.SetActive(true);

        RandomChance(block, block.GetComponent<SCObject>().GetStoryCard.GetCardType);
        //CheckNearestObject(blockList, block);
    }

    void ChangeDescission(CardType type, StoryCard storyCard, GameObject block)
    {
        if (storyCard.GetUnlockedCard.Count > 0)
        {
            if (type == CardType.High && storyCard.GetUnlockedCard.Count >= 1)
            {
                foreach (StoryCard item in storyCard.GetUnlockedCard)
                {
                    if (item.GetCardType == CardType.High)
                    {
                        unlockedPositiveSC.Add(item);
                    }
                    else if (item.GetCardType == CardType.Medium)
                    {
                        unlockedNeutralSC.Add(item);
                    }
                    else if (item.GetCardType == CardType.Low)
                    {
                        unlockedNegativeSC.Add(item);
                    }
                }
                unlockedPositiveSC.RemoveAt(unlockedPositiveSC.IndexOf(storyCard));
                int goodCount = unlockedPositiveSC.Count;
                foreach (SCObject item in blockList)
                {
                    if (item.GetStoryCard.GetCardType == CardType.High &&
                        unlockedPositiveSC.Count > 1)
                    {
                        item.GetStoryCard = unlockedPositiveSC[Random.Range(0,
                        goodCount)];
                    }
                    else if (item.GetComponent<SCObject>().GetStoryCard.GetCardType == CardType.High &&
                        unlockedPositiveSC.Count == 1)
                    {
                        item.GetComponent<SCObject>().GetStoryCard = unlockedPositiveSC[0];
                    }
                }
            }
            else if (type == CardType.Medium && storyCard.GetUnlockedCard.Count >= 1)
            {
                foreach (StoryCard item in storyCard.GetUnlockedCard)
                {
                    if (item.GetCardType == CardType.High)
                    {
                        unlockedPositiveSC.Add(item);
                    }
                    else if (item.GetCardType == CardType.Medium)
                    {
                        unlockedNeutralSC.Add(item);
                    }
                    else if (item.GetCardType == CardType.Low)
                    {
                        unlockedNegativeSC.Add(item);
                    }
                }
                unlockedNeutralSC.RemoveAt(unlockedNeutralSC.IndexOf(storyCard));
                int neutralCount = unlockedNeutralSC.Count;
                foreach (SCObject item in blockList)
                {
                    if (item.GetStoryCard.GetCardType == CardType.Medium &&
                        unlockedNeutralSC.Count > 1)
                    {
                        item.GetStoryCard = unlockedNeutralSC[Random.Range(0,
                        neutralCount)];
                    }
                    else if (item.GetStoryCard.GetCardType == CardType.Medium &&
                        unlockedNeutralSC.Count == 1)
                    {
                        item.GetStoryCard = unlockedNeutralSC[0];
                    }
                }
            }
            else if (type == CardType.Low && storyCard.GetUnlockedCard.Count >= 1)
            {
                foreach (StoryCard item in storyCard.GetUnlockedCard)
                {
                    if (item.GetCardType == CardType.High)
                    {
                        unlockedPositiveSC.Add(item);
                    }
                    else if (item.GetCardType == CardType.Medium)
                    {
                        unlockedNeutralSC.Add(item);
                    }
                    else if (item.GetCardType == CardType.Low)
                    {
                        unlockedNegativeSC.Add(item);
                    }
                }
                unlockedNegativeSC.RemoveAt(unlockedNegativeSC.IndexOf(storyCard));
                int badCount = unlockedNegativeSC.Count;
                foreach (SCObject item in blockList)
                {
                    if (item.GetStoryCard.GetCardType == CardType.Low &&
                        unlockedNegativeSC.Count > 1)
                    {
                        item.GetStoryCard = unlockedNegativeSC[Random.Range(0,
                        badCount)];
                    }
                    else if (item.GetStoryCard.GetCardType == CardType.Low &&
                        unlockedNegativeSC.Count == 1)
                    {
                        item.GetStoryCard = unlockedNegativeSC[0];
                    }
                }
            }
        }

    }

    void CheckNearestObject(List<SCObject> blockList, GameObject block)
    {
        Vector3 resultPos = Vector3.zero;
        float minDist = 1.5f;
        Vector3 blockPos = block.transform.position;
        foreach (SCObject item in blockList)
        {
            float distance = Vector3.Distance(item.GetCardObject.transform.position, block.transform.position);
            if (distance < minDist)
            {
                item.GetCardObject.transform.position += new Vector3(minDist - distance, 0, 0);
            }
        }
    }
}
/**/