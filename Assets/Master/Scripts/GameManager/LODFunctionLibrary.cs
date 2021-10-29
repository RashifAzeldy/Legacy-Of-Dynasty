using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class LODFunctionLibrary
{
    public static void TeleportBlock(GameObject block, Vector3 destination)
    {
        block.transform.position = destination;
    }

    /// <summary>
    /// Return a bool value of a comparison between two PlayerStatusData
    /// </summary>
    /// <param name="playerStatus"> Player status that will be compared with the other data</param>
    /// <param name="CompareData"> The Data that will be the standard requirement of the player status data</param>
    /// <returns></returns>
    public static bool ComparePlayerStatusData(PlayerStatusData playerStatus, PlayerStatusData CompareData)
    {
        bool scoreComparison = playerStatus.PlayerScore >= CompareData.PlayerScore;
        bool ageComparison = playerStatus.PlayerAge == CompareData.PlayerAge;
        bool educationComparison = playerStatus.EducationStage == CompareData.EducationStage;
        bool loverComparison = playerStatus.LoverStage == CompareData.LoverStage;
        bool jobLevelComparison = playerStatus.JobData.jobLevel == CompareData.JobData.jobLevel;
        bool jobTypeComparison = playerStatus.JobData.jobType == CompareData.JobData.jobType;

        return scoreComparison && ageComparison && educationComparison && loverComparison && jobLevelComparison && jobTypeComparison;
    }

    public static void RandomizeYPos(GameObject block, BlockEffort blockEffort)
    {
        float result;

        switch (blockEffort)
        {
            case BlockEffort.High:
                result = Random.Range(1f, 5.5f);
                block.transform.position = new Vector3(block.transform.position.x, result, block.transform.position.z);
                break;
            case BlockEffort.Medium:
                result = Random.Range(-4f, 0f);
                block.transform.position = new Vector3(block.transform.position.x, result, block.transform.position.z);
                break;
            case BlockEffort.Low:
                result = Random.Range(-8.6f, -5f);
                block.transform.position = new Vector3(block.transform.position.x, result, block.transform.position.z);
                break;
            default:
                break;
        }
    }

    public static void SetBlockStoryCard(BlockController block, CardDataBase card)
    {
        block.cardData = card;
    }

    public static void FreezeYRigidbody(GameObject gameObj)
    {
        gameObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public static void ShowStoryText(string storyText, GameObject uiObject, TextMeshProUGUI textUI)
    {
        if ( !uiObject.activeSelf )
        { 
            uiObject.SetActive(true);
            uiObject.GetComponent<StoryCardTextCrawl>().MoveText = true;
        }
        else
        {
            uiObject.transform.position = uiObject.GetComponent<StoryCardTextCrawl>().GetDefaultPosition.position;
        }

        textUI.text = storyText;
    }

    public static void UpdateScore(CardDataBase card, PlayerStatus status, TextMeshProUGUI textObject)
    {
        status.playerStatusData.PlayerScore += card.cardScoreValue;
        textObject.text = "" + status.playerStatusData.PlayerScore;
    }

    public static int CountFinalScore(List<int> score)
    {
        int result = score.Sum();

        return result;
    }

    // X = Horizontal Min
    // Y = Horizontal Max
    // Z = Width
    public static Vector3 CheckCamera(Camera cam)
    {
        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.aspect * halfHeight;
        float horizontalMin = -halfWidth;
        float horizontalMax = halfWidth;

        return new Vector3(horizontalMin, horizontalMax, 0);
    }

    public static void ApplyEffect(GameObject player, CardDataBase card, GameObject gameManager)
    {
        PlayerStatusData status = player.GetComponent<PlayerStatus>().playerStatusData;
        PlayerController controller = player.GetComponent<PlayerController>();
        switch ( card.effect )
        {
            case CardEffect.ChangeStats:
            switch ( card.changedStats )
            {
                case Stats.Education:
                int eduIndex = (int) status.EducationStage;
                eduIndex += card.changeLevel;
                if ( eduIndex < 0 )
                {
                    eduIndex = 0;
                }
                status.EducationStage = (EducationStage) eduIndex;
                break;
                case Stats.Job:
                int jobIndex = (int)status.JobData.jobLevel;

                JobData dataResult = new JobData();
                dataResult.jobType = status.JobData.jobType;
                jobIndex += card.changeLevel;
                dataResult.jobLevel = (JobLevel) jobIndex;

                status.JobData = dataResult;
                break;
                case Stats.Lover:
                int loverIndex = (int) status.LoverStage;
                loverIndex += card.changeLevel;
                if(loverIndex < 0 )
                {
                    loverIndex = 0;
                }
                status.LoverStage = (LoverStage) loverIndex;
                break;
            }
            break;
            case CardEffect.CantJump:
            controller.CanPlayerJump = false;
            controller.StartJumpDelay(card.time);
            break;
            case CardEffect.Dead:
            if ( player.GetComponent<PlayerStatus>().haveChild )
            {
                gameManager.GetComponent<GameOverManager>().ShowDeadMenu();
                gameManager.GetComponent<GameOverManager>().SetCausingDeath.text = card.causingDeath;
            }
            else
            {
                gameManager.GetComponent<GameOverManager>().ShowGameOverMenu();
            }
            break;
        }
    }

    /*
    public static void ChangeStoryCard(storyCard, gameObject(SCObject))
    {
        gameObject.SCObject.GetStoryCard = storyCard;
    }
    */

    public static void GameOver(GameObject gameOverUI)
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0.1f;
    }
}