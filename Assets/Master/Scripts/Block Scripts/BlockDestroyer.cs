using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyer : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] Vector2 offset; 

    private BlockController _blockController;
    
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        _blockController = !_blockController ? collider.gameObject.GetComponent<BlockController>() : _blockController;

        if (_blockController)
        {
            ObjectPoolerManager.Instance.DestroyPoolObjectFromScene(collider.gameObject);
        }
        if ( collider.GetComponent<StoryCardTextCrawl>() )
        {
            collider.GetComponent<StoryCardTextCrawl>().ResetText();
        }
    }

}
