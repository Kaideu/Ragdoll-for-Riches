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
    [SerializeField]
    GameObject _hintPanel;

    public void UpdateCollected(int amount) => _collectedBalance.text = $"Current Claim: {amount}";

    public void ToggleHint(bool isOn) => _hintPanel.SetActive(isOn);


}
