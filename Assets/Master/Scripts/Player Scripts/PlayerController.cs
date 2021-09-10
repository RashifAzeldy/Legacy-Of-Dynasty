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

    Rigidbody2D rb;
    bool pause;
    public bool PlayerPause { get { return pause; } set { pause = value; } }

    bool canJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canJump = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !pause)
        {
            rb.velocity = new Vector2(0, jumpPower);
            StartCoroutine(ResetJump(jumpDelay));
        }

        rb.simulated = pause ? false : true;
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //UpdateBlock(other.gameObject);
        LODFunctionLibrary.UpdateScore(other.GetComponent<BlockController>().cardData, GetComponent<PlayerStatus>(), scoreText);
        other.gameObject.GetComponent<BlockController>().DestroyObject();
    }

    void UpdateBlock(GameObject collectedObj)
    {
        collectedObj.SetActive(false);
        if (collectedObj.GetComponent<BlockController>() != null)
        {
            BlockController otherDesc = collectedObj.GetComponent<BlockController>();
            LODFunctionLibrary.ShowStoryText(otherDesc.cardData.cardStory, crawlTextUI, crawlText);
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
