using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;

    private void Start()
    {
        gameOverMenu.SetActive(false);
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowCollectedCard()
    {

    }
}
