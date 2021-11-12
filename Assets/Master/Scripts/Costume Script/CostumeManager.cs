using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CostumeManager : MonoBehaviour
{
    [SerializeField] List<Costume> hatList = new List<Costume>();
    [SerializeField] List<Image> costumePos = new List<Image>();
    [SerializeField] Button nextButton;
    [SerializeField] Button prevButton;
    [SerializeField] Button selectButton;
    [SerializeField] Image lockImage;

    public CostumeType CostumeType;

    public bool ChoosingHat;

    int currentHatIndex;
    GameManager gameManager;
    EquipedCostume playerCostume;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerCostume = gameManager.GetComponent<EquipedCostume>();
        ShowCostume(gameManager.hatIndex);
    }

    private void Update()
    {
        if (ChoosingHat)
        {
            if (currentHatIndex == 0)
            {
                prevButton.interactable = false;
            }
            else
            {
                prevButton.interactable = true;
            }
            if (currentHatIndex == (hatList.Count - 1))
            {
                nextButton.interactable = false;
            }
            else
            {
                nextButton.interactable = true;
            }
        }
    }

    public void NextCostume()
    {
        switch (CostumeType)
        {
            case CostumeType.Hat:
                currentHatIndex += 1;
                ShowCostume(currentHatIndex);
                break;
            default:
                break;
        }
    }

    public void PreviousCostume()
    {
        switch (CostumeType)
        {
            case CostumeType.Hat:
                currentHatIndex -= 1;
                ShowCostume(currentHatIndex);
                break;
            default:
                break;
        }
    }

    public void SelectCostume()
    {
        switch (CostumeType)
        {
            case CostumeType.Hat:
                playerCostume.hat = hatList[currentHatIndex].CostumeSprite;
                gameManager.hatIndex = currentHatIndex;
                break;
        }
    }

    public void ShowCostume(int index)
    {
        switch (CostumeType)
        {
            case CostumeType.Hat:
                if (!hatList[index].IsCostumeUnlocked)
                {
                    lockImage.gameObject.SetActive(true);
                    selectButton.interactable = false;
                }
                else
                {
                    lockImage.gameObject.SetActive(false);
                    selectButton.interactable = true;
                }
                Color hatPosColor = costumePos[0].color;
                if (hatList[index].CostumeSprite == null)
                {
                    costumePos[0].color = new Color(hatPosColor.r, hatPosColor.g, hatPosColor.b, 0);
                }
                else
                {
                    costumePos[0].sprite = hatList[index].CostumeSprite;
                    costumePos[0].color = new Color(hatPosColor.r, hatPosColor.g, hatPosColor.b, 255);
                }
                break;
            default:
                break;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(gameManager.GameplaySceneName, LoadSceneMode.Single);
    }
    public void Back()
    {
        SceneManager.LoadScene(gameManager.MainMenuSceneName, LoadSceneMode.Single);
    }
}

public enum CostumeType
{
    Hat
}