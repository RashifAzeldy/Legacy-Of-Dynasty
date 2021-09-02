using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    public PlayerStatusData playerStatusData;

    LoverStage GetLover() { return playerStatusData.LoverStage; }
    JobData GetJobData() { return playerStatusData.JobData; }
    Age GetAge() { return playerStatusData.PlayerAge; }
    EducationStage GetEducation() { return playerStatusData.EducationStage; }
    int GetScore() { return playerStatusData.PlayerScore; }

}

[System.Serializable]
public class PlayerStatusData
{
    [SerializeField] private int playerScore;

    [SerializeField] private Age playerAge;

    [SerializeField] private EducationStage educationStage;

    [SerializeField] private LoverStage loverStage;

    [SerializeField] private JobData jobData;

    public int PlayerScore { get => playerScore; set => playerScore = value; }
    public Age PlayerAge { get => playerAge; set => playerAge = value; }
    public EducationStage EducationStage { get => educationStage; set => educationStage = value; }
    public LoverStage LoverStage { get => loverStage; set => loverStage = value; }
    public JobData JobData { get => jobData; set => jobData = value; }

}