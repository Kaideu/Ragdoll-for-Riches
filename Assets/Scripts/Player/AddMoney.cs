using System.Collections;
using System.Collections.Generic;
using Kaideu.Events;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
public class AddMoney : MonoBehaviour
{
    [SerializeField]
    LayerMask obstacles;

    GameObject _lastHit;
    Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public enum BodyPart{head, torso, arm, thigh, forearm, shin}
    public BodyPart bodyPart;
    private void OnCollisionEnter(Collision other) {
        
        if (other.gameObject != _lastHit && Kaideu.Utils.Helpers.IsInLayerMask(obstacles, other.gameObject.layer))
        {
            EventManager.Instance.TriggerEvent(Events.Impact, null);
            if (Mathf.Abs(_rb.velocity.y) > 10)
                MoneyManager.Instance.UpdateCollectedBalance(bodyPart);
            else
                Debug.Log($"Speed not enough for money: {bodyPart}");

            _lastHit = other.gameObject;
        }
    }
}