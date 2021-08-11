using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    BlockMotor motor;

    private void Start()
    {
        motor = FindObjectOfType<BlockMotor>();
    }

    public void SpawnObject( GameObject spawnObj, Vector3 position, Transform parent, int objQuantity )
    {
        StartCoroutine(SpawnDelay(1f, spawnObj, position, parent, 5));
    }

    IEnumerator SpawnDelay( float time, GameObject spawnObj, Vector3 pos, Transform parent, int objQuantity )
    {
        for ( int i = 0; i < objQuantity; i++ )
        {
            yield return new WaitForSeconds(time);
            GameObject block = Instantiate(spawnObj, pos + new Vector3(0, Random.Range(2, -2), 0), Quaternion.identity, parent);
            AddBlockComponent(block);
            motor.GetBlockList.Add(block);
        }
    }

    public void AddBlockComponent(GameObject gameObj)
    {
        //gameObj.AddComponent<Rigidbody2D>();
        gameObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }
}
