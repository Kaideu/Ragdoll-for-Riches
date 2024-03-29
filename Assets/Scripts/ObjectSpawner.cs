using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Input;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    bool debugging = false;

    [SerializeField]
    Transform playerRef;
    [SerializeField]
    GameObject[] _objs;
    [SerializeField]
    float _verticalSpawnDistance = 100;
    [SerializeField]
    Vector2 _horizontalSpawnDistRange = new(0, 10);
    [SerializeField]
    Vector2 _spawnTimeRange = new(1, 3);

    float _nextSpawnTime;


    // Update is called once per frame
    void Update()
    {
        var canSpawn = Time.time >= _nextSpawnTime && playerRef.position.y > _verticalSpawnDistance * 2;

        if ((InputManager.Instance.Controls.Player.Space.WasPressedThisFrame() && debugging)|| ( !debugging && canSpawn && _objs.Length > 0))
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
