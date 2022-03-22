
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackgroundController : MonoBehaviour
{

    [Header("Config:")]
    [SerializeField] private PlayerStatus m_player;

    [Header("Main Background:")]
    [SerializeField]
    private GameObject[] m_dayCycleRenderer;

    [SerializeField] private float m_dayCycleAnimRate = 0.75f;

    [Header("Layer Properties:")]
    [SerializeField] private BackgroundLayerData m_firstLayerProps;
    [SerializeField] private BackgroundLayerData m_secondLayerProps;
    [SerializeField] private BackgroundLayerData m_thirdLayerProps;
    [Space]
    [Header("Layer Properties Parent:")]
    [SerializeField] private Transform m_firstLayerParent;
    [SerializeField] private Transform m_secondLayerParent;
    [SerializeField] private Transform m_thirdLayerParent;

    private Camera _playerCamera;

    private List<GameObject> _currentPropObjects = new List<GameObject>();

    private float _mainBgLength;
    private float _backgroundStartXPos;

    private float _firstLayerSpawnTimer = 0;
    private float _secondLayerSpawnTimer = 0;
    private float _thirdLayerSpawnTimer = 0;

    private void Awake()
    {

        if (!_playerCamera)
            _playerCamera = Camera.main;

        if (!m_player)
            m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();

        if (m_dayCycleRenderer.Length > 0)
            _mainBgLength = Mathf.Round((m_dayCycleRenderer[0].GetComponent<SpriteRenderer>().bounds.size.x));

        _backgroundStartXPos = m_dayCycleRenderer[0].gameObject.transform.position.x;

    }

    void LateUpdate()
    {
        BackgroundMovement(m_dayCycleRenderer, m_dayCycleAnimRate);

        LayerPropsSpawner();

        MoveProps(_currentPropObjects, m_firstLayerProps.AnimSpeed);

        foreach (var propObject in _currentPropObjects)
        {
            float propObjectBounds = _playerCamera.transform.position.x - _playerCamera.orthographicSize;
            if (propObject.transform.position.x < propObjectBounds)
            {
                gameObject.DestroyPool(propObject);
            }
        }

    }

    private void LayerPropsSpawner()
    {

        if (_firstLayerSpawnTimer >= m_firstLayerProps.PropSpawnInterval)
        {
            _currentPropObjects.Add(PropsSpawner(m_firstLayerProps, m_firstLayerParent));
            _firstLayerSpawnTimer = 0;
        }
        else
            _firstLayerSpawnTimer += Time.deltaTime;

        if (_secondLayerSpawnTimer >= m_secondLayerProps.PropSpawnInterval)
        {
            _secondLayerSpawnTimer = 0;

        }
        else
            _secondLayerSpawnTimer += Time.deltaTime;


        if (_thirdLayerSpawnTimer >= m_thirdLayerProps.PropSpawnInterval)
        {
            _currentPropObjects.Add(PropsSpawner(m_firstLayerProps, m_firstLayerParent));
            _thirdLayerSpawnTimer = 0;
        }
        else
            _thirdLayerSpawnTimer += Time.deltaTime;

    }

    /// <summary>
    /// Move the background based on the moveSpeed
    /// </summary>
    /// <param name="renderers"></param>
    /// <param name="moveSpeed"></param>
    /// <param name="xDirection"></param>
    private void BackgroundMovement(GameObject[] renderers, float moveSpeed)
    {
        if (renderers.Length > 0)
        {
            foreach (var bgRenderer in renderers)
            {
                bgRenderer.gameObject.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);

                if (bgRenderer.transform.position.x < (_backgroundStartXPos - _mainBgLength))
                {
                    bgRenderer.gameObject.transform.position = new Vector3(
                        (_backgroundStartXPos + _mainBgLength) * (renderers.Length - 1),
                        bgRenderer.gameObject.transform.position.y, 0);
                }
            }
        }
    }

    private void MoveProps(List<GameObject> propGameObjects, float moveSpeed)
    {
        if (propGameObjects.Count > 0)
        {
            foreach (var gObject in propGameObjects)
            {
                gObject.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            }
        }
    }

    private GameObject SpawnProp(PropertiesData propsData, Transform spawnParent)
    {
        if (propsData.requirementEnable)
        {
            if (LODFunctionLibrary.ComparePlayerStatusData(m_player.playerStatusData, propsData.SpawnRequirement, ScoreCheck.Equal))
                return gameObject.InstantiatePool(propsData.Prefabs, transform.position, Quaternion.identity);
        }
        else
            return gameObject.InstantiatePool(propsData.Prefabs, spawnParent.transform.position, Quaternion.identity);

        return null;
    }

    private GameObject PropsSpawner(BackgroundLayerData layerData, Transform spawnParent)
    {
        GameObject cPropObj = null;
        _firstLayerSpawnTimer = 0;

        while (!cPropObj)
        {
            cPropObj =
                SpawnProp(layerData.LayerPropPrefabs[Random.Range(0, layerData.LayerPropPrefabs.Length)], spawnParent);
        }

        return cPropObj;
    }
}