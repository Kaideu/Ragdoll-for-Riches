using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Input;

public class ObjectSpawner : Kaideu.Utils.SingletonPattern<ObjectSpawner>
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

    float _nextSpawnTime;
    public float Multiplyer = 1f;


    // Update is called once per frame
    void Update()
    {
        var canSpawn = !LevelManager.Instance.HasEnded && Time.time >= _nextSpawnTime/Multiplyer && playerRef.position.y > LevelManager.Instance.SpawnerStopHeight;

        if ((InputManager.Instance.Controls.Player.Interact.WasPressedThisFrame() && debugging)|| ( !debugging && canSpawn && _objs.Length > 0))
        {
            SpawnObject();
            DetirmineNextSpawnTime();
        }
    }

    void SpawnObject()
    {
        var newX = Random.Range(_horizontalSpawnDistRange.x, _horizontalSpawnDistRange.y);
        var newZ = Random.Range(_horizontalSpawnDistRange.x, _horizontalSpawnDistRange.y);

        var _obj = _objs[Random.Range(0, _objs.Length - 1)];
        Instantiate(_obj, playerRef.position - new Vector3(newX, 100, newZ), Quaternion.identity);
    }

    void DetirmineNextSpawnTime() => _nextSpawnTime = Time.time + Random.Range(_spawnTimeRange.x, _spawnTimeRange.y);
}
