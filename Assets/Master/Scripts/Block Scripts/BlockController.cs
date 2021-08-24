using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public CardDataBase cardData;

    void Update()
    {
        transform.Translate(-3.5f * Time.deltaTime, 0, 0);
    }
}
