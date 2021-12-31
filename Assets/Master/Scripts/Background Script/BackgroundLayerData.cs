using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BackgroundTheme/PropsData", fileName = "PropsData")]
[System.Serializable]
public class BackgroundLayerData : ScriptableObject
{

    [SerializeField] private PropertiesData[] m_layerPropPrefabs;
    [SerializeField] private float m_propSpawnInterval = 3.5f;
    [SerializeField] private float m_layerAnimSpeed = 1.5f;
    
    public PropertiesData[] LayerPropPrefabs
    {
        get => m_layerPropPrefabs;
    }

    public float PropSpawnInterval
    {
        get => m_propSpawnInterval;
    }

    public float AnimSpeed
    {
        get => m_layerAnimSpeed;
    }
    
}

[System.Serializable]
public struct PropertiesData
{
    public GameObject Prefabs;
    public bool requirementEnable;
    public PlayerStatusData SpawnRequirement;

}