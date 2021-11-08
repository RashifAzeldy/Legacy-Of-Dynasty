using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyer : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] Vector2 offset;

    void OnTriggerEnter2D(Collider2D collider)
    {
        BlockController cacheController;

        if (cacheController = collider.gameObject.GetComponent<BlockController>())
        {
            string objectTagID;

            switch (cacheController.cardData.cardValue)
            {
                case CardValue.Positive:
                    objectTagID = "BlockPositive";
                    break;
                case CardValue.Negative:
                    objectTagID = "BlockNegative";
                    break;
                case CardValue.Mystery:
                    objectTagID = "BlockMystery";
                    break;
                case CardValue.Neutral:
                    objectTagID = "BlockNeutral";
                    break;
                default:
                    objectTagID = "BlockNeutral";
                    break;
            }

            ObjectPoolerManager.Instance.DestroyPoolObjectFromScene(objectTagID, collider.gameObject);
        }
        if ( collider.GetComponent<StoryCardTextCrawl>() )
        {
            collider.GetComponent<StoryCardTextCrawl>().ResetText();
        }
    }

}
