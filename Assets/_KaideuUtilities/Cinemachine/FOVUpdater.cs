using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class FOVUpdater : Kaideu.Utils.SingletonPattern<FOVUpdater>
{
    /*
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private float max = 120f;
    [SerializeField] private float min = 25f;
    /**/
    public void UpdateFOV(float value)
    {
        /*
        cam.m_Lens.FieldOfView = Mathf.Clamp(value, min, max);
        cam.InternalUpdateCameraState(Vector3.up, Time.deltaTime);
        /**/
        Debug.LogError("FOVUpdater Not Setup");
    }
    
}
