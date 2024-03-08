using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Kaideu.Events;

public class MoneyManager : Kaideu.Utils.SingletonPattern<MoneyManager>
{

    public int _currentBalance = 0;
    public int _collectedBalance = 0;
    private int collectedBalance;

    private void OnEnable()
    {
        EventManager.Instance.StartListening(Events.EndLevel, DepositCollected);
    }
    private void OnDisable()
    {
        EventManager.Instance.StopListening(Events.EndLevel, DepositCollected);
    }

    public void DepositCollected(Dictionary<string, object> arg0)
    {
        _currentBalance += _collectedBalance;
        _collectedBalance = 0;

        EventManager.Instance.TriggerEvent(Events.BankUpdate, new Dictionary<string, object> { { "Balance", _currentBalance } });
        // Debug.Log(_currentBalance);
    }
    public void UpdateCollectedBalance(string bodypart)
    {


        Debug.Log(bodypart);
        switch (bodypart)
        {

            case "head":
                collectedBalance = 1000;
                // Debug.LogWarning(collectedBalance);
                break;
            case "torso":
                collectedBalance = 500;
                // Debug.LogWarning(collectedBalance);
                break;
            case "leg":
                collectedBalance = 250;
                // Debug.LogWarning(collectedBalance);
                break;
            case "arm":
                collectedBalance = 250;
                // Debug.LogWarning(collectedBalance);
                break;

        }
        _collectedBalance += collectedBalance;

        // Debug.LogWarning(_collectedBalance);
        collectedBalance = 0;
        HUD.Instance.UpdateCollected(_collectedBalance);
    }
    public bool UpdateBankBalance(int price)
    {
        bool returned;
        if (price <= _currentBalance)
        {
            _currentBalance -= price;
            returned = true;

        }
        else
        {
            returned = false;
        }

        EventManager.Instance.TriggerEvent(Events.BankUpdate, new Dictionary<string, object> { { "Balance", _currentBalance } });

        return returned;
    }


}