using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMotor : MonoBehaviour
{
    void Update()
    {
        transform.Translate(-3.5f * Time.deltaTime, 0, 0);

        // Teleport Block Later
        //if ( gameObject.transform.position.x >= Desired Pos )
        //{
        //    LODFunctionLibrary.TeleportBlock(block/gameObject, destination);
        //}
    }
}
