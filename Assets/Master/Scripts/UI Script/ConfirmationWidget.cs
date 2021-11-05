using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConfirmationWidget : MonoBehaviour
{

    [Header("Required Widget:")]
    [SerializeField] Button _positiveButton;
    [SerializeField] Button _negativeButton;

    public Button positiveButton{ get; private set; }

    public Button negativeButton { get; private set; }

    public void ActivateWidget(UnityAction positiveButtonClicked, UnityAction negativeButtonClicked)
    {
        gameObject.SetActive(true);

        //Removing Other Listener Implementations
        _positiveButton.onClick.RemoveAllListeners();
        _negativeButton.onClick.RemoveAllListeners();

        //Adding current implementation for the confirmation Buttons
        _positiveButton.onClick.AddListener(positiveButtonClicked);
        _negativeButton.onClick.AddListener(negativeButtonClicked);
    }

    public void DeactivateWidget()
    {
        _positiveButton.onClick.RemoveAllListeners();
        _negativeButton.onClick.RemoveAllListeners();

        gameObject.SetActive(false);
    }

}
