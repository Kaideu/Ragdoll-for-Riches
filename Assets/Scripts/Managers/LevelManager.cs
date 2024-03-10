using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Events;
using Kaideu.Input;
using System;

public class LevelManager : Kaideu.Utils.SingletonPattern<LevelManager>
{
    [SerializeField]
    GameObject _playerPrefab;
    [SerializeField]
    Transform _startPosition;
    [SerializeField]
    Cinemachine.CinemachineVirtualCamera _cmvc;
    [SerializeField]
    float _minParachuteHeight = 50;
    [SerializeField]
    float spawnerStopHeight = 150;

    bool hinted = false;
    bool hasEnded = true;
    public bool HasEnded => hasEnded;
    public float MinParachuteHeight => _minParachuteHeight;
    public float SpawnerStopHeight => spawnerStopHeight;

    private GameObject _player;
    public GameObject Player
    {
        get { if (_player == null) _player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity); return _player; }
    }

    private void OnEnable()
    {
        EventManager.Instance.StartListening(Events.StartLevel, StartLevel);
        EventManager.Instance.StartListening(Events.Grounded, DisableHint);
        EventManager.Instance.StartListening(Events.EndLevel, EndLevel);
    }

    private void OnDisable()
    {
        EventManager.Instance.StopListening(Events.StartLevel, StartLevel);
        EventManager.Instance.StopListening(Events.Grounded, DisableHint);
        EventManager.Instance.StopListening(Events.EndLevel, EndLevel);
    }
    private void Start()
    {
        Player.transform.position = _startPosition.position;
        EventManager.Instance.TriggerEvent(Events.MainMenu, null);
    }

    public void SetPlayerCam(Dictionary<string, object> arg0)
    {
        _cmvc.Follow = Player.transform;
        _cmvc.LookAt = Player.transform;
    }

    private void EndLevel(Dictionary<string, object> arg0)
    {
        if (!hasEnded)
        {
            Debug.LogError("Level Ended");
            hasEnded = true;
            InputManager.Instance.SwitchTo(InputManager.Instance.Controls.UI);

            Player.transform.position = _startPosition.position;
            _cmvc.Follow = Player.transform;
            _cmvc.LookAt = Player.transform;

            Kaideu.UI.UIHandler.Instance.ShowUI("MainMenu");
            EventManager.Instance.TriggerEvent(Events.MainMenu, null);
            hinted = false;

            //EventManager.Instance.TriggerEvent(Events.UI, )
            //Reset Level numbers, positions, camera, etc as needed
        }
    }

    private void StartLevel(Dictionary<string, object> arg0)
    {
        InputManager.Instance.ToggleControls(true);
        InputManager.Instance.SwitchTo(InputManager.Instance.Controls.Player);
        _cmvc.Follow = Player.transform;
        _cmvc.LookAt = Player.transform;
        HUD.Instance.ToggleHint(false);

        hasEnded = false;
        //Camera, animations, etc
    }

    private void DisableHint(Dictionary<string, object> arg0)
    {
        HUD.Instance.ToggleHint(false);
    }

    private void Update()
    {
        if (!hinted && Player.transform.position.y < _minParachuteHeight)
        {
            HUD.Instance.ToggleHint(true);
            hinted = true;
        }
    }
}
