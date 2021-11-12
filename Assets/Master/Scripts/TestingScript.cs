using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestingScript : MonoBehaviour
{
    [SerializeField] AchievementScriptableObj achievement;
    [SerializeField] Canvas parent;
    [SerializeField] GameObject testObjPrefab;

    private void Start()
    {
        Debug.Log("WWWW");
        GameObject test = Instantiate(testObjPrefab, parent.transform);
        test.GetComponent<AchievementDetails>().SetAchivementDetails = achievement;
    }
}