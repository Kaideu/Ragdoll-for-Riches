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

    bool _isSafe;

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
        _isSafe = (bool)arg0["Safe"];
        _title.text = (_isSafe) ? "Safe Landing" : "Game Over";
        _score.text = "$" + ((_isSafe)?MoneyManager.Instance._collectedBalance:"0");
        UIHandler.Instance.ShowUI("EndLevel");
    }

    public void CompleteLevel()
    {
        EventManager.Instance.TriggerEvent(Events.EndLevel, new Dictionary<string, object> { { "Safe", _isSafe } });
    }
}
