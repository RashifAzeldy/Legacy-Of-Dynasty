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
    [SerializeField] Button customCharButton;
    [SerializeField] Button quitButton;
    [SerializeField] ConfirmationWidget confirmWidget;
    [Space]
    [Header("Misc: ")]
    [SerializeField] string nextSceneName;

    private void Awake()
    {
        startButton.onClick.AddListener(() => 
        {

            SceneManager.LoadScene(nextSceneName);
        
        });

        quitButton.onClick.AddListener(() => 
        {

        confirmWidget.ActivateWidget(
            () => { Application.Quit(); }, 
            ()=> { confirmWidget.DeactivateWidget(); 
            });

        });

        confirmWidget.DeactivateWidget();

    }

    private void Update()
    {
    }

}
