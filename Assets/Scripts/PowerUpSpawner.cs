using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Input;
using Unity.VisualScripting;
using Kaideu.Physics;

public class PowerUpSpawner : Kaideu.Utils.SingletonPattern<PowerUpSpawner>
{
    [SerializeField]
    bool debugging = false;

    [SerializeField]
    GameObject[] _objs;
    [SerializeField]
    float _verticalSpawnDistance = 100;
    [SerializeField]
    Vector2 _horizontalSpawnDistRange = new(0, 10);
    [SerializeField]
    Vector2 _spawnTimeRange = new(1, 3);
    Transform playerRef => LevelManager.Instance.Player.transform;
    [SerializeField]
    PhysicsManager ps;

    float _nextSpawnTime;


    private void Start() {
        ps = LevelManager.Instance.Player.GetComponent<PhysicsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        var canSpawn = !LevelManager.Instance.HasEnded && Time.time >= _nextSpawnTime && playerRef.position.y > _verticalSpawnDistance * 2;

        if ((InputManager.Instance.Controls.Player.Interact.WasPressedThisFrame() && debugging)|| ( !debugging && canSpawn && _objs.Length > 0))
        {
            if(Random.Range(0, 50) == 28){
                SpawnObject();
                DetirmineNextSpawnTime();
            }
            
        }
    }

    void SpawnObject()
    {
        var newX = Random.Range(_horizontalSpawnDistRange.x, _horizontalSpawnDistRange.y);
        var newZ = Random.Range(_horizontalSpawnDistRange.x, _horizontalSpawnDistRange.y);

        var _obj = _objs[Random.Range(0, _objs.Length)];
        Instantiate(_obj, playerRef.position - new Vector3(newX, 100, newZ), Quaternion.identity);
    }

    void DetirmineNextSpawnTime() => _nextSpawnTime = Time.time + Random.Range(_spawnTimeRange.x, _spawnTimeRange.y);

    public IEnumerator CountSec(PowerUp.Powers powers){
        yield return new WaitForSeconds(5);
        switch(powers){
            case PowerUp.Powers.Slow:
            ps.ResetTerminalVelocity();
            break;
            case PowerUp.Powers.ClaimMultiply:
            MoneyManager.Instance.Multiplyer = 0;
            break;
            case PowerUp.Powers.SpawnMultiply:
            ObjectSpawner.Instance.Multiplyer = 0;
            break;
        }
        

    }

}
