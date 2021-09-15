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
            cacheController.DestroyObject();
        }
        if ( collider.GetComponent<StoryCardTextCrawl>() )
        {
            collider.GetComponent<StoryCardTextCrawl>().ResetText();
        }
    }

}
