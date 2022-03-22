using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockController : MonoBehaviour
{
    [Header("Block Config : ")]
    //[SerializeField] public Vector2 blockSpeedRange = new Vector2(1.5f, 3.5f);
    [SerializeField] TextMeshProUGUI blockTitleText;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject blockSign;

    [HideInInspector] public CardDataBase cardData;

    private float _blockSpeed;

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

    public void InitBlock()
    {
        blockTitleText.text = cardData.cardName;
    }

    public void DestroyObject()
    {
        ObjectPoolerManager.Instance.DestroyPoolObjectFromScene(gameObject);

    }
}
