using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool _created = false;
    //Accessible only trough editor or from this class
    [SerializeField] EquipedCostume playerEquipedCostume;

    public int hatIndex;

    private void Awake()
    {
        if (!_created)
        {
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(playerEquipedCostume);
            _created = true;
        }
    }
}