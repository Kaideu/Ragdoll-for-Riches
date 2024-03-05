using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{

    public int _currentBalance = 0;
    public int _collectedBalance;
    [SerializeField]
    LayerMask obstacle;
    [SerializeField]
    LayerMask groundLayer;
    private void Awake() {
        _collectedBalance = 0;
    }
    public void UpdateMoney(int collectedMoney){
        _currentBalance += collectedMoney;
        Debug.Log(_currentBalance);
    }

    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == obstacle){
            _collectedBalance += 200;
            Debug.Log(_collectedBalance);
        }
    }

}
