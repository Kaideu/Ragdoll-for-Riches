using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Physics;

public class TerminalVelocity : MonoBehaviour
{
    [SerializeField]
    PhysicsManager _physM;

    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var idealDrag = _physM.MaxAcc / _physM.TVel;
        _rb.drag = idealDrag / (idealDrag * Time.fixedDeltaTime + 1);
    }
}
