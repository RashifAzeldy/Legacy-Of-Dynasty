using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [Header("Config : ")]
    [SerializeField] private string _mainMenuSceneName;

    private AdsManager.InterstitialAd interstitialAd;

    private void Start()
    {
        interstitialAd = AdsManager.Instance.CreateInterstitialAd();
    }


    public void ReturnToMainMenu()
    {

        GameManager.Instance.SaveGameData();

        if (interstitialAd.AdInstance.IsLoaded())
        {
            interstitialAd.ShowAd();
            
            interstitialAd.AdInstance.OnAdClosed += (obj, args) =>
            {
                ChangeScene(_mainMenuSceneName);
            };           
            
            interstitialAd.AdInstance.OnAdFailedToShow += (obj, args) =>
            {
                ChangeScene(_mainMenuSceneName);
            };            
        }
        else
            ChangeScene(_mainMenuSceneName);
        
    }

    private void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

}
