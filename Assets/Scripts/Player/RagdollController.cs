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
    Rigidbody _hrb;

    [SerializeField]
    RagdollAnimState _currentState = RagdollAnimState.Idle;


    private void OnEnable()
    {
        ListenToEvents();
    }

    private void OnDisable()
    {
        DontListenToEvents();
    }

    // Start is called before the first frame update
    private void Start()
    {
        //ListenToEvents();

        _rbBodyParts = GetComponentsInChildren<Rigidbody>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _hrb = GetComponentInChildren<ListenForGround>().GetComponent<Rigidbody>();

        DisableRagdoll(null);
        _anim.enabled = true;
        _currentState = RagdollAnimState.Idle;
    }

    void ListenToEvents()
    {
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.EnableRagdoll, EnableRagdoll);
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.DisableRagdoll, DisableRagdoll);
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.RagdollState, EnableState);
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.EndLevel, ResetRagdoll); //change to hit ground for end level ui
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.StartLevel, Jump);
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.Grounded, CancelVelocity);
    }

    void DontListenToEvents()
    {
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.EnableRagdoll, EnableRagdoll);
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.DisableRagdoll, DisableRagdoll);
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.RagdollState, EnableState);
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.EndLevel, ResetRagdoll); //change to hit ground for end level ui
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.StartLevel, Jump);
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.Grounded, CancelVelocity);
    }

    private void Jump(Dictionary<string, object> arg0) => _anim.SetTrigger("Jumped");

    private void EnableState(Dictionary<string, object> arg0)
    {
        _currentState = (RagdollAnimState)arg0["State"];
        switch (_currentState)
        {
            case RagdollAnimState.Idle:
                _rb.isKinematic = true;
                _anim.applyRootMotion = true;
                _anim.ResetTrigger("Reset");
                _anim.ResetTrigger("Jump");
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
        _rb.velocity = _hrb.velocity;
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
            rb.velocity = _rb.velocity;
        }
        _anim.enabled = false;
        _currentState = RagdollAnimState.Ragdoll;
    }

    private void IdleBehaviour()
    {
        if (Kaideu.Input.InputManager.Instance.Controls.Player.Space.WasPressedThisFrame())
        {
            //_anim.SetTrigger("Jumped");
        }
        _rb.isKinematic = true;
    }

    private void RagdollBehaviour()
    {
        if (Kaideu.Input.InputManager.Instance.Controls.Player.Ragdoll.WasPressedThisFrame())
        {
            DisableRagdoll(null);
        }
        _rb.velocity = _hrb.velocity;
    }

    private void FallingBehaviour()
    {
        EnableRagdoll(null);
        if (Kaideu.Input.InputManager.Instance.Controls.Player.Ragdoll.WasPressedThisFrame())
        {
            EnableRagdoll(null);
        }
        _rb.isKinematic = false;
    }

    private void ResetRagdoll(Dictionary<string, object> arg0)
    {
        _anim.Rebind();
        _anim.enabled = true;
        _rb.isKinematic = true;
        _anim.SetTrigger("Reset");
        _anim.Update(0f);
        DisableRagdoll(null);
        _currentState = RagdollAnimState.Idle;
    }

    private void CancelVelocity(Dictionary<string, object> arg0)
    {
        foreach (var rb in _rbBodyParts)
        {
            rb.velocity = Vector3.zero;
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = true;
        }
    }


}
