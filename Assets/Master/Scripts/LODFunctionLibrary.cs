using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LODFunctionLibrary
{
    public static void TeleportBlock(GameObject block, Vector3 destination)
    {
        block.transform.position = destination;
    }

    /*
    public static void ChangeStoryCard(storyCard, gameObject(SCObject))
    {
          gameObject.SCObject.GetStoryCard = storyCard;
    }
    */
}