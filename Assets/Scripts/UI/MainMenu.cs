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
    }

    private void OnDisable()
    {
        EventManager.Instance.StopListening(Events.BankUpdate, UpdateBank);
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


}
