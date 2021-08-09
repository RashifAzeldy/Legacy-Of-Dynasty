using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [Range(0.01f, 1f)]
    [SerializeField] float smooth = 1f;

    private Vector3 velo = Vector3.zero;

    private void Update()
    {
        Vector3 targetPos = target.TransformPoint(new Vector3(0, 9.5f, -10));

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velo, smooth);
    }
}
