using System.Collections.Generic;
using UnityEngine.Events;

namespace Kaideu.Events
{
    public enum Events
    {
        StartLevel,
        EndLevel,
        CollectItem,
        EnableRagdoll,
        DisableRagdoll,
        UI,
        RagdollState,
        Grounded,
        BankUpdate,
        MainMenu,
        RepositionCamera,
        Customization
    }

    public class EventManager : Utils.SingletonPattern<EventManager>
    {

        private Dictionary<Events, UnityAction<Dictionary<string, object>>>
            eventDictionary = new Dictionary<Events, UnityAction<Dictionary<string, object>>>();


        public void StartListening(Events eventName, UnityAction<Dictionary<string, object>> listener)
        {
            UnityAction<Dictionary<string, object>> thisEvent;

            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent += listener;
                eventDictionary[eventName] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                eventDictionary.Add(eventName, thisEvent);
            }
        }

        public void StopListening(Events eventName, UnityAction<Dictionary<string, object>> listener)
        {
            UnityAction<Dictionary<string, object>> thisEvent;

            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent -= listener;
                eventDictionary[eventName] = thisEvent;
            }
        }

        public void TriggerEvent(Events eventName, Dictionary<string, object> message)
        {
            UnityAction<Dictionary<string, object>> thisEvent = null;

            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(message);
            }
        }

    }
}