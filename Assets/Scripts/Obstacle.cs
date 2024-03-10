using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{

    Rigidbody _rb;
    bool _destroying;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _rb.constraints = RigidbodyConstraints.FreezePositionY;
        _rb.angularDrag = 0;
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        var dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        print(dir);
        _rb.AddTorque(dir * 100, ForceMode.Impulse);
    }

    void Update()
    {
        if (!_destroying && !IsVisibleOnScreen(gameObject))
        {
            Destroy(gameObject, 2);
            _destroying = true;
        }
    }

    private bool IsVisibleOnScreen(GameObject target)
    {
        Camera mainCam = Camera.main;
        Vector3 targetScreenPoint = mainCam.WorldToScreenPoint(target.GetComponent<Renderer>().bounds.center);

        if ((targetScreenPoint.x < 0) || (targetScreenPoint.x > Screen.width) ||
                (targetScreenPoint.y < 0) || (targetScreenPoint.y > Screen.height))
        {
            return false;
        }

        if (targetScreenPoint.z < 0)
        {
            return false;
        }
        return true;
    }
}
