using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class CamPositionManager : MonoBehaviour
{
    public enum CamState
    {
        None,
        MainMenu,
        Falling,
        Customize,
        Settings
    }

    [Serializable]
    public class CamSettings
    {
        [SerializeField]
        public CamState state = CamState.None;
        [SerializeField]
        public float transitionToTime = 0;
        [SerializeField]
        public Vector3 followOffset = Vector3.zero;
        [SerializeField]
        public Vector3 trackedObjectOffset = Vector3.zero;
        [SerializeField]
        public Vector3 aimOffset = Vector3.zero;
    }

    [SerializeField]
    CinemachineVirtualCamera _vc;
    [SerializeField]
    CamSettings[] _camSettingsList;

    Dictionary<CamState, CamSettings> _camSettingsDict;

    CinemachineTransposer _tp;
    CinemachineComposer _cp;
    CinemachineCameraOffset _os;

    [SerializeField]
    CamSettings _currentSettings;
    CamSettings _lastSettings;

    float lerpTime = 0;
    float startTime;
    private void Awake()
    {
        _camSettingsDict = new();
        foreach (CamSettings cs in _camSettingsList)
        {
            _camSettingsDict.Add(cs.state, cs);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        _tp = _vc.GetCinemachineComponent<CinemachineTransposer>();
        _cp = _vc.GetCinemachineComponent<CinemachineComposer>();
        _os = _vc.GetComponent<CinemachineCameraOffset>();

        _currentSettings = _camSettingsDict[CamState.MainMenu];
        _lastSettings = _camSettingsDict[CamState.MainMenu];
    }

    private void OnEnable()
    {
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.StartLevel, SetPlay);
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.EndLevel, SetMainMenu);
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.RepositionCamera, SetCamState);
    }

    private void OnDisable()
    {
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.StartLevel, SetPlay);
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.EndLevel, SetMainMenu);
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.RepositionCamera, SetCamState);
    }

    private void SetMainMenu(Dictionary<string, object> arg0) => SetState(CamState.MainMenu);

    private void SetPlay(Dictionary<string, object> arg0) => SetState(CamState.Falling);

    private void SetCamState(Dictionary<string, object> arg0)
    {
        var state = (CamState)arg0["State"];
        SetState(state);
        object obj;
        Transform target;
        if (arg0.TryGetValue("Target", out obj))
        {
            target = (Transform)obj;
            _vc.Follow = target;
            _vc.LookAt = target;
        }
        else
        {
            _vc.Follow = LevelManager.Instance.Player.transform;
            _vc.LookAt = LevelManager.Instance.Player.transform;
        }
        /*
        switch (state)
        {
            case CamState.Settings:
                var temp = ((GameObject)arg0["Target"]).transform;
                _vc.Follow = temp.transform;
                _vc.LookAt = temp.transform;
                break;
            default:
                _vc.Follow = LevelManager.Instance.Player.transform;
                _vc.LookAt = LevelManager.Instance.Player.transform;
                break;
        }
        /**/
    }
    private void SetState(CamState state)
    {
        _lastSettings = _currentSettings;
        _currentSettings = _camSettingsDict[state];
        startTime = Time.time;
        lerpTime = 0;
    }

    private void Update()
    {
        if (lerpTime < 1)
        {
            if (_currentSettings.transitionToTime <= 0)
            {
                _tp.m_FollowOffset = _currentSettings.followOffset;
                _cp.m_TrackedObjectOffset = _currentSettings.trackedObjectOffset;
                _os.m_Offset = _currentSettings.aimOffset;
                lerpTime = 1;
            }
            else
            {
                lerpTime = (Time.time - startTime) / _currentSettings.transitionToTime;
                _tp.m_FollowOffset = Vector3.Slerp(_lastSettings.followOffset, _currentSettings.followOffset, lerpTime);
                _cp.m_TrackedObjectOffset = Vector3.Slerp(_lastSettings.trackedObjectOffset, _currentSettings.trackedObjectOffset, lerpTime);
                _os.m_Offset = Vector3.Slerp(_lastSettings.aimOffset, _currentSettings.aimOffset, lerpTime);
            }
        }
    }


}
