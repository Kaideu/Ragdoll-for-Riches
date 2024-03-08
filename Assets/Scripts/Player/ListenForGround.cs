using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenForGround : MonoBehaviour
{
    [SerializeField]
    LayerMask groundLayer;

    bool groundHitThisFrame;

    private void OnTriggerEnter(Collider other)
    {
        if (Kaideu.Utils.Helpers.IsInLayerMask(groundLayer, other.gameObject.layer) && !groundHitThisFrame)
        {
            groundHitThisFrame = true;
            Debug.LogWarning("Update 'Safe' condition for velocity check");
            Kaideu.Events.EventManager.Instance.TriggerEvent(Kaideu.Events.Events.Grounded, new Dictionary<string, object> { { "Safe", true } });
            //Kaideu.Events.EventManager.Instance.TriggerEvent(Kaideu.Events.Events.EnableRagdoll, null);

            //Destroy(this);
            //MoneyManager.Instance.UpdateBank(MoneyManager.Instance._collectedBalance);
        }
    }

    private void LateUpdate()
    {
        groundHitThisFrame = false;
    }
}
