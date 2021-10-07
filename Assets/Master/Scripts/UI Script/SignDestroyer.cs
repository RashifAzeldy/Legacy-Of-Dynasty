using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D( Collider2D other )
    {
        if ( !other.gameObject.GetComponent<PlayerController>() )
        {
            GameObject destroyedSign = other.GetComponent<BlockController>().GetBlockSign;
            other.GetComponent<BlockController>().GetBlockSign = null;
            Destroy(destroyedSign);
        }
    }
}
