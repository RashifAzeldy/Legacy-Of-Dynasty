using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] List<AgeState> ageList = new List<AgeState>();
    [SerializeField] SpriteRenderer player;
    [SerializeField] PlayerStatus status;
    bool switchState;
    bool dead;
    int ageIndex = 0;
    Age currentState;
    List<Sprite> characterImage = new List<Sprite>();

    public Age GetPlayerCurrentState { get { return currentState; } }
    public List<Sprite> GetCharacter { get { return characterImage; } }
    public SpriteRenderer PlayerImageCharacter { get { return player; } }

    private void Start()
    {
        ageIndex = -1;
        switchState = true;
    }

    void Update()
    {
        dead = status.IsPlayerDead();
        if (switchState)
        {
            StartCoroutine(SwitchAgeState(60));
            if ( currentState == Age.Adult )
            {
                AddCharaImage();
            }
            switchState = false;
        }
        if ( dead )
        {
            dead = false;
            AddCharaImage();
        }
    }

    IEnumerator SwitchAgeState(float time)
    {
        ageIndex += 1;
        if (ageIndex >= ageList.Count)
        {
            ageIndex = 0;
        }
        // Change To Game Object When it's Converted to 3D Game!
        player.sprite = ageList[ageIndex].GetAgeObject;
        currentState = ageList[ageIndex].GetState;
        status.playerStatusData.PlayerAge = currentState;

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
    Child,
    Teen,
    Adult,
    Elder
}