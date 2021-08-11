using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpPower = 5f;

    Rigidbody2D rb;
    SCObject collectedStoryCard;
    public SCObject GetCollectedStoryObject
    {
        get { return collectedStoryCard; }
        set { collectedStoryCard = value; }
    }
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
        collectedSCObject = other.gameObject;
        if (collectedSCObject != null)
            UpdateBlock(collectedSCObject);
    }

    void UpdateBlock(GameObject collectedObj)
    {
        collectedObj.SetActive(false);
        if (collectedObj.GetComponent<SCObject>() != null)
        {
            SCObject otherDesc = collectedObj.GetComponent<SCObject>();
            collectedStoryCard = otherDesc;
        }
    }

    IEnumerator ResetJump(float time)
    {
        canJump = false;
        yield return new WaitForSeconds(time);
        canJump = true;
    }
}
