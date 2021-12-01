using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Component:  ")]
    [SerializeField] Button startButton;
    [SerializeField] Button achievementButton;
    [SerializeField] GameObject achievementBack;
    [SerializeField] Button achievementBackButton;
    [SerializeField] Button customCharButton;
    [SerializeField] Button quitButton;
    [SerializeField] ConfirmationWidget confirmWidget;
    [Space]
    [Header("Misc: ")]
    [SerializeField] string nextSceneName;
    [SerializeField] public GameObject achievementWidget;

    public GameObject SetBackButton { get { return achievementBack; } set { achievementBack = value; } }

    private void Start()
    {

        if (achievementWidget)
            achievementWidget.SetActive(false);

        confirmWidget.DeactivateWidget();

        startButton.onClick.AddListener(() =>
        { SceneManager.LoadScene(nextSceneName); });

        achievementButton.onClick.AddListener(() =>
        { achievementWidget.SetActive(true); });

        achievementBackButton.onClick.AddListener(() =>
        { achievementWidget.SetActive(false); });

        customCharButton.onClick.AddListener(() => { SceneManager.LoadScene("SkinSelectionScene"); });

        quitButton.onClick.AddListener(() =>
        {
            confirmWidget.ActivateWidget(
                () => 
                { 
                    Application.Quit();
                    GameManager.Instance.SaveGameData();
                },
                () =>
                {
                    confirmWidget.DeactivateWidget();
                });

        });

        achievementBack.transform.SetAsLastSibling();
    }

    private void Update()
    {
    }

}
