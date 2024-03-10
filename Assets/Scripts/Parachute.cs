using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    [SerializeField]
    GameObject parachuteObj;

    private void OnEnable()
    {
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.Parachute, Deploy);
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.EndLevel, Pack);
    }

    private void OnDisable()
    {
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.Parachute, Deploy);
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.EndLevel, Pack);
    }

    private void Deploy(Dictionary<string, object> arg0) => parachuteObj.SetActive(true);
    private void Pack(Dictionary<string, object> arg0) => parachuteObj.SetActive(false);

}
