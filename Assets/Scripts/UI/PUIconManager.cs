using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Events;
using UnityEngine.UI;

public class PUIconManager : Kaideu.Utils.SingletonPattern<PUIconManager>
{
    [SerializeField]
    Image Slow;
    [SerializeField]
    Image Money;
    [SerializeField]
    Image Objects;

    private void OnEnable() {
        EventManager.Instance.StartListening(Events.PowerUpOver, DisableIcon);
    }
    public void ShowIcon(PowerUp.Powers powers){
        switch(powers){
            case PowerUp.Powers.Slow:
            Slow.enabled = true;
            Money.enabled = false;
            Objects.enabled = false;
            break;
            case PowerUp.Powers.ClaimMultiply:
            Money.enabled = true;
            Slow.enabled = false;
            Objects.enabled = false;
            break;
            case PowerUp.Powers.SpawnMultiply:
            Objects.enabled = true;
            Money.enabled = false;
            Slow.enabled = false;
            break;
        }
    }
    private void DisableIcon(Dictionary<string, object> arg){
        Objects.enabled = false;
        Slow.enabled = false;
        Money.enabled = false;
    }
}
