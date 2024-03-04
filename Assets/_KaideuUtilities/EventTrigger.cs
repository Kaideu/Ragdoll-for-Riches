using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaideu.Events
{
    #region CustomObjects

    [Serializable]
    public enum TriggerType
    {
        Awake,
        Start,
        OnTriggerEnter,
        OnTriggerExit,
        OnCollisionEnter,
        OnCollisionExit,
        OnInteractable
    }

    [Serializable]
    public enum ValueType
    {
        String,
        Float,
        Int
    }

    [Serializable]
    public class EventKeyValuePair
    {
        public string Key;
        public virtual object Value { get; }
    }

    [Serializable]
    public class EventKeyStringValue : EventKeyValuePair
    {
        public string value;
        public override object Value => value;
    }
    [Serializable]
    public class EventKeyFloatValue : EventKeyValuePair
    {
        public float value;
        public override object Value => value;
    }
    [Serializable]
    public class EventKeyIntValue : EventKeyValuePair
    {
        public int value;
        public override object Value => value;
    }

    [Serializable]
    public class CustomEvent
    {
        public Kaideu.Events.Events eventType;
        public TriggerType triggerType;
        [Min(0)]
        [SerializeField] float triggerCoolDown;
        [Min(0)]
        [Tooltip("0 = no cap")]
        [SerializeField] int maxTriggerCount;

        public List<EventKeyStringValue> EventKeyStringValues = new();
        public List<EventKeyFloatValue> EventKeyFloatValues = new();
        public List<EventKeyIntValue> EventKeyIntValues = new();

        public Dictionary<string, object> EventDictionary = new();

        private int triggerCount;
        private float lastTriggerTime;

        public bool TriggerCapReached => maxTriggerCount != 0 && triggerCount >= maxTriggerCount;

        public void Initialize()
        {
            foreach (var evtPair in EventKeyStringValues)
                EventDictionary.Add(evtPair.Key, evtPair.Value);
            foreach (var evtPair in EventKeyFloatValues)
                EventDictionary.Add(evtPair.Key, evtPair.Value);
            foreach (var evtPair in EventKeyIntValues)
                EventDictionary.Add(evtPair.Key, evtPair.Value);

            lastTriggerTime = Time.time - triggerCoolDown;
        }

        public void TriggerEvent()
        {
            if (lastTriggerTime + triggerCoolDown <= Time.time)
            {
                EventManager.Instance.TriggerEvent(eventType, EventDictionary);
                triggerCount++;
                lastTriggerTime = Time.time;
            }
            else
                Debug.Log("Event On Cooldown");
        }
    }


    /**/
    #endregion

    public class EventTrigger : MonoBehaviour
    {
        
        [Tooltip("Ignored by Awake and Start trigger types")]
        [SerializeField] LayerMask collisionTriggerLayers;
        [SerializeField] List<CustomEvent> events = new();

        // Start is called before the first frame update
        void Awake()
        {
            foreach (var evt in events)
            {
                evt.Initialize();
            }

            TryTriggerEvents(TriggerType.Awake);
        }

        private void Start()
        {
            TryTriggerEvents(TriggerType.Start);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Kaideu.Utils.Helpers.IsInLayerMask(other.gameObject.layer, collisionTriggerLayers))
                TryTriggerEvents(TriggerType.OnTriggerEnter);
        }

        private void OnTriggerExit(Collider other)
        {
            if (Kaideu.Utils.Helpers.IsInLayerMask(other.gameObject.layer, collisionTriggerLayers))
                TryTriggerEvents(TriggerType.OnTriggerExit);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (Kaideu.Utils.Helpers.IsInLayerMask(collision.gameObject.layer, collisionTriggerLayers))
                TryTriggerEvents(TriggerType.OnCollisionEnter);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (Kaideu.Utils.Helpers.IsInLayerMask(collision.gameObject.layer, collisionTriggerLayers))
                TryTriggerEvents(TriggerType.OnCollisionExit);
        }

        public void TryTriggerEvents(TriggerType triggerType)
        {
            print(triggerType);
            foreach (var evt in events)
            {
                if (triggerType == evt.triggerType)
                {
                    evt.TriggerEvent();
                }
            }

            events.RemoveAll(x => x.TriggerCapReached == true);
        }
    }
}