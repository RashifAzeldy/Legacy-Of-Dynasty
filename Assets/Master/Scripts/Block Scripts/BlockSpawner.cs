using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] BlockController block;
    [SerializeField] List<CardStagesList> cardList = new List<CardStagesList>();
    [SerializeField] List<EffortPosition> spawnPositions = new List<EffortPosition>();

    PlayerState player;
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
        //StartCoroutine(CheckingCountdown(delay * quantity));
    }

    IEnumerator SpawnDelay(float time, BlockController spawnObj, Transform parent, int objQuantity, BlockEffort effort)
    {
        for (int i = 0; i < objQuantity; i++)
        {
            GameObject block;
            yield return new WaitForSeconds(time);
            block = Instantiate(spawnObj.gameObject, new Vector3(16.5f, 0, 0), Quaternion.identity, parent);
            AddBlockStoryCard(cardList, player.GetPlayerCurrentState, block.GetComponent<BlockController>(), effort);
            LODFunctionLibrary.FreezeYRigidbody(block);

            yield return new WaitForSeconds(0.15f);
            block.transform.position = spawnPositions[SpawnPosIndex(effort)].GetSpawnPos[Random.Range(0, 3)].position;
            spawnedBlock.Add(block);
            if (i == (objQuantity - 1))
            {
                startCheck = true;
            }
        }
    }

    IEnumerator CheckingCountdown(float time)
    {
        yield return new WaitForSeconds(time);
        startCheck = true;
    }

    // Child Index State = 0
    // Teen Index State = 1
    // Adult Index State = 2
    // Elder Index State = 3
    void AddBlockStoryCard(List<CardStagesList> cardList, Age playerState, BlockController block, BlockEffort effort)
    {
        switch (playerState)
        {
            case Age.Child:
                if (effort == BlockEffort.High)
                {
                    block.cardData = cardList[0].cardStagesHolder[0].cardDataList[Random.Range(0, cardList[0].cardStagesHolder[0].cardDataList.Length)];
                }
                else if (effort == BlockEffort.Medium)
                {
                    block.cardData = cardList[0].cardStagesHolder[1].cardDataList[Random.Range(0, cardList[0].cardStagesHolder[1].cardDataList.Length)];
                }
                else
                {
                    block.cardData = cardList[0].cardStagesHolder[2].cardDataList[Random.Range(0, cardList[0].cardStagesHolder[2].cardDataList.Length)];
                }
                break;
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
    public BlockEffort GetEffort { get { return effort; } }
    [SerializeField] List<Transform> spawnPos = new List<Transform>(3);
    public List<Transform> GetSpawnPos { get { return spawnPos; } }
}
