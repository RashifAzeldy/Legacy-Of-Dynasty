using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin Data", menuName = "Skin")]
public class SkinData : ScriptableObject
{
    [SerializeField] string skinID;
    [SerializeField] Sprite skin;
    [SerializeField] bool isUnlocked;

    public string GetSkinID() { return skinID; }
    public Sprite GetSkin() { return skin; }
    public bool CheckSkinStatus() { return isUnlocked; }
}
