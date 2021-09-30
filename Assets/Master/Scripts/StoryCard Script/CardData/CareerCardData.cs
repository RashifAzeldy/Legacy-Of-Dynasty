using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JobLevel { None, Entry, Intermediate, Advanced, Legendary }
public enum JobType { None, Unemployed, Doctor, Salaryman, SupermarketCashier }

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
    public JobLevel jobLevel;
}