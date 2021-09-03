using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{

    #region Serialized Property Field

    [Header("Spawning Config : ")]
    [SerializeField] int     spawnLimit         = 15;
    [SerializeField] Vector2 spawnIntervalRange = new Vector2(0, 10);
    
    [Space]
    
    [SerializeField] List<CardDataList>   cardList       = new List<CardDataList>();
    [SerializeField] List<EffortPosition> spawnPositions = new List<EffortPosition>();
    
    [Space]

    [Header("Block Prefabs : ")]
    [SerializeField] BlockController blockNeutral;
    [SerializeField] BlockController blockPositive;
    [SerializeField] BlockController blockNegative;
    [SerializeField] BlockController blockMystery;

    #endregion

    [SerializeField] List<GameObject> spawnedBlock = new List<GameObject>();

    public List<GameObject> GetSpawnedBlock { get { return spawnedBlock; } set { spawnedBlock = value; } }

    PlayerState player;
    PlayerStatus status;
    bool _startCheck = true;


    private void Start()
    {
        player = FindObjectOfType<PlayerState>();
        status = FindObjectOfType<PlayerStatus>();
    }

    private void Update()
    {
        if (spawnedBlock.Count < spawnLimit && _startCheck)
        {
            SpawnBlock(Random.Range(spawnIntervalRange.x, spawnIntervalRange.y), gameObject.transform, 1);
        }
    }

    int SpawnPosIndex(BlockEffort effort)
    {
        int index = 0;
        foreach (EffortPosition item in spawnPositions)
        {
            if (item.GetEffort == effort)
            {
                index = spawnPositions.IndexOf(item);
                return index;
            }
        }
        return index;
    }

    Vector3 getSpawnPosition( BlockEffort spawnAtEffort )
    {
        int index = 0;
        foreach ( EffortPosition item in spawnPositions )
        {
            if ( item.GetEffort == spawnAtEffort )
            {
                index = spawnPositions.IndexOf(item);

                return spawnPositions[SpawnPosIndex(spawnAtEffort)].GetSpawnPos[Random.Range(0, 3)].position;
            }
        }

        return new Vector3(0, 0, 0);
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
            GameObject _cacheBlock;
            BlockController _cacheBController;
            CardDataBase _cacheCard = GetStoryCard(CheckRequirement(status, effort, cardList));

            yield return new WaitForSeconds(time);

            switch ( _cacheCard.cardValue )
            {
                case CardValue.Positive:
                _cacheBlock = Instantiate(blockPositive.gameObject, getSpawnPosition(effort), Quaternion.identity, parent);
                break;
                case CardValue.Negative:
                _cacheBlock = Instantiate(blockNegative.gameObject, getSpawnPosition(effort), Quaternion.identity, parent);
                break;
                case CardValue.Mystery:
                _cacheBlock = Instantiate(blockMystery.gameObject, getSpawnPosition(effort), Quaternion.identity, parent);
                break;
                case CardValue.Neutral:
                _cacheBlock = Instantiate(blockNeutral.gameObject, getSpawnPosition(effort), Quaternion.identity, parent);
                break;
                default:
                _cacheBlock = Instantiate(blockNeutral.gameObject, getSpawnPosition(effort), Quaternion.identity, parent);
                break;
            }

            _cacheBController = _cacheBlock.GetComponent<BlockController>();
            _cacheBController.cardData = _cacheCard;

            yield return new WaitForSeconds(0.15f);            
            
            spawnedBlock.Add(_cacheBlock);
            //if (i == (objQuantity - 1))
            //{
            //    startCheck = true;
            //}
        }
    }

    IEnumerator CheckingCountdown(float time)
    {
        yield return new WaitForSeconds(time);
        _startCheck = true;
    }

    List<CardDataBase> CheckRequirement(PlayerStatus status, BlockEffort effort, List<CardDataList> baseCardList)
    {
        List<CardDataBase> cacheSpawnableCards = new List<CardDataBase>();
        
        switch ( effort )
        {
            case BlockEffort.High:
                
                foreach ( CardDataBase item in baseCardList[0].highEffortCards )
                
                    if(LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement))
                        cacheSpawnableCards.Add(item);
                
                break;

            case BlockEffort.Medium:
                
                foreach ( CardDataBase item in baseCardList[0].normalEffortCards )
                    if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement))
                        cacheSpawnableCards.Add(item);
                
                break;
            
            case BlockEffort.Low:
            
                foreach ( CardDataBase item in baseCardList[0].lowEffortCards )
                    if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement))
                        cacheSpawnableCards.Add(item);
                
            break;
        }
        Debug.Log("Cache Count : " + cacheSpawnableCards.Count);
        return cacheSpawnableCards;
    }

    // Child Index State = 0
    // Teen Index State = 1
    // Adult Index State = 2
    // Elder Index State = 3
    CardDataBase GetStoryCard( List<CardDataBase> cardList )
    {
        return cardList[Random.Range(0, cardList.Count)];
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
