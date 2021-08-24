using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] float blockSpeed = 3.5f;
    public CardData cardData;
    BlockSpawner spawner;
    void Start()
    {
        spawner = FindObjectOfType<BlockSpawner>();
    }
    void Update()
    {
        transform.Translate(-blockSpeed * Time.deltaTime, 0, 0);
        if (gameObject.transform.position.x <= -3.5f)
        {
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        spawner.GetSpawnedBlock.RemoveAt(spawner.GetSpawnedBlock.IndexOf(this.gameObject));
        Destroy(gameObject);
    }
}
