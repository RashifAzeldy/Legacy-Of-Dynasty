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

    public LoverData Lover { get=> playerStatusData.LoverData; }
    public JobData JobData { get=> playerStatusData.JobData; }
    public Age Age { get=> playerStatusData.PlayerAge; }
    public EducationData Education { get=> playerStatusData.EducationData; }
    public int Score { get=> playerStatusData.PlayerScore; }

    public bool haveChild;
}