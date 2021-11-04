using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Costume", menuName = "Costume")]
public class Costume : ScriptableObject
{
    [SerializeField] bool isUnlocked;
    [SerializeField] Sprite costume;

    public bool IsCostumeUnlocked { get { return isUnlocked; } set { isUnlocked = value; } }
    public Sprite CostumeSprite { get { return costume; } set { costume = value; } }
}
