using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    enum RagdollAnimState
    {
        Idle,
        Ragdoll
    }

    Rigidbody[] _rbBodyParts;
    Animator _anim;
    CharacterController _cc;
    RagdollAnimState _currentState = RagdollAnimState.Idle;

    // Start is called before the first frame update
    private void Start()
    {
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.EnableRagdoll, EnableRagdoll);
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.DisableRagdoll, DisableRagdoll);

        _rbBodyParts = GetComponentsInChildren<Rigidbody>();
        _anim = GetComponent<Animator>();
        _cc = GetComponent<CharacterController>();

        DisableRagdoll(null);
    }

    private void Update()
    {
        switch (_currentState)
        {
            case RagdollAnimState.Idle:
                IdleBehaviour();
                break;
            case RagdollAnimState.Ragdoll:
                RagdollBehaviour();
                break;
        }
    }

    private void DisableRagdoll(Dictionary<string, object> arg0)
    {
        foreach (var rb in _rbBodyParts)
        {
            rb.isKinematic = true;
        }
        _anim.enabled = true;
        _cc.enabled = true;

        _currentState = RagdollAnimState.Idle;
    }

    private void EnableRagdoll(Dictionary<string, object> arg0)
    {
        foreach (var rb in _rbBodyParts)
        {
            rb.isKinematic = false;
        }
        _anim.enabled = false;
        _cc.enabled = false;

        _currentState = RagdollAnimState.Ragdoll;
    }

    private void IdleBehaviour()
    {
        if (Kaideu.Input.InputManager.Instance.Controls.Player.Space.WasPressedThisFrame())
        {
            //EnableRagdoll(null);
            _anim.SetBool("Falling", true);
        }
    }

    private void RagdollBehaviour()
    {
        if (Kaideu.Input.InputManager.Instance.Controls.Player.Space.WasPressedThisFrame())
        {
            DisableRagdoll(null);
        }
    }


}
