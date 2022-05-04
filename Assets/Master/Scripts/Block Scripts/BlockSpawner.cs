using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if ( !Instance )
            Instance = this;

    }

    [SerializeField] PlayerState player;
    [SerializeField] PlayerStatus status;
    bool _startCheck = true;

    [SerializeField] List<CardDataBase> activeCards = new List<CardDataBase>();
    public List<CardDataBase> GetActiveCards { get { return activeCards; } set { activeCards = value; } }

    private void Update()
    {
        if ( _startCheck && Time.timeScale == 1 )
        {
            _startCheck = false;
            SpawnBlock(Random.Range(spawnIntervalRange.x, spawnIntervalRange.y));
        }
    }

    int SpawnPosIndex( BlockEffort effort )
    {

        foreach ( EffortPosition item in spawnPositions )
        {
            if ( item.GetEffort == effort )
                return spawnPositions.IndexOf(item);

        }
        return 0;
    }

    Vector3 GetSpawnPosition( BlockEffort spawnAtEffort )
    {

        foreach ( EffortPosition item in spawnPositions )
        {
            if ( item.GetEffort == spawnAtEffort )
            {
                int posIndex = SpawnPosIndex(spawnAtEffort);

                //Change This Code
                return spawnPositions[posIndex].GetSpawnPos[Random.Range(0, spawnPositions[posIndex].GetSpawnPos.Count)].position;
            }
        }

        return Vector3.zero;
    }

    public void SpawnBlock( float delay )
    {
        StartCoroutine(SpawnDelay(delay, BlockEffort.High));
        StartCoroutine(SpawnDelay(delay, BlockEffort.Medium));
        StartCoroutine(SpawnDelay(delay, BlockEffort.Low));
        StartCoroutine(CheckingCountdown(delay));
    }

    IEnumerator SpawnDelay( float time, BlockEffort effort )
    {
        CardDataBase cCard = GetStoryCard(CheckRequirement(status, effort, cardList));
        yield return new WaitForSeconds(time);

        if ( cCard )
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
            cBlockController.addCard = true;
        }
    }

    public IEnumerator CheckingCountdown( float time )
    {
        yield return new WaitForSeconds(time);
        _startCheck = true;
    }

    // Add When Player Touch With Blocks
    public void RemoveActiveCard( CardDataBase card )
    {
        CardDataBase removedCard = activeCards.FirstOrDefault(( a ) => a.cardName == card.cardName);
        activeCards.Remove(removedCard);
    }

    List<CardDataBase> CheckRequirement( PlayerStatus status, BlockEffort effort, List<CardDataList> baseCardList )
    {
        List<CardDataBase> cacheSpawnableCards = new List<CardDataBase>();
        // Here.
        switch ( effort )
        {
            case BlockEffort.High:
            if ( player.GetPlayerCurrentState == Age.Child )
            {
                foreach ( CardDataBase item in baseCardList[0].highEffortCards )
                {
                    if ( item.scoreSpawn )
                    {
                        if ( LODFunctionLibrary.SpawnableScoreLimitCount(status.playerStatusData, item) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                    else
                    {
                        if ( LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                }
            }
            else if ( player.GetPlayerCurrentState == Age.Teen )
            {
                foreach ( CardDataBase item in baseCardList[1].highEffortCards )
                {
                    if ( item.scoreSpawn )
                    {
                        if ( LODFunctionLibrary.SpawnableScoreLimitCount(status.playerStatusData, item) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                    else
                    {
                        if ( LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                }
            }
            else if ( player.GetPlayerCurrentState == Age.Adult )
            {
                foreach ( CardDataBase item in baseCardList[2].highEffortCards )
                {
                    if ( item.scoreSpawn )
                    {
                        if ( LODFunctionLibrary.SpawnableScoreLimitCount(status.playerStatusData, item) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                    else
                    {
                        if ( LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                }
            }
            else if ( player.GetPlayerCurrentState == Age.Elder )
            {
                foreach ( CardDataBase item in baseCardList[3].highEffortCards )
                {
                    if ( item.scoreSpawn )
                    {
                        if ( LODFunctionLibrary.SpawnableScoreLimitCount(status.playerStatusData, item) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                    else
                    {
                        if ( LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                }
            }

            break;

            case BlockEffort.Medium:
            if ( player.GetPlayerCurrentState == Age.Child )
            {
                foreach ( CardDataBase item in baseCardList[0].normalEffortCards )
                {
                    if ( item.scoreSpawn )
                    {
                        if ( LODFunctionLibrary.SpawnableScoreLimitCount(status.playerStatusData, item) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                    else
                    {
                        if ( LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                }
            }
            else if ( player.GetPlayerCurrentState == Age.Teen )
            {
                foreach ( CardDataBase item in baseCardList[1].normalEffortCards )
                {
                    if ( item.scoreSpawn )
                    {
                        if ( LODFunctionLibrary.SpawnableScoreLimitCount(status.playerStatusData, item) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                    else
                    {
                        if ( LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                }
            }
            else if ( player.GetPlayerCurrentState == Age.Adult )
            {
                foreach ( CardDataBase item in baseCardList[2].normalEffortCards )
                {
                    if ( item.scoreSpawn )
                    {
                        if ( LODFunctionLibrary.SpawnableScoreLimitCount(status.playerStatusData, item) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                    else
                    {
                        if ( LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                }
            }
            else if ( player.GetPlayerCurrentState == Age.Elder )
            {
                foreach ( CardDataBase item in baseCardList[3].normalEffortCards )
                {
                    if ( item.scoreSpawn )
                    {
                        if ( LODFunctionLibrary.SpawnableScoreLimitCount(status.playerStatusData, item) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                    else
                    {
                        if ( LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                }
            }

            break;

            case BlockEffort.Low:
            if ( player.GetPlayerCurrentState == Age.Child )
            {
                foreach ( CardDataBase item in baseCardList[0].lowEffortCards )
                {
                    if ( item.scoreSpawn )
                    {
                        if ( LODFunctionLibrary.SpawnableScoreLimitCount(status.playerStatusData, item) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                    else
                    {
                        if ( LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                }
            }
            else if ( player.GetPlayerCurrentState == Age.Teen )
            {
                foreach ( CardDataBase item in baseCardList[1].lowEffortCards )
                {
                    if ( item.scoreSpawn )
                    {
                        if ( LODFunctionLibrary.SpawnableScoreLimitCount(status.playerStatusData, item) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                    else
                    {
                        if ( LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                }
            }
            else if ( player.GetPlayerCurrentState == Age.Adult )
            {
                foreach ( CardDataBase item in baseCardList[2].lowEffortCards )
                {
                    if ( item.scoreSpawn )
                    {
                        if ( LODFunctionLibrary.SpawnableScoreLimitCount(status.playerStatusData, item) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                    else
                    {
                        if ( LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                }
            }
            else if ( player.GetPlayerCurrentState == Age.Elder )
            {
                foreach ( CardDataBase item in baseCardList[3].lowEffortCards )
                {
                    if ( item.scoreSpawn )
                    {
                        if ( LODFunctionLibrary.SpawnableScoreLimitCount(status.playerStatusData, item) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                    else
                    {
                        if ( LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement, item.scoreCondition) )
                        {
                            cacheSpawnableCards.Add(item);
                        }
                    }
                }
            }

            break;
        }
        return cacheSpawnableCards;
    }

    // Child Index State = 0
    // Teen Index State = 1
    // Adult Index State = 2
    // Elder Index State = 3
    CardDataBase GetStoryCard( List<CardDataBase> cardList )
    {
        if ( cardList.Count != 0 )
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
