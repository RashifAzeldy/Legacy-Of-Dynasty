using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] bool hasLover;
    public bool PlayerHasLover { get { return hasLover; } set { hasLover = value; } }
    [SerializeField] Education educationStatus;
    public Education GetEducationStatus { get { return educationStatus; } set { educationStatus = value; } }
    [SerializeField] Career careerStatus;
    public Career GetCareerStatus { get { return careerStatus; } set { careerStatus = value; } }
    [SerializeField] PlayerState state;
    public PlayerState GetPlayerState { get { return state; } set { state = value; } }

}

public enum Education
{
    None
}
public enum Career
{
    None
}