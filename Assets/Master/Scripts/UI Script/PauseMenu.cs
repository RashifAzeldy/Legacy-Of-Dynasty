using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [Header("Config : ")]
    [SerializeField] private string _mainMenuSceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMainMenu()
    {
        ChangeScene("");
    }

    private void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

}
