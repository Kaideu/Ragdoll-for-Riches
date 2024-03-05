using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : Kaideu.Utils.SingletonPattern<MoneyManager>
{

    public int _currentBalance = 0;
    public int _collectedBalance;
    [SerializeField]
    LayerMask obstacle;
    [SerializeField]
    LayerMask groundLayer;

    private void Start()
    {
        _collectedBalance = 0;
    }

    public void UpdateMoney(int collectedMoney){
        _currentBalance += collectedMoney;
        Debug.Log(_currentBalance);
    }

    
    void OnTriggerEnter(Collider other)
    {
        if(Kaideu.Utils.Helpers.IsInLayerMask(obstacle, other.gameObject.layer)){
            _collectedBalance += 200;
            Debug.Log(_collectedBalance);
        }
    }

}
