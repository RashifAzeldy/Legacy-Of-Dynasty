using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] BlockController block;
    [SerializeField] List<CardStagesList> cardList = new List<CardStagesList>();
    [SerializeField] List<EffortPosition> spawnPositions = new List<EffortPosition>();

    PlayerState player;
    PlayerStatus status;
    [SerializeField] List<GameObject> spawnedBlock = new List<GameObject>();
    public List<GameObject> GetSpawnedBlock { get { return spawnedBlock; } set { spawnedBlock = value; } }

    bool startCheck;

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
    private void Start()
    {
        player = FindObjectOfType<PlayerState>();
        status = GetComponent<PlayerStatus>();
        SpawnBlock(1, block, gameObject.transform, 5);
    }

    private void Update()
    {
        if (spawnedBlock.Count < 15 && startCheck)
        {
            SpawnBlock(1.5f, block, gameObject.transform, 1);
        }
    }

    public void SpawnBlock(float delay, BlockController block, Transform parent, int quantity)
    {
        startCheck = false;
        StartCoroutine(SpawnDelay(delay, block, parent, quantity, BlockEffort.High));
        StartCoroutine(SpawnDelay(delay, block, parent, quantity, BlockEffort.Medium));
        StartCoroutine(SpawnDelay(delay, block, parent, quantity, BlockEffort.Low));
    }

    IEnumerator SpawnDelay(float time, BlockController spawnObj, Transform parent, int objQuantity, BlockEffort effort)
    {
        for (int i = 0; i < objQuantity; i++)
        {
            GameObject block;
            yield return new WaitForSeconds(time);
            block = Instantiate(spawnObj.gameObject, new Vector3(16.5f, 0, 0), Quaternion.identity, parent);
            CheckCardEffort(cardList, player.GetPlayerCurrentState, block.GetComponent<BlockController>(), effort);
            LODFunctionLibrary.FreezeYRigidbody(block);

            block.transform.position = spawnPositions[SpawnPosIndex(effort)].GetSpawnPos[Random.Range(0, 3)].position;
            spawnedBlock.Add(block);
            if (i == (objQuantity - 1))
            {
                startCheck = true;
            }
        }
    }

    // Child Index State = 0
    // Teen Index State = 1
    // Adult Index State = 2
    // Elder Index State = 3
    void CheckCardEffort( List<CardStagesList> cardList, Age playerState, BlockController block, BlockEffort effort )
    {
        List<CardData> tempCardList = new List<CardData>();
        Debug.Log(tempCardList.Count);
        switch ( playerState )
        {
            case Age.Child:
                foreach ( CardData item in cardList[0].cardStagesHolder[0].cardDataList )
                {
                    if ( item.card.cardSpawnAtEffort == effort )
                    {
                        tempCardList.Add(item);
                    }
                }
            break;
        }
        Debug.Log(tempCardList.Count + ", Effort : " + effort);
        CheckSpawnRequirement(tempCardList, block);
    }

    void CheckSpawnRequirement ( List<CardData> cardList, BlockController block ) 
    {
        List<CardData> resultList = new List<CardData>();
        foreach ( CardData item in cardList )
        {
            if ( item.card.GetCareerRequirement == status.GetCareerStatus && item.card.GetEducationRequirement == status.GetEducationStatus
                && item.card.GetLoveRequirement == status.GetLoverStage)
            {
                resultList.Add(item);
            }
        }
        Debug.Log(resultList.Count);
        if(cardList.Count != 0)
            AddStoryCard(resultList, block);
    }

    void AddStoryCard( List<CardData> cardList, BlockController block ) 
    {
        if(cardList.Count > 1)
            block.cardData = cardList[Random.Range(0, cardList.Count)];
        else
        {
            block.cardData = cardList[0];
        }
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
    public BlockEffort GetEffort { get { return effort; } }
    [SerializeField] List<Transform> spawnPos = new List<Transform>(3);
    public List<Transform> GetSpawnPos { get { return spawnPos; } }
}
