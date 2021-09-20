using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField] List<GameObject> grounds = new List<GameObject>();

    [SerializeField] float groundSpeed = 1f;

    void Update()
    {
        foreach ( GameObject item in grounds )
        {
            if ( item.transform.position.x <= -11.34f )
            {
                item.transform.position = new Vector3(38.6f, -7.363f, 0);
            }
            item.transform.Translate(-1 * groundSpeed * Time.deltaTime, 0, 0);
        }
    }
}
