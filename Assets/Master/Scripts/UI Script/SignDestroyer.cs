using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D( Collider2D other )
    {
        if ( other.gameObject.GetComponent<BlockController>() )
        {
            GameObject destroyedSign = other.GetComponent<BlockController>().GetBlockSign;
            Destroy(destroyedSign);
            other.GetComponent<BlockController>().GetBlockSign = null;
        }
    }
}
