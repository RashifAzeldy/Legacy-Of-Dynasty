using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LODFunctionLibrary
{
    public static void TeleportBlock(GameObject block, Vector3 destination)
    {
        block.transform.position = destination;
    }

    public static void RandomizeYPos(GameObject block, BlockEffort blockEffort)
    {
        float result;
        if (blockEffort == BlockEffort.High)
        {
            result = Random.Range(1f, 5.5f);
            block.transform.position = new Vector3(block.transform.position.x, result, block.transform.position.z);
        }
        else if (blockEffort == BlockEffort.Medium)
        {
            result = Random.Range(-4f, 0f);
            block.transform.position = new Vector3(block.transform.position.x, result, block.transform.position.z);
        }
        else
        {
            result = Random.Range(-8.6f, -5f);
            block.transform.position = new Vector3(block.transform.position.x, result, block.transform.position.z);
        }
    }

    public static void FreezeYRigidbody(GameObject gameObj)
    {
        gameObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    /*
    public static void ChangeStoryCard(storyCard, gameObject(SCObject))
    {
        gameObject.SCObject.GetStoryCard = storyCard;
    }
    */
}