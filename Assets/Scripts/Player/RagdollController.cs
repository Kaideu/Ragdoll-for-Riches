using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public enum RagdollAnimState
    {
        Idle,
        Jumping,
        Falling,
        Ragdoll
    }

    Rigidbody[] _rbBodyParts;
    Animator _anim;
    Rigidbody _rb;

    [SerializeField]
    RagdollAnimState _currentState = RagdollAnimState.Idle;

    /*
    private void OnEnable()
    {
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.EnableRagdoll, EnableRagdoll);
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.DisableRagdoll, DisableRagdoll);
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.RagdollState, EnableState);
    }

    private void OnDisable()
    {
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.EnableRagdoll, EnableRagdoll);
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.DisableRagdoll, DisableRagdoll);
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.RagdollState, EnableState);
    }
    /**/

    // Start is called before the first frame update
    private void Start()
    {
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.EnableRagdoll, EnableRagdoll);
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.DisableRagdoll, DisableRagdoll);
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.RagdollState, EnableState);

        _rbBodyParts = GetComponentsInChildren<Rigidbody>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();

        DisableRagdoll(null);
        _anim.enabled = true;
    }

    private void EnableState(Dictionary<string, object> arg0)
    {
        _currentState = (RagdollAnimState)arg0["State"];
        switch (_currentState)
        {
            case RagdollAnimState.Idle:
                _rb.isKinematic = true;
                _anim.applyRootMotion = true;
                break;
            case RagdollAnimState.Falling:
                _rb.isKinematic = false;
                _anim.applyRootMotion = false;
                break;
        }
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
            case RagdollAnimState.Falling:
                FallingBehaviour();
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
        _currentState = RagdollAnimState.Falling;
    }

    private void EnableRagdoll(Dictionary<string, object> arg0)
    {
        foreach (var rb in _rbBodyParts)
        {
            rb.isKinematic = false;
        }
        _anim.enabled = false;
        _currentState = RagdollAnimState.Ragdoll;
    }

    private void IdleBehaviour()
    {
        if (Kaideu.Input.InputManager.Instance.Controls.Player.Space.WasPressedThisFrame())
        {
            _anim.SetBool("Jumped", true);
        }
    }

    private void RagdollBehaviour()
    {
        if (Kaideu.Input.InputManager.Instance.Controls.Player.Space.WasPressedThisFrame())
        {
            DisableRagdoll(null);
        }
    }

    private void FallingBehaviour()
    {

        if (Kaideu.Input.InputManager.Instance.Controls.Player.Space.WasPressedThisFrame())
        {
            EnableRagdoll(null);
        }
    }


}
