using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Input;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    Rigidbody _rb;

    [SerializeField]
    float _moveSpeed;

    [SerializeField]
    Vector2 _input;

    private void Start()
    {
        InputManager.Instance.ToggleControls(true);
        InputManager.Instance.SwitchTo(InputManager.Instance.Controls.Player);
    }

    // Update is called once per frame
    void Update()
    {
        _input = InputManager.Instance.Controls.Player.Move.ReadValue<Vector2>();

        if (_rb)
            _rb.AddForce(new Vector3(_input.x, 0, _input.y) * _moveSpeed * Time.deltaTime, ForceMode.Force);
    }
}
