using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCardTextCrawl : MonoBehaviour
{
    [SerializeField] float textSpeed = 5f;

    [SerializeField] Transform defaultTextPos;
    [SerializeField] Transform cam;
    [SerializeField] BlockDestroyer destroyer;
    [SerializeField] Vector2 offset;

    bool move;

    public bool MoveText { get => move; set => move = value; }
    public Transform GetDefaultPosition { get => defaultTextPos; }

    private void Start()
    {
        defaultTextPos.position = new Vector2(cam.position.x, defaultTextPos.position.y) + offset;
    }

    private void Update()
    {
        if(move)
            transform.Translate(-Vector3.right * textSpeed * Time.deltaTime);
        else
        {
            transform.position = defaultTextPos.position;
        }
    }

    public void ResetText()
    {
        gameObject.SetActive(false);
        move = false;
        transform.position = new Vector3(defaultTextPos.position.x, defaultTextPos.position.y, 0);
    }
}
