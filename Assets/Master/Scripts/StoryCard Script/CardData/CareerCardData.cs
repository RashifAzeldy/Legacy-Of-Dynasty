using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JobLevel { Entry, Intermediate, Advanced, Legendary }
public enum JobType { Doctor, Salaryman, SupermarketCashier }

public class CareerCardData : CardDataBase
{

}

[System.Serializable]
public struct JobData
{
    /// <summary>
    /// What kind of job type is it ?
    /// </summary>
    public JobType jobType;

    /// <summary>
    /// What is the job level ?
    /// </summary>
    JobLevel jobLevel;
}