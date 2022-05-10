using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LoverStage
{
    None, Single, GirlfriendOrBoyfriend, Married, Soulmate
}

[CreateAssetMenu(menuName = "Card/CardData/LoverActivityData", fileName = "LoversActivityCard")]
[System.Serializable]
public class LoverCardData : CardDataBase
{
    
}

[System.Serializable]
public struct LoverData
{
    /// <summary>
    /// What kind of job type is it ?
    /// </summary>
    public LoverStage loverStage;

    /// <summary>
    /// What is the job level ?
    /// </summary>
    public int loverScore;
}
