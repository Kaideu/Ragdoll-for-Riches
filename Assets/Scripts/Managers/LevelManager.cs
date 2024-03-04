using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Events;
using System;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        EventManager.Instance.StartListening(Events.EndLevel, EndLevel);
    }

    private void EndLevel(Dictionary<string, object> arg0)
    {
        Debug.LogError("Level Ended");
    }
}
