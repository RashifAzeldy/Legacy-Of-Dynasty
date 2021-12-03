using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    #region Singleton

    private static PlayerStatus _instance;
    private PlayerStatus()
    {
        _instance = this;
    }

    public static PlayerStatus Instance { get => _instance; }


    #endregion
    public PlayerStatusData playerStatusData;

    LoverStage GetLover() { return playerStatusData.LoverStage; }
    JobData GetJobData() { return playerStatusData.JobData; }
    Age GetAge() { return playerStatusData.PlayerAge; }
    EducationStage GetEducation() { return playerStatusData.EducationStage; }
    int GetScore() { return playerStatusData.PlayerScore; }

    public bool haveChild;
}