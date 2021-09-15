using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioning : MonoBehaviour
{
    [SerializeField] Transform playerDefaultPos;
    [SerializeField] BlockDestroyer destroyer;

    [SerializeField] Vector3 destroyerOffset;
    [SerializeField] Vector3 camOffset;
 
    void Start()
    {
        transform.position = playerDefaultPos.position + new Vector3(LODFunctionLibrary.CheckCamera(GetComponent<Camera>()).y / 2, 0, 0) + camOffset;    
        destroyer.transform.position = new Vector3(LODFunctionLibrary.CheckCamera(GetComponent<Camera>()).x, destroyer.transform.position.y, 0) + destroyerOffset;
    }
}
