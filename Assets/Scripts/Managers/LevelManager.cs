using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Events;
using Kaideu.Input;
using System;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        EventManager.Instance.StartListening(Events.StartLevel, StartLevel);
        EventManager.Instance.StartListening(Events.EndLevel, EndLevel);

        StartLevel(null);

    }

    private void EndLevel(Dictionary<string, object> arg0)
    {
        Debug.LogError("Level Ended");
        InputManager.Instance.ToggleControls(false);
        //EventManager.Instance.TriggerEvent(Events.UI, )
        //Reset Level numbers, positions, camera, etc as needed
    }

    private void StartLevel(Dictionary<string, object> arg0)
    {
        InputManager.Instance.ToggleControls(true);
        InputManager.Instance.SwitchTo(InputManager.Instance.Controls.Player);
        //Camera, animations, etc
    }
}
