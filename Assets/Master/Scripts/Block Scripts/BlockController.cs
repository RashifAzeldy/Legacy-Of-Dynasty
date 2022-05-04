using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockController : MonoBehaviour
{
    [Header("Block Config : ")]
    [SerializeField] TextMeshProUGUI blockTitleText;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject blockSign;

    public CardDataBase cardData;

    private float _blockSpeed;
    public bool addCard;

    public GameObject GetBlockSign
    {
        get { return blockSign; }
        set { blockSign = value; }
    }

    void Update()
    {
        _blockSpeed = BlockSpawner.Instance.BlockSpeed;
        transform.Translate(-_blockSpeed * Time.deltaTime, 0, 0);
        if (cardData != null)
        {
            text.text = cardData.cardName;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(AddToActiveCard(0.5f));
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.GetComponent<BlockDestroyer>())
            DestroyObject();
    }

    public IEnumerator AddToActiveCard ( float delay )
    {
        yield return new WaitForSeconds(delay);
        BlockSpawner.Instance.GetActiveCards.Add(cardData);
    }

    public void InitBlock()
    {
        blockTitleText.text = cardData.cardName;
    }

    public void DestroyObject()
    {
        
        BlockSpawner.Instance.RemoveActiveCard(cardData);
    }
}
