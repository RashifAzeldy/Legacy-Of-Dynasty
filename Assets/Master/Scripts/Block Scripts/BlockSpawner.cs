using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{

    [Header("Config : ")]
    [SerializeField] BlockController block;
    [SerializeField] List<CardDataList> cardList = new List<CardDataList>();
    [SerializeField] Transform spawnPoint;

    PlayerState player;

    private void Start()
    {
        player = FindObjectOfType<PlayerState>();
        SpawnObject(block, gameObject.transform, 5);
    }

    public void SpawnObject(BlockController spawnObj, Transform parent, int objQuantity)
    {

    }

    //void SpawnBlock(BlockController blockPrefab, )


    IEnumerator SpawnDelay(float time, BlockController spawnObj, Transform parent, int objQuantity, BlockEffort effort)
    {
        for (int i = 0; i < objQuantity; i++)
        {
            GameObject tempBlock;
            yield return new WaitForSeconds(time);
            tempBlock = Instantiate(spawnObj.gameObject, spawnPoint.position, Quaternion.identity, parent);

            BlockController tempBlockController = tempBlock.GetComponent<BlockController>();

            LODFunctionLibrary.FreezeYRigidbody(tempBlock);

            yield return new WaitForSeconds(0.15f);

            LODFunctionLibrary.RandomizeYPos(tempBlock, tempBlockController.cardData.cardSpawnAtEffort);
        }
    }
}
    // Child Index State = 0
    // Teen Index State = 1
    // Adult Index State = 2
    // Elder Index State = 3
 

public enum BlockEffort
{
    High,
    Normal,
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