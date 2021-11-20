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
    public List<int> completedAchievementIndex = new List<int>();
    public List<int> achievementProgressList = new List<int>();
    public List<int> unlockedHatList = new List<int>();
    public int hatIndex; // Save !
    #endregion

    public static GameManager Instance { get; private set; }

    private void Awake()
    {

        achievementManager = GetComponent<AchievementManager>();
        playerEquipedCostume = GetComponent<EquipedCostume>();

        if (!Instance)
        {

            Instance = this;

            Debug.Log(achievementManager.name + ", " + playerEquipedCostume.name);

            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(playerEquipedCostume);
            DontDestroyOnLoad(achievementManager);

        }
        else
        {
            Debug.Log(this.gameObject.name);
            Destroy(this.gameObject);
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

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(skinScene, LoadSceneMode.Single);
        }
#endif

        if (Input.GetKeyDown(KeyCode.S))
        {
            LODFunctionLibrary.SaveGame();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            LODFunctionLibrary.LoadGame();
            for (int i = 0; i < detailList.Count; i++)
            {
                achievementManager.GetAchievementList[i].playerProgress = achievementProgressList[i];
                detailList[i].SetDetails(achievementManager.GetAchievementList[i]);
            }
            foreach (var item in unlockedHatList)
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