using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] List<AgeState> ageList = new List<AgeState>();
    [SerializeField] SpriteRenderer player;
    bool switchState;
    int ageIndex = 0;
    Age currentState;
    public Age GetPlayerCurrentState { get { return currentState; } }

    private void Start()
    {
        ageIndex = -1;
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
            ageIndex = 0;
        }
        // Change To Game Object When it's Converted to 3D Game!
        player.sprite = ageList[ageIndex].GetAgeObject;
        currentState = ageList[ageIndex].GetState;

        yield return new WaitForSeconds(time);
        switchState = true;
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