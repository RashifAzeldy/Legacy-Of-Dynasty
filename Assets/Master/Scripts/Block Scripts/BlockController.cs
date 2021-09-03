using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [Header("Block Config : ")]
    [SerializeField] float blockSpeed = 3.5f;

    [SerializeField] public CardDataBase cardData;

    BlockSpawner spawner;

    void Start()
    {
        spawner = FindObjectOfType<BlockSpawner>();
    }
    void Update()
    {
        transform.Translate(-blockSpeed * Time.deltaTime, 0, 0);    
    }

    public void DestroyObject()
    {
        spawner.GetSpawnedBlock.RemoveAt(spawner.GetSpawnedBlock.IndexOf(this.gameObject));
        Destroy(gameObject);
    }
}
