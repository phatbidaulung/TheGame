using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;
    //delegate   ()
    public delegate void RewardedAdResult(bool isSuccess, int rewarded);

    //event  
    public static event RewardedAdResult AdResult;

    public enum AD_NETWORK { Unity, Admob }

    //[Header("REWARDED VIDEO AD")]
    [HideInInspector] public AD_NETWORK rewardedUnit;
    [HideInInspector] public int getRewarded = 5;
    [HideInInspector] public float timePerWatch = 90;
    float lastTimeWatch = -999;

    [Header("SHOW AD VICTORY/GAMEOVER")]
    public AD_NETWORK adGameOverUnit;
    public int showAdGameOverCounter = 2;
    int counter_gameOver = 0;
    public int showAdVictoryCounter = 1;
    int counter_victory = 0;

    public bool resetCounterOnAdShow = true;        //whenever show gameover or victory, should reset the counter of both after show ad?

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (AdsManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ShowAdmobBanner(bool show)
    {
        Debug.Log("RemoveAds.Instance.RemovedAds() : " + IngameData.Instance.IsShowAds);
        if(!IngameData.Instance.IsShowAds)
            AdmobController.Instance.ShowBanner(show);
    }

    #region NORMAL AD

    public void ShowNormalAd(EStatusGame state)
    {
        Debug.Log("SHOW NORMAL AD " + state);
        if (!IngameData.Instance.IsShowAds)
        {
            if (state == EStatusGame.GameOver)
                StartCoroutine(ShowNormalAdCo(state, 0.8f));
            else
                StartCoroutine(ShowNormalAdCo(state, 0));
        }
    }

    IEnumerator ShowNormalAdCo(EStatusGame state, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (state == GameManager.Instance.StatusGameIs())
        {
            counter_gameOver++;
            if (counter_gameOver >= showAdGameOverCounter)
            {
                if (adGameOverUnit == AD_NETWORK.Admob)
                {
                    if (AdmobController.Instance.ForceShowInterstitialAd())
                    {
                        counter_gameOver = 0;
                        if (resetCounterOnAdShow)
                            ResetCounter();
                    }
                }
            }
        }
        else if (state == GameManager.Instance.StatusGameIs())
        {
            counter_victory++;
            if (counter_victory >= showAdVictoryCounter)
            {
                if (adGameOverUnit == AD_NETWORK.Admob)
                {
                    if (AdmobController.Instance.ForceShowInterstitialAd())
                    {
                        counter_victory = 0;
                        if (resetCounterOnAdShow)
                            ResetCounter();
                    }
                }
            }
        }
        //}
    }

    public void ResetCounter()
    {
        counter_gameOver = 0;
        //counter_gameFinish = 0;
    }

    #endregion

    #region REWARDED VIDEO AD

    public bool isRewardedAdReady()
    {
        // if ((rewardedUnit == AD_NETWORK.Unity) && UnityAds.Instance.isRewardedAdReady())
        //     return true;

        if ((rewardedUnit == AD_NETWORK.Admob) && AdmobController.Instance.isRewardedVideoAdReady())
            return true;

        return false;

    }

    public float TimeWaitingNextWatch()
    {
        return timePerWatch - (Time.realtimeSinceStartup - lastTimeWatch);
    }

    private void AdmobController_AdResult(bool isWatched)
    {
        AdmobController.AdResult -= AdmobController_AdResult;
        AdResult(true, getRewarded);
    }
    #endregion
}
