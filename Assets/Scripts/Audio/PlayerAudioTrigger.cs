using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Events;

public class PlayerAudioTrigger : MonoBehaviour
{
    [SerializeField]
    AudioClip[] Sounds;

    private void OnEnable() {
        EventManager.Instance.StartListening(Events.Parachute, PlayParacute);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(Events.Parachute, PlayParacute);
    }

    private void PlayParacute(Dictionary<string, object> arg){
        SwitchAudio(0);
        gameObject.GetComponent<AudioSource>().Play();
    }
    private void PlayStep(Dictionary<string, object> arg){
        SwitchAudio(1);
        gameObject.GetComponent<AudioSource>().Play();
    }
    private void PlayJump(Dictionary<string, object> arg){
        SwitchAudio(2);
        gameObject.GetComponent<AudioSource>().Play();
    }
    private void SwitchAudio(int index){
        gameObject.GetComponent<AudioSource>().Stop();
        gameObject.GetComponent<AudioSource>().clip = Sounds[index];
    }
}
