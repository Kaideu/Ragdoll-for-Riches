using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void Update()
    {
        if (!IsVisibleOnScreen(gameObject))
        {
            Destroy(gameObject, 2);
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
