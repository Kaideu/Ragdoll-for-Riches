using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityPeeker : MonoBehaviour
{
    Rigidbody _rb;
    public Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _velocity = _rb.velocity;
    }
}
