using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] List<AgeState> ageList = new List<AgeState>();
    [SerializeField] SpriteRenderer player;
    [SerializeField] PlayerStatus status;
    [SerializeField] PlayerController playerController;
    bool switchState;
    int ageIndex = 0;
    Age currentState;
    List<Sprite> characterImage = new List<Sprite>();

    public Age GetPlayerCurrentState { get { return currentState; } }
    public List<Sprite> GetCharacter { get { return characterImage; } }
    public SpriteRenderer PlayerImageCharacter { get { return player; } }

    private void Start()
    {
        switchState = true;
    }

    void Update()
    {
        if (switchState)
        {
            StartCoroutine(SwitchAgeState(60));
            switchState = false;
        }
    }

    IEnumerator SwitchAgeState(float time)
    {
        ageIndex += 1;
        if (ageIndex >= ageList.Count)
        {
            // Change Dynasty ~
            playerController.AddCurrentScore();
            AddCharaImage();
            ageIndex = 1;
        }
        player.sprite = ageList[ageIndex].GetAgeObject;
        currentState = ageList[ageIndex].GetState;
        status.playerStatusData.PlayerAge = currentState;

        switch (currentState)
        {
            case Age.Child:
                status.playerStatusData.EducationStage = EducationStage.GradeSchool;
                break;
            case Age.Teen:
                status.playerStatusData.EducationStage = EducationStage.MiddleSchool;
                yield return new WaitForSeconds(time / 2);
                status.playerStatusData.EducationStage = EducationStage.HighSchool;
                break;
            case Age.Adult:
                status.playerStatusData.EducationStage = EducationStage.University;
                yield return new WaitForSeconds(time / 3);
                status.playerStatusData.EducationStage = EducationStage.None;
                JobData playerJob = new JobData();
                playerJob.jobType = (JobType)Random.Range(1, 4);
                playerJob.jobLevel = (JobLevel)1;
                status.playerStatusData.JobData = playerJob;
                yield return new WaitForSeconds(time / 3);
                playerJob.jobLevel = (JobLevel)2;
                status.playerStatusData.JobData = playerJob;
                yield return new WaitForSeconds(time / 3);
                playerJob.jobLevel = (JobLevel)3;
                status.playerStatusData.JobData = playerJob;
                break;
            case Age.Elder:
                JobData elder = new JobData();
                status.playerStatusData.JobData = elder;
                break;
        }

        yield return new WaitForSeconds(time);
        switchState = true;
    }

    public void AddCharaImage()
    {
        characterImage.Add(player.sprite);
    }
}

[System.Serializable]
public class AgeState
{
    [SerializeField] Age ageState;
    public Age GetState { get { return ageState; } }

    // Change To Game Object When it's Converted to 3D Game!
    [SerializeField] Sprite playerObject;
    public Sprite GetAgeObject { get { return playerObject; } set { playerObject = value; } }
}

public enum Age
{
    None,
    Child,
    Teen,
    Adult,
    Elder
}