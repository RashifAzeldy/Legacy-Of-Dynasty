using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] BlockController block;
    [SerializeField] CardStagesList cardList;

    PlayerState player;
    private void Start()
    {
        player = FindObjectOfType<PlayerState>();
        SpawnObject(block, gameObject.transform, 5);
    }
    public void SpawnObject(BlockController spawnObj, Transform parent, int objQuantity)
    {
        StartCoroutine(SpawnDelay(1f, spawnObj, parent, objQuantity));
    }

    IEnumerator SpawnDelay(float time, BlockController spawnObj, Transform parent, int objQuantity)
    {
        for (int i = 0; i < objQuantity; i++)
        {
            GameObject block;
            yield return new WaitForSeconds(time);
            block = Instantiate(spawnObj.gameObject, new Vector3(15, 0, 0), Quaternion.identity, parent);
            AddBlockStoryCard(cardList, player.GetPlayerCurrentState, block.GetComponent<BlockController>());
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
    void AddBlockStoryCard(CardStagesList cardList, Age playerState, BlockController block)
    {
        switch (playerState)
        {
            case Age.Child:
                int randomIndex = Random.Range(0, 2);
                block.cardData = cardList.cardStagesHolder[0].cardDataList[randomIndex];
                Debug.Log(cardList.cardStagesHolder[0].cardDataList[randomIndex] + ", " + block.cardData.card.cardName);
                break;
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
        }
    }
}

public enum BlockEffort
{
    High,
    Medium,
    Low
}