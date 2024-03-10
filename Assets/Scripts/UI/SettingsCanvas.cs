using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Events;
using Kaideu.UI;

public class SettingsCanvas : MonoBehaviour
{
    [SerializeField]
    Transform SettingsCamTarget;

    private void OnEnable()
    {
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.Settings, ShowSelf);
    }

    private void OnDisable()
    {
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.Settings, ShowSelf);
    }

    public void BackToMenu()
    {
        Kaideu.Events.EventManager.Instance.TriggerEvent(Kaideu.Events.Events.MainMenu, null);
    }

    public void ShowSelf(Dictionary<string, object> arg0)
    {
        UIHandler.Instance.ShowUI("Settings");
        var hasEnded = LevelManager.Instance.HasEnded;
        EventManager.Instance.TriggerEvent(Events.RepositionCamera, new Dictionary<string, object> { 
            { "State", (hasEnded) ? CamPositionManager.CamState.Settings : CamPositionManager.CamState.Falling }, 
            { "Target", SettingsCamTarget } });
    }
}
