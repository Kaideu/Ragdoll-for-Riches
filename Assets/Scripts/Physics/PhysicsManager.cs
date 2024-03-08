using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaideu.Physics
{
    public class PhysicsManager : MonoBehaviour
    {
        [SerializeField]
        float _maxAcceleration = 9.8f;
        public float MaxAcc => _maxAcceleration;

        [SerializeField]
        float _terminalVelocity = 50;
        public float TVel => _terminalVelocity;
        float _oTV;

        private void Start()
        {
            _oTV = _terminalVelocity;
        }

        public void SetTerminalVelocity(float amount) => _terminalVelocity = amount;

        public void ResetTerminalVelocity() => _terminalVelocity = _oTV;
    }
}