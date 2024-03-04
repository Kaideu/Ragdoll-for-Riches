using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.UI;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        UIHandler.Instance.ShowUI("HUD");
        Kaideu.Events.EventManager.Instance.TriggerEvent(Kaideu.Events.Events.StartLevel, null);
    }
}
