using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using NaughtyAttributes;
using UnityEngine;
using UnityEditor;
public class BackgroundController : MonoBehaviour
{

    [Header("Config:")] 
    [SerializeField] private PlayerController m_player;

    [Header("Main Background:")]
    [SerializeField] private GameObject[] m_dayCycleRenderer;
    [SerializeField] private float m_dayCycleAnimRate = 0.75f;

    [Header("Layered Background:")] 
    [SerializeField] private BackgroundLayerData[] m_layerData;
    [SerializeField] private GameObject[] m_secondLayerProp;
    [SerializeField] private float m_secondLayerAnimRate;
    [SerializeField] private GameObject[] m_thirdLayerProp;
    [SerializeField] private float m_thirdLayerAnimRate;
    
    private float _mainBgLength;
    private float _backgroundStartXPos;

    private void Awake()
    {
        if (m_dayCycleRenderer.Length > 0)
            _mainBgLength = Mathf.Round((m_dayCycleRenderer[0].GetComponent<SpriteRenderer>().bounds.size.x));
        
        _backgroundStartXPos = m_dayCycleRenderer[0].gameObject.transform.position.x;
        
    }

    void LateUpdate()
    {
        BackgroundMovement(m_dayCycleRenderer, m_dayCycleAnimRate);
    }

    /// <summary>
    /// Move the background based on the animation rate
    /// </summary>
    /// <param name="renderers"></param>
    /// <param name="animationRate"></param>
    /// <param name="xDirection"></param>
    private void BackgroundMovement(GameObject[] renderers, float animationRate, int xDirection = -1)
    {
        if (renderers.Length > 0)
        {
            foreach (var bgRenderer in renderers)
            {
                Vector3 translation = new Vector3(xDirection * animationRate * Time.deltaTime, 0, 0);
                bgRenderer.gameObject.transform.Translate(translation);

                if (bgRenderer.transform.position.x < (_backgroundStartXPos - _mainBgLength))
                {
                    bgRenderer.gameObject.transform.position = new Vector3(
                        (_backgroundStartXPos + _mainBgLength) * (renderers.Length - 1),
                        bgRenderer.gameObject.transform.position.y, 0);
                }
            }
        }
    }
}


[CreateAssetMenu(menuName = "BackgroundTheme/LayerData", fileName = "LayerData")]
[System.Serializable]
public class BackgroundLayerData : ScriptableObject
{

    [SerializeField] private GameObject[] m_layerPropPrefabs;
    [SerializeField] private float m_propSpawnRate;
    [SerializeField] private float m_firstLayerAnimRate;

    public GameObject[] LayerPropPrefabs
    {
        get => m_layerPropPrefabs;
    }

    public float PropSpawnRate
    {
        get => m_propSpawnRate;
    }

    public float AnimRate
    {
        get => m_firstLayerAnimRate;
    }
    
}
