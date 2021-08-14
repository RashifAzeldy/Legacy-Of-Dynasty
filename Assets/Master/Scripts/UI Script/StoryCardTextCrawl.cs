using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCardTextCrawl : MonoBehaviour
{
    bool move;
    public bool MoveText { get { return move; } set { move = value; } }
    void Update()
    {
        if (move)
        {
            gameObject.transform.Translate(-5f * Time.deltaTime, 0, 0);
        }
    }
}
