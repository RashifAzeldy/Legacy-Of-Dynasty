using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestingScript : MonoBehaviour
{
    [SerializeField] GameObject testObjPrefab;

    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.X) )
        {
            BlockSpawner block = FindObjectOfType<BlockSpawner>();
            block.SpawnObject(testObjPrefab, gameObject.transform, 5, BlockEffort.High);
            block.SpawnObject(testObjPrefab, gameObject.transform, 5, BlockEffort.Medium);
            block.SpawnObject(testObjPrefab, gameObject.transform, 5, BlockEffort.Low);
        }
    }
}