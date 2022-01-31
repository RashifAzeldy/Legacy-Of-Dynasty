using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour
{

   public static AdsManager Instance { get; set; }

   [SerializeField] protected string interstitialAdID;
   
   
   private void Awake()
   {

      if (!Instance)
         Instance = this;
      else
         Destroy(gameObject);
         
      MobileAds.Initialize(status => { });

      InterstitialAdManager.AdID = interstitialAdID;
      InterstitialAdManager.RequestAd();

   }

   public void ShowInterstitialAd()
   {
      InterstitialAdManager.ShowAd();
   }


   public static class InterstitialAdManager
   {

      public static string AdID;

      public static InterstitialAd InterstitialAd
      {
         get => _interstitialAd;
      }

      private static InterstitialAd _interstitialAd;
   
      public static void ShowAd()
      {
         if (_interstitialAd.IsLoaded())
            _interstitialAd.Show();
      
      }

      public static void RequestAd()
      {
         _interstitialAd = new InterstitialAd(AdID);

         _interstitialAd.OnAdFailedToLoad -= OnAdFailedToLoad;
         _interstitialAd.OnAdClosed -= OnAdClosed;
         _interstitialAd.OnAdFailedToShow -= OnAdFailedToShow;

         _interstitialAd.OnAdFailedToLoad += OnAdFailedToLoad;
         _interstitialAd.OnAdClosed += OnAdClosed;
         _interstitialAd.OnAdFailedToShow += OnAdFailedToShow;
         
         AdRequest cacheRequest = new AdRequest.Builder().Build();
         _interstitialAd.LoadAd(cacheRequest);

      }

      private static void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
      {
         
         Debug.LogError("Ad Failed to load !!!");
         
         _interstitialAd.Destroy();
         RequestAd();
      }

      private static void OnAdClosed(object sender, EventArgs args)
      {
         Debug.Log("Ad is closed !!!");
         _interstitialAd.Destroy();
         RequestAd();
      }

      private static void OnAdFailedToShow(object sender, EventArgs args)
      {
         Debug.LogError("Ad Failed to show !!!");
         
         ShowAd();
      }
      

   }
   
}
