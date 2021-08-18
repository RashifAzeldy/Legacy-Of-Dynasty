using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] CardStagesList cardList;
    public void SpawnObject(GameObject spawnObj, Transform parent, int objQuantity, BlockEffort effortType)
    {
        StartCoroutine(SpawnDelay(1f, spawnObj, parent, objQuantity, effortType));
    }

    IEnumerator SpawnDelay(float time, GameObject spawnObj, Transform parent, int objQuantity, BlockEffort effortType)
    {
        for (int i = 0; i < objQuantity; i++)
        {
            GameObject block;
            yield return new WaitForSeconds(time);
            if (effortType == BlockEffort.High)
            {
                block = Instantiate(spawnObj, new Vector3(15, 0, 0), Quaternion.identity, parent);
                LODFunctionLibrary.RandomizeYPos(block, BlockEffort.High);
            }
            else if (effortType == BlockEffort.Medium)
            {
                block = Instantiate(spawnObj, new Vector3(15, 0, 0), Quaternion.identity, parent);
                LODFunctionLibrary.RandomizeYPos(block, BlockEffort.Medium);
            }
            else
            {
                block = Instantiate(spawnObj, new Vector3(15, 0, 0), Quaternion.identity, parent);
                LODFunctionLibrary.RandomizeYPos(block, BlockEffort.Low);
            }
            yield return new WaitForSeconds(0.25f);
            SetBlockComponent(block);
        }
    }

    void SetBlockComponent(GameObject block)
    {
        LODFunctionLibrary.FreezeYRigidbody(block);
        LODFunctionLibrary.SetBlockStoryCard(block.GetComponent<BlockController>(), cardList.cardStagesHolder[0].cardDataList[0]);
    }

}

public enum BlockEffort
{
    High,
    Medium,
    Low
}