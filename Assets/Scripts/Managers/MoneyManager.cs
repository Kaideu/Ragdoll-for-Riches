using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoneyManager : Kaideu.Utils.SingletonPattern<MoneyManager>
{

    public int _currentBalance = 0;
    public int _collectedBalance = 0;
    private int collectedBalance;

    public void UpdateBank(int collectedMoney)
    {
        _currentBalance += collectedMoney;
        _collectedBalance = 0;
        // Debug.Log(_currentBalance);
    }
    public void UpdateBalance(string bodypart)
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
    }
    public bool UpdateBalance(int price)
    {
        if (price <= _currentBalance)
        {
            _currentBalance -= price;
            return true;

        }
        else
        {
            return false;
        }
    }


}