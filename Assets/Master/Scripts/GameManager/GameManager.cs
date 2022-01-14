using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // For Checking Current Active Scene Name
    [SerializeField] string mainMenuScene;
    public string MainMenuSceneName { get { return mainMenuScene; } }
    [SerializeField] string skinScene;
    public string SkinSelectionScene { get { return skinScene; } }
    [SerializeField] string gameplayScene;
    public string GameplaySceneName { get { return gameplayScene; } }

    private static bool _created = false;

    [SerializeField] List<Costume> hatList = new List<Costume>();
    public List<Costume> GetHatList { get { return hatList; } }
    [SerializeField] List<AchievementDetails> detailList = new List<AchievementDetails>();

    [SerializeField] GameObject mainMenuUIBase;
    [SerializeField] GameObject achvDetailPrefab;
    Canvas mainMenuCanvas;

    public GameObject achievementDetailBase { get; private set; }

    [SerializeField] AchievementManager achievementManager;
    EquipedCostume playerEquipedCostume;

    #region Saved Data
    // public List<int> completedAchievementIndex = new List<int>();
    // public List<int> achievementProgressList = new List<int>();
    // public List<int> unlockedHatList = new List<int>();

    public PlayerData playerSavedData;
    //public int currentHatIndex;

    #endregion

    public static GameManager Instance { get; private set; }

    private void Awake()
    {

        if (Instance)
        {

            Destroy(this.gameObject);

        }
        else
        {

            LoadGameData();

            Instance = this;

            achievementManager = GetComponent<AchievementManager>();
            playerEquipedCostume = GetComponent<EquipedCostume>();

            Debug.Log(achievementManager.name + ", " + playerEquipedCostume.name);

            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(playerEquipedCostume);
            DontDestroyOnLoad(achievementManager);

        }


        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            MainMenuSetup();
        }

    }

    public void MainMenuSetup()
    {
        MainMenuManager menuManager = FindObjectOfType<MainMenuManager>();
        GameObject achWidget = menuManager.achievementWidget;

        foreach (AchievementScriptableObj item in achievementManager.GetAchievementList)
        {
            GameObject achievementDetail = Instantiate(achvDetailPrefab,
                achWidget.GetComponentInChildren<VerticalLayoutGroup>().transform);
            detailList.Add(achievementDetail.GetComponent<AchievementDetails>());
            achievementDetail.GetComponent<AchievementDetails>().SetDetails(item);
        }
    }

    public void SaveGameData()
    {
        PlayerData cacheSaveData = new PlayerData(CheckCompletedAchievementIndex(), CheckAchievementProgress(), CheckUnlockedHat(), playerSavedData.equipedHatIndex);

        SaveSystem.SavePlayerData(cacheSaveData);
    }

    public void LoadGameData()
    {

        PlayerData cachePData = SaveSystem.LoadPlayerData();

        if (cachePData != null)
        {

            playerSavedData.completedAchievementIndex = cachePData.completedAchievementIndex;
            playerSavedData.achievementProgress = cachePData.achievementProgress;
            playerSavedData.unlockedHatIndex = cachePData.unlockedHatIndex;
            playerSavedData.equipedHatIndex = cachePData.equipedHatIndex;

            for (int i = 0; i < detailList.Count; i++)
            {
                achievementManager.GetAchievementList[i].playerProgress = playerSavedData.achievementProgress[i];
                detailList[i].SetDetails(achievementManager.GetAchievementList[i]);
            }
            foreach (var item in playerSavedData.unlockedHatIndex)
            {
                hatList[item].IsCostumeUnlocked = true;
            }

        }

    }

    public List<int> CheckCompletedAchievementIndex()
    {
        List<int> result = new List<int>();
        foreach (var item in achievementManager.GetAchievementList)
        {
            if (item.isCompleted)
            {
                result.Add(achievementManager.GetAchievementList.IndexOf(item));
            }
        }
        return result;
    }

    public List<int> CheckAchievementProgress()
    {
        List<int> result = new List<int>();
        foreach (var item in achievementManager.GetAchievementList)
        {
            result.Add(item.playerProgress);
        }
        return result;
    }

    public List<int> CheckUnlockedHat()
    {
        List<int> result = new List<int>();
        foreach (Costume item in hatList)
        {
            if (item.IsCostumeUnlocked)
            {
                result.Add(hatList.IndexOf(item));
            }
        }
        return result;
    }

}