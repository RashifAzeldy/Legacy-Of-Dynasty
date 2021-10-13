using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] PlayerStatus status;
    [SerializeField] PlayerState state;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject deadMenu;

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

    private void Start()
    {
        gameOverMenu.SetActive(false);
    }

    public void ShowGameOverMenu()
    {
        gameOver = true;
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
        player.GetScoreList.Add(status.playerStatusData.PlayerScore);
        finalScoreText.text = "" + LODFunctionLibrary.CountFinalScore(player.GetScoreList);
        #region Add Score
        for ( int i = 0; i < player.GetScoreList.Count; i++ )
        {
            GameObject dyScore = Instantiate(dynastyScorePrefab, dynastyScoreParent);
            dyScore.GetComponent<TextMeshProUGUI>().text = "" + player.GetScoreList[i];
        }
        #endregion
        #region Show Collected Activities
        for ( int i = 0; i < player.GetCollectedCards.GetCollectedStoryCard.Count; i++ )
        {
            GameObject actText = Instantiate(collectedActPrefab, collectedActParent);
            actText.GetComponent<TextMeshProUGUI>().text = player.GetCollectedCards.GetCollectedStoryCard[i].cardName 
                + " | " + player.GetCollectedCards.GetCollectedStoryCard[i].cardScoreValue;
        }
        #endregion
        #region Show Character Per Dynasty
        state.AddCharaImage();
        for ( int i = 0; i < state.GetCharacter.Count; i++ )
        {
            GameObject chara = Instantiate(characterPrefab, characterParent);
            CharaListData data = chara.GetComponent<CharaListData>();

            data.dynastyText.text = "Dynasty : " + (i + 1);
            if ( state.GetCharacter.Count != 0 )
            {
                data.characterImage.sprite = state.GetCharacter[i];
            }
            else
            {
                data.characterImage.sprite = state.PlayerImageCharacter.sprite;
            }
        }
        #endregion
    }

    public void ShowDeadMenu()
    {
        Time.timeScale = 0;
        deadMenu.SetActive(true);
    }
}
