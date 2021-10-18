using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatusData
{

    public StatusDataAs statusDataAs;

    public int playerScore;

    public Age playerAge;

    public EducationStage educationStage;

    public LoverStage loverStage;

    public JobData jobData;

    public int PlayerScore { get => playerScore; set => playerScore = value; }
    public Age PlayerAge { get => playerAge; set => playerAge = value; }
    public EducationStage EducationStage { get => educationStage; set => educationStage = value; }
    public LoverStage LoverStage { get => loverStage; set => loverStage = value; }
    public JobData JobData { get => jobData; set => jobData = value; }

}

[System.Serializable]
public enum StatusDataAs 
{ 
    PlayerStatus, RadiantCardRequirement, CareerCardRequirement, EducationCardRequirement, LoverCardRequirement 
}