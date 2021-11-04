using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] PlayerStatus status;
    [SerializeField] PlayerState state;
    [SerializeField] AchievementManager achievementManager;
    [SerializeField] MenuManager menuManager;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject deadMenu;
    [SerializeField] TextMeshProUGUI causingDeathText;
    [SerializeField] GameObject pauseButton;

    [Space]
    [Header("Dynasty Score : ")]
    // Dynasty Score
    [SerializeField] Transform dynastyScoreParent;
    [SerializeField] GameObject dynastyScorePrefab;
    [Space]

    // Collected Activities
    [Header("Collected Activities / Story Card : ")]
    [SerializeField] Transform collectedActParent;
    [SerializeField] GameObject collectedActPrefab;
    [Space]

    // Character
    [Header("Character : ")]
    [SerializeField] Transform characterParent;
    [SerializeField] GameObject characterPrefab;
    [Space]

    [SerializeField] TextMeshProUGUI finalScoreText;

    public bool gameOver;
    public TextMeshProUGUI SetCausingDeath
    {
        get { return causingDeathText; }
        set { causingDeathText = value; }
    }

    private void Start()
    {
        gameOverMenu.SetActive(false);
        achievementManager = FindObjectOfType<AchievementManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShowGameOverMenu();
        }
    }

    public void ShowGameOverMenu()
    {
        pauseButton.SetActive(false);
        #region Add Score
        for (int i = 0; i < player.GetScoreList.Count; i++)
        {
            GameObject dyScore = Instantiate(dynastyScorePrefab, dynastyScoreParent);
            dyScore.GetComponent<TextMeshProUGUI>().text = "" + player.GetScoreList[i];
        }
        #endregion
        #region Show Collected Activities
        for (int i = 0; i < player.GetCollectedCards.GetCollectedStoryCard.Count; i++)
        {
            GameObject actText = Instantiate(collectedActPrefab, collectedActParent);
            actText.GetComponent<TextMeshProUGUI>().text = player.GetCollectedCards.GetCollectedStoryCard[i].cardName
                + " | " + player.GetCollectedCards.GetCollectedStoryCard[i].cardScoreValue;
        }
        #endregion
        #region Show Character Per Dynasty
        state.AddCharaImage();
        for (int i = 0; i < state.GetCharacter.Count; i++)
        {
            GameObject chara = Instantiate(characterPrefab, characterParent);
            CharaListData data = chara.GetComponent<CharaListData>();

            data.dynastyText.text = "Dynasty : " + (i + 1);
            if (state.GetCharacter.Count != 0)
            {
                data.characterImage.sprite = state.GetCharacter[i];
            }
            else
            {
                data.characterImage.sprite = state.PlayerImageCharacter.sprite;
            }
        }
        #endregion
        gameOver = true;
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
        player.GetScoreList.Add(status.playerStatusData.PlayerScore);
        finalScoreText.text = "" + LODFunctionLibrary.CountFinalScore(player.GetScoreList);
        pauseButton.SetActive(false);
        achievementManager.AchievementProgress(player, player.GetCollectedCards.GetCollectedStoryCard);
    }

    public void ShowDeadMenu()
    {
        Time.timeScale = 0;
        deadMenu.SetActive(true);
        pauseButton.SetActive(false);
        player.GetScoreList.Add(status.playerStatusData.PlayerScore);
        achievementManager.AchievementProgress(player, player.GetCollectedCards.GetCollectedStoryCard);
    }

    public void ContinueAsChild()
    {
        deadMenu.SetActive(false);
        pauseButton.SetActive(true);
        menuManager.UnpauseGame();
        player.GetScoreList.Add(status.playerStatusData.PlayerScore);
        status.playerStatusData.PlayerScore = 0;
        player.GetScoreText.text = status.playerStatusData.PlayerScore.ToString();
        // Re-Spawn All Blocks with Player Spawn Down Here : 
    }

    public void CostumeSelection()
    {
        SceneManager.LoadScene("Skin Testing");
        Time.timeScale = 1;
    }
}
