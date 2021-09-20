using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseButton;
    [SerializeField] TextMeshProUGUI countdownText;

    [SerializeField] int countdown;

    public void PauseGame()
    {
        // Show Pause Menu
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        // Stop Game Time
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        // Hide Pause Menu
        pauseMenu.SetActive(false);
        // Start CountDown
        StartCoroutine(UnpauseCountdown(countdown));
    }

    IEnumerator UnpauseCountdown(int time)
    {
        // Show Text Countdown
        countdownText.gameObject.SetActive(true);
        countdownText.text = " ";
        // Change Text Number According int text - 1
        for (int i = countdown; i >= 0; i--)
        {
            yield return new WaitForSecondsRealtime(1);
            if (i == 0)
            {
                // Play Game Time Normally
                pauseButton.SetActive(true);
                countdownText.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                // Show Text Value Same Like int time
                countdownText.text = "" + time;
            }
            time -= 1;
        }
    }
}