using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Events;

public class WindTrigger : MonoBehaviour
{
    [SerializeField]
    AudioClip[] Sounds;
    void OnEnable()
    {
        EventManager.Instance.StartListening(Events.StartLevel, StartWind);
        EventManager.Instance.StartListening(Events.Grounded, StopWind);
        EventManager.Instance.StartListening(Events.MainMenu, StartMusic);
    }
    void OnDisable()
    {
        EventManager.Instance.StopListening(Events.StartLevel, StartWind);
        EventManager.Instance.StopListening(Events.Grounded, StopWind);
        EventManager.Instance.StopListening(Events.MainMenu, StartMusic);
    }
    private void SwitchAudio(int index){
        gameObject.GetComponent<AudioSource>().Stop();
        gameObject.GetComponent<AudioSource>().clip = Sounds[index];
    }
    private void StartWind(Dictionary<string, object> arg){
        SwitchAudio(0);
        gameObject.GetComponent<AudioSource>().Play();
    }
    private void StopWind(Dictionary<string, object> arg){
        SwitchAudio(0);
    }
    private void StartMusic(Dictionary<string, object> arg){
        SwitchAudio(1);
        gameObject.GetComponent<AudioSource>().Play();
    }
}
