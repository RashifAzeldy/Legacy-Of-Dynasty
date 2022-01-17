using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] List<AchievementScriptableObj> achievementList = new List<AchievementScriptableObj>();
    public List<AchievementScriptableObj> GetAchievementList { get { return achievementList; } set { achievementList = value; } }
    [SerializeField] GameObject notifPrefab;

    public void AchievementProgress(PlayerController player, List<CardDataBase> collectedCard)
    {
        foreach (AchievementScriptableObj item in achievementList)
        {
            if (!item.isCompleted)
            {
                LODFunctionLibrary.CheckAchievementProgress(player, collectedCard, item, player.gameObject.GetComponent<PlayerStatus>());
                LODFunctionLibrary.AchievementCompleted(item);
            }
        }
    }
    /*
        public void SpawnAchivementNotification(AchievementScriptableObj achievement)
        {
            GameObject notifObject = Instantiate(notifPrefab, notifPrefab.GetComponent<AchievementNotification>().notifParent);
            LODFunctionLibrary.SetAchivementNotification(achievement, notifObject);
        }
        */
}
