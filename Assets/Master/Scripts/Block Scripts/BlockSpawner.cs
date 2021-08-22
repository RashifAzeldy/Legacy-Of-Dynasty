using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] BlockController block;
<<<<<<< Updated upstream
    [SerializeField] List<CardStagesList> cardList = new List<CardStagesList>();
=======
    [SerializeField] CardStagesList cardList;
    [SerializeField] List<EffortPosition> spawnPositions = new List<EffortPosition>();
>>>>>>> Stashed changes

    PlayerState player;

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
<<<<<<< Updated upstream
        StartCoroutine(SpawnDelay(1f, spawnObj, parent, objQuantity, BlockEffort.High));
        StartCoroutine(SpawnDelay(1f, spawnObj, parent, objQuantity, BlockEffort.Medium));
        StartCoroutine(SpawnDelay(1f, spawnObj, parent, objQuantity, BlockEffort.Low));
=======
        player = FindObjectOfType<PlayerState>();
        StartCoroutine(SpawnDelay(1f, block, gameObject.transform, 5, BlockEffort.High));
        StartCoroutine(SpawnDelay(1f, block, gameObject.transform, 5, BlockEffort.Medium));
        StartCoroutine(SpawnDelay(1f, block, gameObject.transform, 5, BlockEffort.Low));
>>>>>>> Stashed changes
    }

    IEnumerator SpawnDelay(float time, BlockController spawnObj, Transform parent, int objQuantity, BlockEffort effort)
    {
        for (int i = 0; i < objQuantity; i++)
        {
            GameObject block;
            yield return new WaitForSeconds(time);
<<<<<<< Updated upstream
            block = Instantiate(spawnObj.gameObject, new Vector3(16.5f, 0, 0), Quaternion.identity, parent);
            AddBlockStoryCard(cardList, player.GetPlayerCurrentState, block.GetComponent<BlockController>(), effort);
=======
            block = Instantiate(spawnObj.gameObject, new Vector3(15, 0, 0), Quaternion.identity, parent);
            //AddBlockStoryCard(cardList, player.GetPlayerCurrentState, block.GetComponent<BlockController>());
>>>>>>> Stashed changes
            LODFunctionLibrary.FreezeYRigidbody(block);

            yield return new WaitForSeconds(0.15f);
            block.transform.position = spawnPositions[SpawnPosIndex(effort)].GetSpawnPos[Random.Range(0, 3)].position;
        }
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

<<<<<<< Updated upstream
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
=======
[System.Serializable]
public class EffortPosition
{
    [SerializeField] BlockEffort effort;
    public BlockEffort GetEffort { get { return effort; } }
    [SerializeField] List<Transform> spawnPos = new List<Transform>(3);
    public List<Transform> GetSpawnPos { get { return spawnPos; } }
}
>>>>>>> Stashed changes
