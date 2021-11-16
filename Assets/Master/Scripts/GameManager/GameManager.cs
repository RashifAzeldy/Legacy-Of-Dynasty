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

    public  GameObject achievementObject { get; private set; }

    private static bool _created = false;
    //Accessible only trough editor or from this class
    [SerializeField] EquipedCostume playerEquipedCostume;
    [SerializeField] AchievementManager achievementManager;

    public int hatIndex;

    [SerializeField] GameObject mainMenuUIBase;
    [SerializeField] GameObject achvDetailPrefab;
    [SerializeField] GameObject backButton;
    Canvas mainMenuCanvas;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(playerEquipedCostume);
            DontDestroyOnLoad(achievementManager);
            _created = true;

            Instance = this;

        }
        else
            Destroy(this.gameObject);

        MainMenuSetup();

    }

    public void MainMenuSetup()
    {
        mainMenuCanvas = FindObjectOfType<Canvas>();
        
        GameObject menuUIBase = Instantiate(mainMenuUIBase, mainMenuCanvas.transform);

        achievementObject = menuUIBase;

        foreach (AchievementScriptableObj item in achievementManager.GetAchievementList)
        {
            GameObject achievementDetail = Instantiate(achvDetailPrefab,
                menuUIBase.GetComponentInChildren<VerticalLayoutGroup>().transform);
            achievementDetail.GetComponent<AchievementDetails>().SetAchivementDetails = item;
        }
        Instantiate(backButton, menuUIBase.GetComponentInChildren<VerticalLayoutGroup>().transform);
    }

    public void GameplaySetup()
    {
        FindObjectOfType<GameOverManager>().m_gameManager = this;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(skinScene, LoadSceneMode.Single);
        }
#endif
    }
}