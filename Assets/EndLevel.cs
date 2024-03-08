using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Events;
using Kaideu.UI;
using System;
using TMPro;

public class EndLevel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _title;
    [SerializeField]
    TextMeshProUGUI _score;

    private void OnEnable()
    {
        EventManager.Instance.StartListening(Events.Grounded, Show);
    }
    private void OnDisable()
    {
        EventManager.Instance.StartListening(Events.Grounded, Show);
    }

    private void Show(Dictionary<string, object> arg0)
    {
        var isSafe = (bool)arg0["Safe"];
        _title.text = (isSafe) ? "Safe Landing" : "Game Over";
        _score.text = "$" + MoneyManager.Instance._collectedBalance.ToString();
        UIHandler.Instance.ShowUI("EndLevel");
    }

    public void CompleteLevel()
    {
        EventManager.Instance.TriggerEvent(Events.EndLevel, null);
    }
}
