using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Events;

public class SpeedLinesScript : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.Instance.StartListening(Events.StartLevel, StartParticles);
        EventManager.Instance.StartListening(Events.Grounded, StopParticles);
    }
    void OnDisable()
    {
        EventManager.Instance.StopListening(Events.StartLevel, StartParticles);
        EventManager.Instance.StopListening(Events.Grounded, StopParticles);
    }
    private void StartParticles(Dictionary<string, object> arg){
        gameObject.GetComponent<ParticleSystem>().Play();
    }
    private void StopParticles(Dictionary<string, object> arg){
        gameObject.GetComponent<ParticleSystem>().Stop();
    }
}
