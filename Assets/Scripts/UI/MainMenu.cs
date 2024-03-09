using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.UI;
using Kaideu.Events;
using System;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _bankBalance;

    private void OnEnable()
    {
        EventManager.Instance.StartListening(Events.BankUpdate, UpdateBank);
        EventManager.Instance.StartListening(Events.MainMenu, ShowSelf);
    }

    private void OnDisable()
    {
        EventManager.Instance.StopListening(Events.BankUpdate, UpdateBank);
        EventManager.Instance.StopListening(Events.MainMenu, ShowSelf);
    }

    private void ShowSelf(Dictionary<string, object> arg0)
    {
        Kaideu.UI.UIHandler.Instance.ShowUI("MainMenu");
        Kaideu.Events.EventManager.Instance.TriggerEvent(Kaideu.Events.Events.RepositionCamera, new Dictionary<string, object>() { { "State", CamPositionManager.CamState.MainMenu } });
    }

    private void UpdateBank(Dictionary<string, object> arg0)
    {
        _bankBalance.text = $"${(int)arg0["Balance"]}";
    }

    public void Play()
    {
        UIHandler.Instance.ShowUI("HUD");
        Kaideu.Events.EventManager.Instance.TriggerEvent(Kaideu.Events.Events.StartLevel, null);
    }

    public void Customize()
    {
        EventManager.Instance.TriggerEvent(Events.Customization, null);
    }


}
