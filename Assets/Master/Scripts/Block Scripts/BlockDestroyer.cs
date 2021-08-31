using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyer : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        BlockController cacheController;
        if (cacheController = collider.gameObject.GetComponent<BlockController>())
        {
            cacheController.DestroyObject();
        }
    }

}