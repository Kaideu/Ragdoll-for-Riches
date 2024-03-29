using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenForGround : MonoBehaviour
{
    [SerializeField]
    LayerMask groundLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (Kaideu.Utils.Helpers.IsInLayerMask(groundLayer, other.gameObject.layer))
        {
            Debug.LogWarning("Update 'Safe' condition for velocity check");
            Kaideu.Events.EventManager.Instance.TriggerEvent(Kaideu.Events.Events.EndLevel, new Dictionary<string, object> { { "Safe", true } });
            Destroy(this);
        }
    }
}
