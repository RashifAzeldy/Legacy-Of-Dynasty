using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockDetector : MonoBehaviour
{
    [SerializeField] Transform tempSignPos;
    [SerializeField] GameObject signPrefab;
    [SerializeField] Transform parent;

    [SerializeField] Sprite positiveSign;
    [SerializeField] Sprite neutralSign;
    [SerializeField] Sprite negativeSign;
    [SerializeField] Sprite mysterySign;

    void ShowSign(BlockController block)
    {
        if (block.cardData)
        {
            GameObject sign = Instantiate(signPrefab, parent);
            block.GetBlockSign = sign;
            switch (block.cardData.cardValue)
            {
                case CardValue.Positive:
                    sign.GetComponent<Image>().sprite = positiveSign;
                    break;
                case CardValue.Negative:
                    sign.GetComponent<Image>().sprite = negativeSign;
                    break;
                case CardValue.Mystery:
                    sign.GetComponent<Image>().sprite = mysterySign;
                    break;
                case CardValue.Neutral:
                    sign.GetComponent<Image>().sprite = neutralSign;
                    break;
                default:
                    sign.GetComponent<Image>().sprite = neutralSign;
                    break;
            }

            RectTransform signRect = sign.GetComponent<RectTransform>();
            sign.GetComponent<Image>().preserveAspect = true;
            signRect.position = Camera.main.WorldToScreenPoint(new Vector3(
                0, block.transform.position.y, 0));
            signRect.position = new Vector3(tempSignPos.position.x, signRect.position.y, signRect.position.z);
        }
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( collision.gameObject.GetComponent<BlockController>() )
        {
            ShowSign(collision.gameObject.GetComponent<BlockController>());
        }
    }
}