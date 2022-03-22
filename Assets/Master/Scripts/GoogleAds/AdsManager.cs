using System;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.Events;

public class AdsManager : MonoBehaviour
{

   public static AdsManager Instance { get; set; }

   [Header("Config: ")]
   [SerializeField] protected string m_interstitialAdID;
   [SerializeField] protected string m_bannerAdID;

   private void Awake()
   {
      if (!Instance)
      {
         Instance = this;
         DontDestroyOnLoad(Instance);
      }
      else
         Destroy(gameObject);

#if DEVELOPMENT_BUILD
      List<string> deviceIds = new List<string>();
      deviceIds.Add("81880ABE6520372262CC52D9F56F562B");
      RequestConfiguration requestConfiguration = new RequestConfiguration
            .Builder()
         .SetTestDeviceIds(deviceIds)
         .build();
      
      MobileAds.SetRequestConfiguration(requestConfiguration);
#endif
      MobileAds.Initialize(status => { });

   }

   public BannerAd CreateBannerAd(bool showOnCreated = true)
   {

      return new BannerAd(m_bannerAdID, showOnCreated);

   }

   public InterstitialAd CreateInterstitialAd()
   {
      return new InterstitialAd(m_interstitialAdID);
   }
   
   public class InterstitialAd
   {

      public GoogleMobileAds.Api.InterstitialAd AdInstance;

      public InterstitialAd(string adID)
      {
         RequestAd(adID);
      }

      /// <summary>
      /// Request an Ad for the AdInstance. (Already done when creating class Instance)
      /// </summary>
      /// <param name="adID">The interstitial Ad ID</param>
      public void RequestAd(string adID)
      {
         AdInstance = new GoogleMobileAds.Api.InterstitialAd(adID);
         
         AdInstance.OnAdFailedToLoad += OnAdFailedToLoad;
         AdInstance.OnAdClosed += OnAdClosed;
         AdInstance.OnAdFailedToShow += OnAdFailedToShow;

         AdRequest request = new AdRequest.Builder().Build();
         AdInstance.LoadAd(request);
      }

      /// <summary>
      /// Show the interstitial Ad
      /// </summary>
      public void ShowAd()
      {
         if (AdInstance.IsLoaded())
            AdInstance.Show();
      }

      #region Event Function
      
      private void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
      {
         
         LoadAdError loadAdError = args.LoadAdError;

         // Gets the domain from which the error came.
         string domain = loadAdError.GetDomain();

         // Gets the error code. See
         // https://developers.google.com/android/reference/com/google/android/gms/ads/AdRequest
         // and https://developers.google.com/admob/ios/api/reference/Enums/GADErrorCode
         // for a list of possible codes.
         int code = loadAdError.GetCode();

         // Gets an error message.
         // For example "Account not approved yet". See
         // https://support.google.com/admob/answer/9905175 for explanations of
         // common errors.
         string message = loadAdError.GetMessage();

         // Gets the cause of the error, if available.
         AdError underlyingError = loadAdError.GetCause();

         // All of this information is available via the error's toString() method.
         Debug.Log("Load error string: " + loadAdError.ToString());

         // Get response information, which may include results of mediation requests.
         ResponseInfo responseInfo = loadAdError.GetResponseInfo();
         Debug.Log("Response info: " + responseInfo.ToString());
         
      }

      private void OnAdClosed(object sender, EventArgs args)
      {

         Debug.Log("Ad is closed");
         AdInstance.Destroy();

      }

      private void OnAdFailedToShow(object sender, EventArgs args)
      {

         Debug.LogError(sender + " ad failed to show, \nmessage: " + args);

      }
      
      #endregion
   }

   public class BannerAd
   {

      public BannerView AdInstance { get; private set; }

      private bool _isLoaded;
      
      public BannerAd(string adID, bool showOnCreated = true)
      {
         RequestAd(adID);
         if (!showOnCreated)
            ToggleAd(false);
      }

      ~BannerAd()
      {
         Debug.Log("Banner Ad Destroyed");
         AdInstance.Destroy();
      }
      
      public void ToggleAd(bool active)
      {
         if (active)
            AdInstance.Show();
         else
            AdInstance.Hide();
      }
      
      /// <summary>
      /// Request a banner Ad. (Already done automatically when banner ad constructor called)
      /// </summary>
      /// <param name="adID">The banner ad ID</param>
      /// <param name="bannerAd">The variable reference for the out Banner Ad</param>
      /// <returns>Whether the banner ad is loaded or not</returns>
      public void RequestAd(string adID)
      {
         AdInstance = new BannerView(adID, AdSize.SmartBanner, AdPosition.Bottom);

         AdInstance.OnAdLoaded += OnAdLoaded;
         AdInstance.OnAdFailedToLoad += OnAdFailedToLoad;
         AdInstance.OnAdClosed += OnAdClosed;

         AdRequest request = new AdRequest.Builder().Build();
         AdInstance.LoadAd(request);
         
      }
      
      private void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
      {
         
         LoadAdError loadAdError = args.LoadAdError;

         // Gets the domain from which the error came.
         string domain = loadAdError.GetDomain();

         // Gets the error code. See
         // https://developers.google.com/android/reference/com/google/android/gms/ads/AdRequest
         // and https://developers.google.com/admob/ios/api/reference/Enums/GADErrorCode
         // for a list of possible codes.
         int code = loadAdError.GetCode();

         // Gets an error message.
         // For example "Account not approved yet". See
         // https://support.google.com/admob/answer/9905175 for explanations of
         // common errors.
         string message = loadAdError.GetMessage();

         // Gets the cause of the error, if available.
         AdError underlyingError = loadAdError.GetCause();

         // All of this information is available via the error's toString() method.
         Debug.Log("Load error string: " + loadAdError.ToString());

         // Get response information, which may include results of mediation requests.
         ResponseInfo responseInfo = loadAdError.GetResponseInfo();
         Debug.Log("Response info: " + responseInfo.ToString());
         
      }

      private void OnAdClosed(object sender, EventArgs args)
      {
         Debug.Log(sender + " Is closed");
         
      }

      private void OnAdLoaded(object sender, EventArgs args)
      {
         Debug.Log(sender + " Is loaded");
         _isLoaded = true;
      }
      
   }

}
