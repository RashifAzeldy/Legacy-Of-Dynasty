using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpPower = 5f;
    [SerializeField] GameObject crawlTextUI;
    [SerializeField] TextMeshProUGUI crawlText;

    Rigidbody2D rb;
    GameObject collectedSCObject;
    float playerScore;
    public float GetPlayerScore
    {
        get { return playerScore; }
        set { playerScore = value; }
    }
    bool canUpdateScore;
    public bool PlayerUpdateScore { get { return canUpdateScore; } set { canUpdateScore = value; } }

    bool pause;
    public bool PlayerPause { get { return pause; } set { pause = value; } }

    bool canJump;

    int indexVar;
    public int GetIndexVar { get { return indexVar; } set { indexVar = value; } }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canUpdateScore = true;
        canJump = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !pause)
        {
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            StartCoroutine(ResetJump(0.3f));
        }

        if (pause)
        {
            rb.simulated = false;
        }
        else
        {
            rb.simulated = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //UpdateBlock(other.gameObject);
        other.gameObject.GetComponent<BlockController>().DestroyObject();
    }

    void UpdateBlock(GameObject collectedObj)
    {
        collectedObj.SetActive(false);
        if (collectedObj.GetComponent<BlockController>() != null)
        {
            BlockController otherDesc = collectedObj.GetComponent<BlockController>();
            LODFunctionLibrary.ShowStoryText(otherDesc.cardData.card.cardStory, crawlTextUI, crawlText);
            crawlTextUI.GetComponent<StoryCardTextCrawl>().MoveText = true;
        }
    }

    IEnumerator ResetJump(float time)
    {
        canJump = false;
        yield return new WaitForSeconds(time);
        canJump = true;
    }
}
