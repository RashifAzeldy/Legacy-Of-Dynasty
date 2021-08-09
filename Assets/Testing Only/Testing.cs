using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] GameObject testObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(Mathf.Abs(10 - 5));
            Debug.Log(Mathf.Abs(29 - 102));
        }
    }
}
