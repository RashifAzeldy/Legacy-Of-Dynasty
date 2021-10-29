using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] Animator anim;

    public void SelectedAccessories()
    {
        anim.SetBool("Accessories", true);
    }
    public void UnselectedAccessories()
    {
        anim.SetBool("Accessories", false);
    }
}
