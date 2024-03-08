using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaideu.UI;
using TMPro;
using System;

public class HUD : Kaideu.Utils.SingletonPattern<HUD>
{
    [SerializeField]
    TextMeshProUGUI _collectedBalance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void UpdateCollected(int amount)
    {
        _collectedBalance.text = $"Current Claim: {amount}";
    }
}
