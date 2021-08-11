using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public void SpawnObject( GameObject spawnObj, Vector3 position, Transform parent, int objQuantity )
    {
        StartCoroutine(SpawnDelay(1f, spawnObj, position, parent, 5));
    }

    IEnumerator SpawnDelay( float time, GameObject spawnObj, Vector3 pos, Transform parent, int objQuantity )
    {
        for ( int i = 0; i < objQuantity; i++ )
        {
            yield return new WaitForSeconds(time);
            GameObject block = Instantiate(spawnObj, pos, Quaternion.identity, parent);
            // AddBlockComponent(block);
        }
    }

    //public void AddBlockComponent(GameObject gameObj)
    //{

    //}
}
