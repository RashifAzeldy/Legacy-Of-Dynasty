using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMotor : MonoBehaviour
{
    void Update()
    {
        transform.Translate(-3.5f * Time.deltaTime, 0, 0);

        // Teleport Block Later
        //if ( gameObject.transform.position.x <= -4f )
        //{
        //    LODFunctionLibrary.TeleportBlock(gameObject, new Vector3(55f, 0f));
        //    LODFunctionLibrary.RandomizeYPos(gameObject, Effort??);
        //}
    }
}
