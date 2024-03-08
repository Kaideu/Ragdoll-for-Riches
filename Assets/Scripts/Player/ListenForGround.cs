using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Events;
using System;

public class ListenForGround : MonoBehaviour
{
    [SerializeField]
    LayerMask _groundLayer;
    [SerializeField][Min(0)]
    float _maxSafeYVelocity;

    Rigidbody _rb;
    bool _groundHit;

    private void OnEnable()
    {
        EventManager.Instance.StartListening(Events.StartLevel, Reset);
    }

    private void OnDisable()
    {
        EventManager.Instance.StopListening(Events.StartLevel, Reset);
    }

    private void Reset(Dictionary<string, object> arg0) => _groundHit = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Kaideu.Utils.Helpers.IsInLayerMask(_groundLayer, other.gameObject.layer) && !_groundHit)
        {
            print(_rb.velocity.y);
            _groundHit = true;
            Debug.LogWarning("Update 'Safe' condition for velocity check");
            Kaideu.Events.EventManager.Instance.TriggerEvent(Kaideu.Events.Events.Grounded, new Dictionary<string, object> { { "Safe",  Mathf.Abs(_rb.velocity.y) <= _maxSafeYVelocity} });
            //Kaideu.Events.EventManager.Instance.TriggerEvent(Kaideu.Events.Events.EnableRagdoll, null);

            //Destroy(this);
            //MoneyManager.Instance.UpdateBank(MoneyManager.Instance._collectedBalance);
        }
    }
}
