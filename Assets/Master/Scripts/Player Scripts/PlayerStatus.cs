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

    public LoverStage Lover { get=> playerStatusData.LoverStage; }
    public JobData JobData { get=> playerStatusData.JobData; }
    public Age Age { get=> playerStatusData.PlayerAge; }
    public EducationStage Education { get=> playerStatusData.EducationStage; }
    public int Score { get=> playerStatusData.PlayerScore; }

    public bool haveChild;
}