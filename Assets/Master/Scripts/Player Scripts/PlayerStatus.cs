using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] LoverStage currentLoverStage;
    public LoverStage GetLoverStage { get { return currentLoverStage; } set { currentLoverStage = value; } }
    [SerializeField] Education educationStatus;
    public Education GetEducationStatus { get { return educationStatus; } set { educationStatus = value; } }
    [SerializeField] Career careerStatus;
    public Career GetCareerStatus { get { return careerStatus; } set { careerStatus = value; } }
    [SerializeField] PlayerState state;
    public PlayerState GetPlayerState { get { return state; } set { state = value; } }

}

public enum Education
{
    None,
    Gradeschool,
    Middleschool,
    Highschool,
    University
}
public enum Career
{
    None,
    Jobless,
    Doctor,
    SupermarketCashier
}
public enum LoverStage 
{
    Single,
    GirlfriendOrBoyfriend,
    Married,
    Soulmate
}