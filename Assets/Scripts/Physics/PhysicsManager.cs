using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaideu.Physics
{
    public class PhysicsManager : MonoBehaviour
    {
        [SerializeField]
        float maxAcceleration = 9.8f;
        public float MaxAcc => maxAcceleration;

        [SerializeField]
        float terminalVelocity = 50;
        public float TVel => terminalVelocity;
    }
}