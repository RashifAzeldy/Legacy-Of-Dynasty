﻿using System.Collections;
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

    [SerializeField] StoryCardCollector collectedCards;
    [SerializeField] GameOverManager gOver;

    Rigidbody2D rb;

    List<int> playerScore = new List<int>();
    public List<int> GetScoreList { get { return playerScore; } set { playerScore = value; } }
    bool pause;
    public bool PlayerPause { get { return pause; } set { pause = value; } }
    bool dead;
    public bool PlayerIsDead { get { return dead; } set { dead = value; } }

    public StoryCardCollector GetCollectedCards { get { return collectedCards; } }

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

        // Only For Testing in Unity Editor !
        // Game Over Test

        if ( Input.GetKeyDown(KeyCode.G) )
        {
            // Game Over
            gOver.ShowGameOverMenu();
            // Show Collected StoryCard
        }

        if ( Input.GetKeyDown(KeyCode.D) )
        {
            gOver.ShowDeadMenu();
        }

        rb.simulated = pause ? false : true;

        //if ( dead )
        //{
        //    gOver.ShowGameOverMenu();
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //UpdateBlock(other.gameObject);
        if ( other.gameObject.GetComponent<BlockController>() )
        {
            LODFunctionLibrary.UpdateScore(other.GetComponent<BlockController>().cardData, GetComponent<PlayerStatus>(), scoreText);
            LODFunctionLibrary.ShowStoryText(other.GetComponent<BlockController>().cardData.cardStory, crawlTextUI, crawlText);
            if( !collectedCards.GetCollectedStoryCard.Contains(other.gameObject.GetComponent<BlockController>().cardData) )
            {
                collectedCards.GetCollectedStoryCard.Add(other.gameObject.GetComponent<BlockController>().cardData);
            }
            other.gameObject.GetComponent<BlockController>().DestroyObject();
        }
    }

    void UpdateBlock(GameObject collectedObj)
    {
        collectedObj.SetActive(false);
        if (collectedObj.GetComponent<BlockController>() != null)
        {
            BlockController otherDesc = collectedObj.GetComponent<BlockController>();
            LODFunctionLibrary.ShowStoryText(otherDesc.cardData.cardStory, crawlTextUI, crawlText);
        }
    }

    IEnumerator ResetJump(float time)
    {
        canJump = false;
        yield return new WaitForSeconds(time);
        canJump = true;
    }
}
