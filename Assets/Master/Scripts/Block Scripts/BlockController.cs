using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockController : MonoBehaviour
{
    [Header("Block Config : ")]
    [SerializeField] public Vector2 blockSpeedRange = new Vector2(1.5f, 3.5f);
    [SerializeField] TextMeshProUGUI blockTitleText;

    [HideInInspector] public CardDataBase cardData;
    
    public float blockSpeed { get; set; }

    BlockSpawner spawner;
    

    void Start()
    {
        spawner = FindObjectOfType<BlockSpawner>();
    }
    void Update()
    {
        transform.Translate(-blockSpeed * Time.deltaTime, 0, 0);    
    }

    public void InitBlock()
    {
        blockTitleText.text = cardData.cardName;
    }

    public void DestroyObject()
    {
        spawner.GetSpawnedBlock.RemoveAt(spawner.GetSpawnedBlock.IndexOf(this.gameObject));
        Destroy(gameObject);
    }
}
