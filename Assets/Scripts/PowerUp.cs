using System.Collections;
using System.Collections.Generic;
using Kaideu.Physics;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Powers{Slow, ClaimMultiply, SpawnMultiply}
    public Powers powers;
    [SerializeField]
    LayerMask player;
    [SerializeField]
    PhysicsManager ps;
    private void Start() {
        ps = LevelManager.Instance.Player.GetComponent<PhysicsManager>();
    }
    public void PowerUpSwitch(Powers power){
        switch(power){
            case Powers.Slow:
            SlowObjects();
            break;
            case Powers.ClaimMultiply:
            ClaimDouble();
            break;
            case Powers.SpawnMultiply:
            SpawnDouble();
            break;
        }
    }

    public void SlowObjects(){
        ps.SetTerminalVelocity(ps.MaxTV/2);
        
        Debug.Log(powers);
        StartCoroutine(PowerUpSpawner.Instance.CountSec(powers));
    }
    public void ClaimDouble(){
        MoneyManager.Instance.Multiplyer = 2;
        Debug.Log(powers);
        StartCoroutine(PowerUpSpawner.Instance.CountSec(powers));
    }
    public void SpawnDouble(){
        ObjectSpawner.Instance.Multiplyer = 2;
        Debug.Log(powers);
        StartCoroutine(PowerUpSpawner.Instance.CountSec(powers));
    }

    private void OnTriggerEnter(Collider other){
        if (Kaideu.Utils.Helpers.IsInLayerMask(player, other.gameObject.layer)){
            PowerUpSwitch(powers);
        }
        else{
            Debug.LogError("Error On Impact");
        }
        Destroy(gameObject);
    }

}
