using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpPower = 3f;
    [SerializeField] float jumpDelay = 1.25f;
    [SerializeField] GameObject crawlTextUI;
    [SerializeField] TextMeshProUGUI crawlText;
    [SerializeField] TextMeshProUGUI scoreText;
    public TextMeshProUGUI GetScoreText { get { return scoreText; } set { scoreText = value; } }

    [SerializeField] StoryCardCollector collectedCards;
    [SerializeField] GameOverManager gOver;

    Rigidbody2D rb;

    List<int> playerScore = new List<int>();
    public List<int> GetScoreList { get { return playerScore; } set { playerScore = value; } }
    bool pause;
    public bool PlayerPause { get { return pause; } set { pause = value; } }

    public StoryCardCollector GetCollectedCards { get { return collectedCards; } }

    [SerializeField] bool canJump;
    public bool CanPlayerJump { get { return canJump; } set { canJump = value; } }

    bool delayIsRunning;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canJump = true;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && canJump && !pause)
        {
            rb.velocity = new Vector2(0, jumpPower);
            canJump = false;
            StartJumpDelay(jumpDelay);
        }

        rb.simulated = pause ? false : true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BlockController blockObject;

        if (blockObject = other.gameObject.GetComponent<BlockController>())
        {

            if (blockObject.cardData.random)
            {
                CardDataBase randomizedCard = blockObject.cardData.randomCards[Random.Range(0,
                blockObject.cardData.randomCards.Count)];

                LODFunctionLibrary.UpdateScore(randomizedCard, GetComponent<PlayerStatus>(), scoreText);
                LODFunctionLibrary.ShowStoryText(randomizedCard.cardStory, crawlTextUI, crawlText);
                collectedCards.GetCollectedStoryCard.Add(randomizedCard);
                LODFunctionLibrary.ApplyEffect(gameObject, randomizedCard, gOver.gameObject);
            }
            else
            {
                LODFunctionLibrary.UpdateScore(other.GetComponent<BlockController>().cardData, GetComponent<PlayerStatus>(), scoreText);
                LODFunctionLibrary.ShowStoryText(other.GetComponent<BlockController>().cardData.cardStory, crawlTextUI, crawlText);
                collectedCards.GetCollectedStoryCard.Add(blockObject.cardData);
                LODFunctionLibrary.ApplyEffect(gameObject, blockObject.cardData, gOver.gameObject);
            }

            string objectTagID;

            switch (blockObject.cardData.cardValue)
            {
                case CardValue.Positive:
                    objectTagID = "BlockPositive";
                    break;
                case CardValue.Negative:
                    objectTagID = "BlockNegative";
                    break;
                case CardValue.Mystery:
                    objectTagID = "BlockMystery";
                    break;
                case CardValue.Neutral:
                    objectTagID = "BlockNeutral";
                    break;
                default:
                    objectTagID = "BlockNeutral";
                    break;
            }

            ObjectPoolerManager.Instance.DestroyPoolObjectFromScene(objectTagID, other.gameObject);

        }
    }

    public void StartJumpDelay(float time)
    {
        if (delayIsRunning)
        {
            StopAllCoroutines();
        }

        StartCoroutine(ResetJump(time));
    }

    public void AddCurrentScore()
    {
        playerScore.Add(PlayerStatus.Instance.playerStatusData.PlayerScore);
        PlayerStatus.Instance.playerStatusData.PlayerScore = 0;
    }

    IEnumerator ResetJump(float time)
    {
        delayIsRunning = true;
        yield return new WaitForSeconds(time);
        canJump = true;
        delayIsRunning = false;
    }
}
