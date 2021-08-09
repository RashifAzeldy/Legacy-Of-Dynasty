using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseButton;
    [SerializeField] TextMeshProUGUI countdownText;

    [SerializeField] GManager manager;
    [SerializeField] PlayerController controller;
    [SerializeField] BackgroundManager backgroundManager;
    [SerializeField] GroundController ground;

    [SerializeField] int countdown;
    public void PauseGame()
    {
        // Stop Game Time
        manager.isPaused = true;
        controller.PlayerPause = true;
        backgroundManager.BackgroundPause = true;
        ground.GroundPause = true;

        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        // Show Pause Menu
    }

    public void UnpauseGame()
    {
        // Hide Pause Menu
        // Start CountDown
        pauseMenu.SetActive(false);
        StartCoroutine(UnpauseCountdown(countdown));
        // Play Game Time Normally
    }

    IEnumerator UnpauseCountdown(int time)
    {
        // Show Text Countdown
        // Show Text Value Same Like int time

        countdownText.gameObject.SetActive(true);
        countdownText.text = " ";
        for (int i = countdown; i >= 0; i--)
        {
            yield return new WaitForSeconds(1);
            // Change Text Number According int text - 1
            if (i == 0)
            {
                controller.PlayerPause = false;
                manager.isPaused = false;
                backgroundManager.BackgroundPause = false;
                ground.GroundPause = false;

                pauseButton.SetActive(true);
                countdownText.gameObject.SetActive(false);
            }
            else
            {
                countdownText.text = "" + time;
            }
            time -= 1;
        }
    }
}