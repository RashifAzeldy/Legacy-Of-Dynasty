using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatusData
{
    [SerializeField] StatusDataAs statusDataAs;

    [SerializeField] int playerScore;

    [SerializeField] Age playerAge;

    [SerializeField] EducationData educationData;

    [SerializeField] LoverData loverData;

    [SerializeField] JobData jobData;

    public int PlayerScore { get => playerScore; set => playerScore = value; }
    public Age PlayerAge { get => playerAge; set => playerAge = value; }
    public EducationData EducationData { get => educationData; set => educationData = value; }
    public LoverData LoverData { get => loverData; set => loverData = value; }
    public JobData JobData { get => jobData; set => jobData = value; }
}

[System.Serializable]
public enum StatusDataAs
{
    PlayerStatus, RadiantCardRequirement, CareerCardRequirement, EducationCardRequirement, LoverCardRequirement
}