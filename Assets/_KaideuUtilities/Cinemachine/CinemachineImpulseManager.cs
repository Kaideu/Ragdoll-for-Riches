/*
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using Kaideu.Utilities;

public class CinemachineImpulseManager : PGCTools.Singleton<CinemachineImpulseManager>
{

    public enum ShakeType
    {
        Shot,
        ChargedShot,
        ExplosiveBarrel
    }

    [Serializable]
    public class ShakeProfile
    {
        public ShakeType type;
        public CinemachineImpulseSource impulseSource;
        public float force;
    }

    [SerializeField] private ShakeProfile[] shakeProfiles;
    [SerializeField] private CinemachineIndependentImpulseListener[] listenerProfilesToAdd;

    private Dictionary<ShakeType, ShakeProfile> shakeDictionary;

    private void Awake()
    {
        shakeDictionary = new Dictionary<ShakeType, ShakeProfile>();

        foreach (ShakeProfile profile in shakeProfiles)
        {
            shakeDictionary.Add(profile.type, profile);
        }

        foreach (CinemachineIndependentImpulseListener l in listenerProfilesToAdd)
        {
            Camera.main.gameObject.AddComponent(l);
        }
    }

    public void SendImpulse(ShakeType type, float forceModifier = 1)
    {
        ShakeProfile profile;
        
        if (shakeDictionary.TryGetValue(type, out profile))
        {
            profile.impulseSource.GenerateImpulseWithForce(profile.force * forceModifier);
        }

    }

}
/**/