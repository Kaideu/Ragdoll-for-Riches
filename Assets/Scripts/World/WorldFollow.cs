using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.Events;
using Unity.VisualScripting;

public class WorldFollow : MonoBehaviour
{
    
    [SerializeField]
    Transform Target;
    [SerializeField]
    private Vector3 WorldPosition;
    [SerializeField]
    float offsetx = 0;
    [SerializeField]
    float offsety = 0;
    [SerializeField]
    float offsetz = 0;
    [SerializeField]
    Transform StartPos;

    
    void OnEnable()
    {  
        EventManager.Instance.StartListening(Events.MainMenu, SetLevPos);
    }
    void OnDisable()
    {
        EventManager.Instance.StopListening(Events.MainMenu, SetLevPos);
    }
    public void SetLevPos(Dictionary<string, object> arg){
        gameObject.transform.position = new Vector3(StartPos.position.x, StartPos.position.y -offsety, StartPos.position.z);
        Debug.Log("Im Working" + transform.position);
    }

    public void FollowTarget(){
        WorldPosition = new Vector3(LevelManager.Instance.Player.transform.position.x + offsetx, LevelManager.Instance.Player.transform.position.y - offsety, LevelManager.Instance.Player.transform.position.z + offsetz);
        gameObject.transform.position = WorldPosition;
    }
    void Update()
    {
        if(gameObject.transform.position.y > 0)
        {
            FollowTarget();
        }
    }
}

