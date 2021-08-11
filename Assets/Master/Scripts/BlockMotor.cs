using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMotor : MonoBehaviour
{
    [SerializeField] List<GameObject> blockList = new List<GameObject>();
    public List<GameObject> GetBlockList { get { return blockList; } set { blockList = value; } }
  
    void Update()
    {
        if(blockList.Count > 2 )
        {
            foreach ( GameObject item in blockList )
            {
                item.transform.Translate(-3.5f * Time.deltaTime, 0, 0);
            }
        }    
    }
}
