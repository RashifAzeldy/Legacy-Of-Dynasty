using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinDisplay : MonoBehaviour
{
    [SerializeField] List<SkinData> skin = new List<SkinData>();
    [SerializeField] SpriteRenderer display;

    [SerializeField] Button leftArrow;
    [SerializeField] Button rightArrow;

    static int currentIndex = 0;

    void Start()
    {
        SetDisplay(currentIndex);
    }

    public void SetDisplay(int skinIndex)
    {
        display.sprite = skin[skinIndex].GetSkin();
    }

    public void ChangeDisplayedSkin ( bool right )
    {
        int _tempIndex = 0;
        switch ( right )
        {
            case false:
            _tempIndex = currentIndex - 1;
            break;
            case true:
            _tempIndex = currentIndex + 1;
            break;
        }
        currentIndex = _tempIndex;
        SetDisplay(_tempIndex);
    }

    private void Update()
    {
        if(currentIndex == 0 )
        {
            leftArrow.interactable = false;
            rightArrow.interactable = true;
        }else if(currentIndex == (skin.Count - 1) )
        {
            rightArrow.interactable = false;
            leftArrow.interactable = true;
        }
        else
        {
            rightArrow.interactable = true;
            leftArrow.interactable = true;
        }
    }
}