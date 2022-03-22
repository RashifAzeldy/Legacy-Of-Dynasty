using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockSpawner : MonoBehaviour
{

    #region Serialized Property Field

    [Header("Spawning Config : ")]
    [SerializeField] int spawnLimit = 15;
    [SerializeField] Vector2 spawnIntervalRange = new Vector2(5, 15);

    [Space]

    [SerializeField] List<CardDataList> cardList = new List<CardDataList>();
    [SerializeField] List<EffortPosition> spawnPositions = new List<EffortPosition>();

    [Space]

    [Header("Block Config:")]
    public BlockController BlockNeutralPrefab;
    public BlockController BlockPositivePrefab;
    public BlockController BlockNegativePrefab;
    public BlockController BlockMysteryPrefab;

    [Space]

    [SerializeField] float blockSpeed = 5.5f;

    #endregion

    public static BlockSpawner Instance { get; private set; }

    public float BlockSpeed
    {
        get => blockSpeed;
    }

    private void OnEnable()
    {
        if (!Instance)
            Instance = this;

    }

    [SerializeField] PlayerState player;
    [SerializeField] PlayerStatus status;
    bool _startCheck = true;

    private void Update()
    {
        if (_startCheck && Time.timeScale == 1)
        {
            SpawnBlock(Random.Range(spawnIntervalRange.x, spawnIntervalRange.y), gameObject.transform, 1);
        }
    }

    int SpawnPosIndex(BlockEffort effort)
    {

        foreach (EffortPosition item in spawnPositions)
        {
            if (item.GetEffort == effort)
                return spawnPositions.IndexOf(item);

        }
        return 0;
    }

    Vector3 GetSpawnPosition(BlockEffort spawnAtEffort)
    {

        foreach (EffortPosition item in spawnPositions)
        {
            if (item.GetEffort == spawnAtEffort)
            {
                int posIndex = SpawnPosIndex(spawnAtEffort);

                //Change This Code
                return spawnPositions[posIndex].GetSpawnPos[Random.Range(0, spawnPositions[posIndex].GetSpawnPos.Count)].position;
            }
        }

        return Vector3.zero;
    }

    public void SpawnBlock(float delay, Transform parent, int quantity)
    {

        _startCheck = false;
        StartCoroutine(SpawnDelay(delay, parent, quantity, BlockEffort.High));
        StartCoroutine(SpawnDelay(delay, parent, quantity, BlockEffort.Medium));
        StartCoroutine(SpawnDelay(delay, parent, quantity, BlockEffort.Low));
        StartCoroutine(CheckingCountdown(delay * quantity));
    }

    IEnumerator SpawnDelay(float time, Transform parent, int objQuantity, BlockEffort effort)
    {
        for (int i = 0; i < objQuantity; i++)
        {
            CardDataBase cCard = GetStoryCard(CheckRequirement(status, effort, cardList));

            yield return new WaitForSeconds(time);

            if (cCard)
            {
                BlockController cBlockController = null;
                GameObject cBlockReference =
                    cCard.cardValue == CardValue.Positive ? BlockPositivePrefab.gameObject
                    : cCard.cardValue == CardValue.Negative ? BlockNegativePrefab.gameObject
                    : cCard.cardValue == CardValue.Mystery ? BlockMysteryPrefab.gameObject
                    : cCard.cardValue == CardValue.Neutral ? BlockNeutralPrefab.gameObject
                    : null;

                gameObject.InstantiatePool(cBlockReference, GetSpawnPosition(effort), Quaternion.identity);

                cBlockController = cBlockReference.GetComponent<BlockController>();

                cBlockController.cardData = cCard;

                cBlockController.InitBlock();
            }
        }
    }

    public IEnumerator CheckingCountdown(float time)
    {
        yield return new WaitForSeconds(time);
        _startCheck = true;
    }

    List<CardDataBase> CheckRequirement(PlayerStatus status, BlockEffort effort, List<CardDataList> baseCardList)
    {
        List<CardDataBase> cacheSpawnableCards = new List<CardDataBase>();
        // Here.
        switch (effort)
        {
            case BlockEffort.High:
                if (player.GetPlayerCurrentState == Age.Child)
                {
                    foreach (CardDataBase item in baseCardList[0].highEffortCards)

                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition))
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Teen)
                {
                    foreach (CardDataBase item in baseCardList[1].highEffortCards)

                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition))
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Adult)
                {
                    foreach (CardDataBase item in baseCardList[2].highEffortCards)

                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition))
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Elder)
                {
                    foreach (CardDataBase item in baseCardList[3].highEffortCards)

                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition))
                            cacheSpawnableCards.Add(item);
                }

                break;

            case BlockEffort.Medium:
                if (player.GetPlayerCurrentState == Age.Child)
                {
                    foreach (CardDataBase item in baseCardList[0].normalEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition))
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Teen)
                {
                    foreach (CardDataBase item in baseCardList[1].normalEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition))
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Adult)
                {
                    foreach (CardDataBase item in baseCardList[2].normalEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition))
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Elder)
                {
                    foreach (CardDataBase item in baseCardList[3].normalEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition))
                            cacheSpawnableCards.Add(item);
                }

                break;

            case BlockEffort.Low:
                if (player.GetPlayerCurrentState == Age.Child)
                {
                    foreach (CardDataBase item in baseCardList[0].lowEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition))
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Teen)
                {
                    foreach (CardDataBase item in baseCardList[1].lowEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition))
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Adult)
                {
                    foreach (CardDataBase item in baseCardList[2].lowEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition))
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Elder)
                {
                    foreach (CardDataBase item in baseCardList[3].lowEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition))
                            cacheSpawnableCards.Add(item);
                }

                break;
        }
        return cacheSpawnableCards;
    }

    // Child Index State = 0
    // Teen Index State = 1
    // Adult Index State = 2
    // Elder Index State = 3
    CardDataBase GetStoryCard(List<CardDataBase> cardList)
    {
        if (cardList.Count != 0)
            return cardList[Random.Range(0, cardList.Count)];

        return null;
    }

}

public enum BlockEffort
{
    High,
    Medium,
    Low
}

[System.Serializable]
public class EffortPosition
{
    [SerializeField] BlockEffort effort;

    [SerializeField] List<Transform> spawnPos = new List<Transform>(3);

    public BlockEffort GetEffort { get { return effort; } }
    public List<Transform> GetSpawnPos { get { return spawnPos; } }
}
