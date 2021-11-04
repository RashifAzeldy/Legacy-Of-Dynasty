using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumePosData : MonoBehaviour
{
    [SerializeField] EquipedCostume costumeData;
    [SerializeField] SpriteRenderer playerHat;

    public SpriteRenderer GetPlayerHat { get { return playerHat; } set { playerHat = value; } }

    private void Start()
    {
        costumeData = FindObjectOfType<EquipedCostume>();
        EquipCostume();
    }

    public void EquipCostume()
    {
        if (costumeData != null)
            playerHat.sprite = costumeData.hat;
    }
}
