using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] BlockController block;
    [SerializeField] List<CardStagesList> cardList = new List<CardStagesList>();

    PlayerState player;
    private void Start()
    {
        player = FindObjectOfType<PlayerState>();
        SpawnObject(block, gameObject.transform, 5);
    }
    public void SpawnObject(BlockController spawnObj, Transform parent, int objQuantity)
    {
        StartCoroutine(SpawnDelay(1f, spawnObj, parent, objQuantity, BlockEffort.High));
        StartCoroutine(SpawnDelay(1f, spawnObj, parent, objQuantity, BlockEffort.Medium));
        StartCoroutine(SpawnDelay(1f, spawnObj, parent, objQuantity, BlockEffort.Low));
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
            if (block.GetComponent<BlockController>().cardData.card.cardSpawnAtEffort == BlockEffort.High)
            {
                LODFunctionLibrary.RandomizeYPos(block, BlockEffort.High);
            }
            else if (block.GetComponent<BlockController>().cardData.card.cardSpawnAtEffort == BlockEffort.Medium)
            {
                LODFunctionLibrary.RandomizeYPos(block, BlockEffort.Medium);
            }
            else
            {
                LODFunctionLibrary.RandomizeYPos(block, BlockEffort.Low);
            }
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