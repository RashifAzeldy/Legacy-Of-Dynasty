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
    bool _startCheck = true;


    private void Start()
    {
        player = FindObjectOfType<PlayerState>();
    }

    private void Update()
    {
        if (spawnedBlock.Count < spawnLimit && _startCheck)
        {
            SpawnBlock(Random.Range(spawnIntervalRange.x, spawnIntervalRange.y), blockNeutral, gameObject.transform, 1);
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

    public void SpawnBlock(float delay, BlockController block, Transform parent, int quantity)
    {

        _startCheck = false;
            StartCoroutine(SpawnDelay(delay, block, parent, quantity, BlockEffort.High));
            StartCoroutine(SpawnDelay(delay, block, parent, quantity, BlockEffort.Medium));
            StartCoroutine(SpawnDelay(delay, block, parent, quantity, BlockEffort.Low));
        StartCoroutine(CheckingCountdown(delay * quantity));
    }

    IEnumerator SpawnDelay(float time, BlockController spawnObj, Transform parent, int objQuantity, BlockEffort effort)
    {
        for (int i = 0; i < objQuantity; i++)
        {
            GameObject _tempBlock;
            yield return new WaitForSeconds(time);

            _tempBlock = Instantiate(spawnObj.gameObject, getSpawnPosition(effort), Quaternion.identity, parent);
            
            AddBlockStoryCard(cardList, player.GetPlayerCurrentState, _tempBlock.GetComponent<BlockController>(), effort);

            yield return new WaitForSeconds(0.15f);            
            
            spawnedBlock.Add(_tempBlock);
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

    Vector3 getSpawnPosition(BlockEffort spawnAtEffort)
    {
        int index = 0;
        foreach (EffortPosition item in spawnPositions)
        {
            if (item.GetEffort == spawnAtEffort)
            {
                index = spawnPositions.IndexOf(item);

                return spawnPositions[SpawnPosIndex(spawnAtEffort)].GetSpawnPos[Random.Range(0, 3)].position;
            }
        }

        return new Vector3(0, 0, 0);
    }

    // Child Index State = 0
    // Teen Index State = 1
    // Adult Index State = 2
    // Elder Index State = 3
    void AddBlockStoryCard(List<CardDataList> cardList, Age playerState, BlockController block, BlockEffort effort)
    {
        switch (playerState)
        {
            //case Age.Child:
            //    if (effort == BlockEffort.High)
            //    {
            //        block.cardData = cardList[0].cardStagesHolder[0].cardDataList[Random.Range(0, cardList[0].cardStagesHolder[0].cardDataList.Length)];
            //    }
            //    else if (effort == BlockEffort.Medium)
            //    {
            //        block.cardData = cardList[0].cardStagesHolder[1].cardDataList[Random.Range(0, cardList[0].cardStagesHolder[1].cardDataList.Length)];
            //    }
            //    else
            //    {
            //        block.cardData = cardList[0].cardStagesHolder[2].cardDataList[Random.Range(0, cardList[0].cardStagesHolder[2].cardDataList.Length)];
            //    }
            //    break;
        }
    }
}

public enum BlockEffort
{
    High,
    Medium,
    Low
}

/*
    case Age.Teen:
        block.cardData = cardList.cardStagesHolder[1].cardDataList[Random.Range(0,
                        cardList.cardStagesHolder[1].cardDataList.Length)];
        break;
    case Age.Adult:
        block.cardData = cardList.cardStagesHolder[2].cardDataList[Random.Range(0,
                        cardList.cardStagesHolder[2].cardDataList.Length)];
        break;
    case Age.Elder:
        block.cardData = cardList.cardStagesHolder[3].cardDataList[Random.Range(0,
                        cardList.cardStagesHolder[3].cardDataList.Length)];
        break;
*/
[System.Serializable]
public class EffortPosition
{
    [SerializeField] BlockEffort effort;
   
    [SerializeField] List<Transform> spawnPos = new List<Transform>(3);

    public BlockEffort GetEffort { get { return effort; } }
    public List<Transform> GetSpawnPos { get { return spawnPos; } }
}
