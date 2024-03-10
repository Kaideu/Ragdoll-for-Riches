using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Events;
using System;

public class MenuTrigger : MonoBehaviour
{
    [SerializeField]
    AudioClip[] Sounds;
    [SerializeField]
    AudioSource music;
    [SerializeField]
    AudioSource sfx;
    [SerializeField]
    AudioSource bg;

    void OnEnable()
    {
        EventManager.Instance.StartListening(Events.StartLevel, StartWind);
        EventManager.Instance.StartListening(Events.Grounded, StopWind);
        EventManager.Instance.StartListening(Events.MainMenu, StartMusic);
        EventManager.Instance.StartListening(Events.Parachute, PlayParaSFX);
        EventManager.Instance.StartListening(Events.PowerUp, PlayPUSFX);
        EventManager.Instance.StartListening(Events.Impact, PlayImpactSFX);
    }
    void OnDisable()
    {
        EventManager.Instance.StopListening(Events.StartLevel, StartWind);
        EventManager.Instance.StopListening(Events.Grounded, StopWind);
        EventManager.Instance.StopListening(Events.MainMenu, StartMusic);
        EventManager.Instance.StopListening(Events.Parachute, PlayParaSFX);
        EventManager.Instance.StopListening(Events.PowerUp, PlayPUSFX);
        EventManager.Instance.StopListening(Events.Impact, PlayImpactSFX);
    }
    private void SwitchSFX(int index){
        sfx.Stop();
        sfx.clip = Sounds[index];
    }
    private void StartWind(Dictionary<string, object> arg){
        music.Stop();
        bg.Play();
    }
    private void StopWind(Dictionary<string, object> arg){
        bg.Stop();
        var isSafe = (bool)arg["Safe"];
        if(isSafe){
            sfx.clip = Sounds[4];
        }
        else{
            sfx.clip = Sounds[6];
        }
        sfx.Play();
        
    }
    
    private void StartMusic(Dictionary<string, object> arg){
        music.Play();
    }
    private void PlayParaSFX(Dictionary<string, object> arg){
        SwitchSFX(2);
        sfx.Play();
    }
    private void PlayStepSFX(Dictionary<string, object> arg){
        SwitchSFX(3);
        sfx.Play();
    }
    private void PlayJump(Dictionary<string, object> arg){
        SwitchSFX(4);
        sfx.Play();
    }
    private void PlayPUSFX(Dictionary<string, object> arg){
        SwitchSFX(7);
        sfx.Play();
    }
    private void PlayImpactSFX(Dictionary<string, object> arg){
        sfx.clip = Sounds[5];
        sfx.Play();
    }
}
