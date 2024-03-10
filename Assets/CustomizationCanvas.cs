using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.UI;
using Kaideu.Events;
using System;
using UnityEngine.UI;
using TMPro;

public class CustomizationCanvas : MonoBehaviour
{
    [Serializable]
    public struct ButtonUICombo
    {
        [SerializeField]
        public Canvas itemGUIs;

        [SerializeField]
        public Button enableButton;
    }

    [SerializeField]
    ButtonUICombo[] _buttonUICombos;
    [SerializeField]
    TextMeshProUGUI _bankAmount;

    private void OnEnable()
    {
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.Customization, ShowSelf);
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.BankUpdate, updateBankText);
    }

    private void OnDisable()
    {
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.Customization, ShowSelf);
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.BankUpdate, updateBankText);
    }

    private void Start()
    {
        /*
        for (int i = 0; i < _buttonUICombos.Length; i++)
        {
            print(i);
            _buttonUICombos[i].enableButton.onClick.AddListener(delegate { SwitchItem(i); });
        }
        /**/
        SwitchItem(0);
    }

    public void BackToMenu()
    {
        foreach (ButtonUICombo ui in _buttonUICombos)
            ui.itemGUIs.enabled = false;
        Kaideu.Events.EventManager.Instance.TriggerEvent(Kaideu.Events.Events.MainMenu, null);
    }

    public void ShowSelf(Dictionary<string, object> arg0)
    {
        _buttonUICombos[0].itemGUIs.enabled = true;
        UIHandler.Instance.ShowUI("Customization");
        EventManager.Instance.TriggerEvent(Events.RepositionCamera, new Dictionary<string, object> { { "State", CamPositionManager.CamState.Customize } });
    }

    public void SwitchItem(int index)
    {
        for (int i = 0; i < _buttonUICombos.Length; i++)
        {
            _buttonUICombos[i].itemGUIs.enabled = (i == index);
            _buttonUICombos[i].enableButton.interactable = !(i == index);
        }
    }

    private void updateBankText(Dictionary<string, object> arg0)
    {
        _bankAmount.text = $"${arg0["Balance"]}";
    }
}
