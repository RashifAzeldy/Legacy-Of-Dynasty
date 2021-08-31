using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] LoverStage loverStatus;
    public LoverStage GetLoverStatus { get { return loverStatus; } set { loverStatus = value; } }
    [SerializeField] EducationStage educationStatus;
    public EducationStage GetEducationStatus { get { return educationStatus; } set { educationStatus = value; } }
    [SerializeField] JobData careerStatus;
    public JobData GetCareerStatus { get { return careerStatus; } set { careerStatus = value; } }
    [SerializeField] PlayerState state;
    public PlayerState GetPlayerState { get { return state; } set { state = value; } }

}