using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] GameObject skyBG;
    [SerializeField] GameObject propPrefabA;
    [SerializeField] GameObject propPrefabB;
    [SerializeField] GameObject propPrefabC;

    [SerializeField] List<GameObject> propAList = new List<GameObject>();
    [SerializeField] List<GameObject> propBList = new List<GameObject>();
    [SerializeField] List<GameObject> propCList = new List<GameObject>();

    float scaleA;
    float scaleB;
    float scaleC;

    public Vector3 RandomizePos( float minX, float maxX )
    {
        Vector3 result;
        float xResult;
        xResult = Random.Range(minX, maxX);
        result = new Vector3(xResult, -4.77f, -1f);

        return result;
    }

    public float RandomizeSize( float minScale, float maxScale )
    {
        float scale = Random.Range(minScale, maxScale);

        return scale;
    }

    private void Start()
    {
        for ( int i = 0; i < 5; i++ )
        {
            scaleA = RandomizeSize(0.5f, 1.5f);
            // Instantiate Prop A
            GameObject propA = Instantiate(propPrefabA, RandomizePos(9.41f, 46.5f), Quaternion.identity);
            propA.transform.localScale += new Vector3(scaleA, scaleA, 0);
            propAList.Add(propA);

            scaleB = RandomizeSize(0.5f, 1.5f);
            // Instantiate Prop B
            GameObject propB = Instantiate(propPrefabB, RandomizePos(9.41f, 46.5f), Quaternion.identity);
            propB.transform.localScale += new Vector3(scaleB, scaleB, 0);
            propBList.Add(propB);

            scaleC = RandomizeSize(0.5f, 1.5f);
            // Instantiate Prop C
            GameObject propC = Instantiate(propPrefabC, RandomizePos(9.41f, 46.5f), Quaternion.identity);
            propC.transform.localScale += new Vector3(scaleC, scaleC, 0);
            propCList.Add(propC);
        }
    }

    private void Update()
    {
        if ( skyBG != null )
        {
            SpriteRenderer render = skyBG.GetComponent<SpriteRenderer>();
            Vector2 offset = new Vector2(-1.0f * Time.deltaTime, 0);
            render.material.SetTextureOffset("_MainTex", offset);
        }

        foreach ( GameObject item in propAList )
        {
            if ( item.transform.localScale.x < 1 )
            {
                item.transform.Translate(new Vector3(-0.05f * Time.deltaTime, 0, 0));
            }
            else if ( item.transform.localScale.x >= 1 && item.transform.localScale.x < 1.25f )
            {
                item.transform.Translate(new Vector3(-0.25f * Time.deltaTime, 0, 0));
            }
            else
            {
                item.transform.Translate(new Vector3(-0.5f * Time.deltaTime, 0, 0));
            }

            if ( item.transform.position.x <= -6f )
            {
                scaleA = RandomizeSize(0.5f, 1.5f);
                item.transform.localScale = new Vector3(scaleA, scaleA, 0);
                item.transform.position = RandomizePos(4f, 46.5f);
            }
        }

        foreach ( GameObject item in propBList )
        {
            if ( item.transform.localScale.x < 1 )
            {
                item.transform.Translate(new Vector3(-0.05f * Time.deltaTime, 0, 0));
            }
            else if ( item.transform.localScale.x >= 1 && item.transform.localScale.x < 1.25f )
            {
                item.transform.Translate(new Vector3(-0.25f * Time.deltaTime, 0, 0));
            }
            else
            {
                item.transform.Translate(new Vector3(-0.5f * Time.deltaTime, 0, 0));
            }

            if ( item.transform.position.x <= -6f )
            {
                scaleB = RandomizeSize(0.5f, 1.5f);
                item.transform.localScale = new Vector3(scaleB, scaleB, 0);
                item.transform.position = RandomizePos(3f, 46.5f);
            }
        }

        foreach ( GameObject item in propCList )
        {
            if ( item.transform.localScale.x < 1 )
            {
                item.transform.Translate(new Vector3(-0.05f * Time.deltaTime, 0, 0));
            }
            else if ( item.transform.localScale.x >= 1 && item.transform.localScale.x < 1.25f )
            {
                item.transform.Translate(new Vector3(-0.25f * Time.deltaTime, 0, 0));
            }
            else
            {
                item.transform.Translate(new Vector3(-0.5f * Time.deltaTime, 0, 0));
            }

            if ( item.transform.position.x <= -6f )
            {
                scaleC = RandomizeSize(0.5f, 1.5f);
                item.transform.localScale = new Vector3(scaleC, scaleC, 0);
                item.transform.position = RandomizePos(3f, 46.5f);
            }
        }
    }
}
