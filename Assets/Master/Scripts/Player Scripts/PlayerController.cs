using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpPower = 3f;
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
            rb.AddForce(new Vector2(0, jumpPower * 2), ForceMode2D.Impulse);
            StartCoroutine(ResetJump(1f));
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
        if ( other.gameObject.GetComponent<BlockController>() )
        {
            LODFunctionLibrary.UpdateScore(other.GetComponent<BlockController>().cardData, GetComponent<PlayerStatus>(), scoreText);
            LODFunctionLibrary.ShowStoryText(other.GetComponent<BlockController>().cardData.cardStory, crawlTextUI, crawlText);
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
