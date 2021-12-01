using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [Header("Config : ")]
    [SerializeField] private string _mainMenuSceneName;

    
    public void ReturnToMainMenu()
    {

        GameManager.Instance.SaveGameData();
        ChangeScene(_mainMenuSceneName);
    }

    private void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

}
