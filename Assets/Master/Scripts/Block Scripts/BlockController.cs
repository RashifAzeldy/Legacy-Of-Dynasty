using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockController : MonoBehaviour
{
    [Header("Block Config : ")]
    [SerializeField] float blockSpeed = 3.5f;

    [SerializeField] public CardDataBase cardData;
    [SerializeField] TextMeshProUGUI text;

    BlockSpawner spawner;

    void Start()
    {
        spawner = FindObjectOfType<BlockSpawner>();
    }
    void Update()
    {
        transform.Translate(-blockSpeed * Time.deltaTime, 0, 0);    
        if(cardData != null )
        {
            text.text = cardData.cardName;
        }
    }

    public void DestroyObject()
    {
        spawner.GetSpawnedBlock.RemoveAt(spawner.GetSpawnedBlock.IndexOf(this.gameObject));
        Destroy(gameObject);
    }
}
