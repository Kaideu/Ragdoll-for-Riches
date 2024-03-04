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
    Cinemachine.CinemachineVirtualCamera cmvc;

    //[Header("CinemachineSettings")]
    //[SerializeField]



    private GameObject _player;
    public GameObject Player
    {
        get { if (_player == null) _player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity); return _player; }
    }

    private void OnEnable()
    {
        EventManager.Instance.StartListening(Events.StartLevel, StartLevel);
        EventManager.Instance.StartListening(Events.EndLevel, EndLevel);
    }

    private void OnDisable()
    {
        EventManager.Instance.StopListening(Events.StartLevel, StartLevel);
        EventManager.Instance.StopListening(Events.EndLevel, EndLevel);
    }
    private void Start()
    {

        Player.transform.position = _startPosition.position;
        cmvc.Follow = Player.transform;
        cmvc.LookAt = Player.transform;

        Kaideu.UI.UIHandler.Instance.ShowUI("MainMenu");

    }

    private void EndLevel(Dictionary<string, object> arg0)
    {
        Debug.LogError("Level Ended");
        InputManager.Instance.SwitchTo(InputManager.Instance.Controls.UI);

        Player.transform.position = _startPosition.position;
        cmvc.Follow = Player.transform;
        cmvc.LookAt = Player.transform;


        Kaideu.UI.UIHandler.Instance.ShowUI("MainMenu");

        //EventManager.Instance.TriggerEvent(Events.UI, )
        //Reset Level numbers, positions, camera, etc as needed
    }

    private void StartLevel(Dictionary<string, object> arg0)
    {
        InputManager.Instance.ToggleControls(true);
        InputManager.Instance.SwitchTo(InputManager.Instance.Controls.Player);


        //Camera, animations, etc
    }
}
