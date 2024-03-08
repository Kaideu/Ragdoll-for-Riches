using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public string name;
    public int value;
    public bool purchased = false;
    public bool equipped = false;

    public void Buy()
    {
        if (!purchased) purchased = true;
    }

    public void Equip()
    {
        if (!purchased) return;
        if (!equipped) equipped = true;
    }

    public void UnEquip()
    {
        if (!equipped) equipped = false;
    }
}
public class CustomizeMenu : MonoBehaviour
{
    [SerializeField] List<Item> items = new List<Item>();
    [SerializeField] private Button RightBtn;
    [SerializeField] private Button LeftBtn;
    [SerializeField] private Button BuyBtn;
    [SerializeField] private Button EquipBtn;
    [SerializeField] public int currItemIndex;

    public enum ListFor { Costume, Hair, Headgear }
    public ListFor listFor;
    public event Action<Item> OnItemChanged;

    // Start is called before the first frame update
    void Start()
    {
        currItemIndex = 0;
        LeftBtn.onClick.AddListener(() => PrevItem());
        RightBtn.onClick.AddListener(() => NextItem());
    }

    void NextItem()
    {
        if (items.Count == 0) { Debug.LogWarning("Item List Empty"); return; }
        RightBtn.gameObject.SetActive(false);
        if (currItemIndex < items.Count - 1) currItemIndex++;
        else currItemIndex = 0;
        OnItemChanged?.Invoke(items[currItemIndex]);
        RightBtn.gameObject.SetActive(true);
    }
    void PrevItem()
    {
        if (items.Count == 0) { Debug.LogWarning("Items List Empty"); return; }
        LeftBtn.gameObject.SetActive(false);
        if (currItemIndex > 0) currItemIndex--;
        else currItemIndex = items.Count - 1;
        LeftBtn.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
