using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EducationStage
{
    None, GradeSchool, MiddleSchool, HighSchool, University
}

[CreateAssetMenu(menuName = "Card/CardData/EducationActivityData", fileName = "EducationActivityCard")]
[System.Serializable]
public class EducationCardData : CardDataBase
{
   
}

[System.Serializable]
public struct EducationData
{
    /// <summary>
    /// What kind of job type is it ?
    /// </summary>
    public EducationStage eduStage;

    /// <summary>
    /// What is the job level ?
    /// </summary>
    public int eduScore;
}