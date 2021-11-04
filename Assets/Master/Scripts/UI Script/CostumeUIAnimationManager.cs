using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeUIAnimationManager : MonoBehaviour
{
    [SerializeField] Animator anim;

    CostumeManager manager;

    private void Start()
    {
        manager = GetComponent<CostumeManager>();
    }

    public void SelectedHat()
    {
        anim.SetBool("Hat", true);
        manager.ChoosingHat = true;
    }
    public void UnselectMenu()
    {
        anim.SetBool("Hat", false);
        manager.ChoosingHat = false;
    }
}
