using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VisualManager : MonoBehaviour
{
    [Serializable]
    public class PlayerVisuals
    {
        [SerializeField]
        public int costume = 0;
        [SerializeField]
        public int hair = 0;
        [SerializeField]
        public int headgear = 0;
        [SerializeField]
        public int beard = 0;
    }

    [SerializeField] private GameObject[] characterCostumes;
    [SerializeField] private GameObject[] characterHairs;
    [SerializeField] private GameObject[] characterHeadGears;
    [SerializeField] private GameObject[] characterBeards;

    Dictionary<CustomizeMenu.ListFor, CustomizeMenu> menuDict;

    PlayerVisuals _myVisuals = new();
    public PlayerVisuals MyVisuals => _myVisuals;

    private void Awake()
    {
        menuDict = new Dictionary<CustomizeMenu.ListFor, CustomizeMenu>() {
            { CustomizeMenu.ListFor.Costume, null },
            { CustomizeMenu.ListFor.Hair, null },
            { CustomizeMenu.ListFor.Headgear, null },
            { CustomizeMenu.ListFor.Beard, null } };
    }

    private void OnEnable()
    {
        Kaideu.Events.EventManager.Instance.StartListening(Kaideu.Events.Events.MainMenu, ResetVisuals);
    }

    private void OnDisable()
    {
        Kaideu.Events.EventManager.Instance.StopListening(Kaideu.Events.Events.MainMenu, ResetVisuals);
    }

    public void SetMenu(CustomizeMenu.ListFor menu, CustomizeMenu cm)
    {
        menuDict[menu] = cm;
        switch (menu)
        {
            case CustomizeMenu.ListFor.Costume:
                menuDict[menu].OnItemChanged += CostumeMenu_OnItemChanged;
                break;
            case CustomizeMenu.ListFor.Hair:
                menuDict[menu].OnItemChanged += HairMenu_OnItemChanged;
                break;
            case CustomizeMenu.ListFor.Headgear:
                menuDict[menu].OnItemChanged += HeadgearMenu_OnItemChanged;
                break;
            case CustomizeMenu.ListFor.Beard:
                menuDict[menu].OnItemChanged += BeardMenu_OnItemChanged;
                break;
        }

    }
    private void BeardMenu_OnItemChanged(Item obj)
    {
        if (characterBeards.Length == 0) return;
        foreach (GameObject beard in characterBeards)
        {
            if (beard.name.Equals(obj.name)) beard.SetActive(true);
            else beard.SetActive(false);
        }
    }

    private void HeadgearMenu_OnItemChanged(Item obj)
    {
        if (characterHeadGears.Length == 0) return;
        foreach (GameObject headGear in characterHeadGears)
        {
            if (headGear.name.Equals(obj.name)) headGear.SetActive(true);
            else headGear.SetActive(false);
        }
    }

    private void HairMenu_OnItemChanged(Item obj)
    {
        if (characterHairs.Length == 0) return;
        foreach (GameObject hair in characterHairs)
        {
            if (hair.name.Equals(obj.name)) hair.SetActive(true);
            else hair.SetActive(false);
        }
    }

    private void CostumeMenu_OnItemChanged(Item obj)
    {
        if (characterCostumes.Length == 0) return;
        foreach (GameObject costume in characterCostumes)
        {
            if (costume.name.Equals(obj.name)) costume.SetActive(true);
            else costume.SetActive(false);
        }
    }

    internal void SetVisual(CustomizeMenu.ListFor itemType, int currItemIndex)
    {
        switch (itemType)
        {
            case CustomizeMenu.ListFor.Costume:
                _myVisuals.costume = currItemIndex;
                break;
            case CustomizeMenu.ListFor.Hair:
                _myVisuals.hair = currItemIndex;
                break;
            case CustomizeMenu.ListFor.Headgear:
                _myVisuals.headgear = currItemIndex;
                break;
            case CustomizeMenu.ListFor.Beard:
                _myVisuals.beard = currItemIndex;
                break;
        }
    }

    private void ResetVisuals(Dictionary<string, object> arg0)
    {
        print("Reset Visuals");
        for (int i = 0; i < characterCostumes.Length; i++)
        {
            var item = characterCostumes[i];

            if (i == _myVisuals.costume)
                item.SetActive(true);
            else item.SetActive(false);
        }

        for (int i = 0; i < characterHairs.Length; i++)
        {
            var item = characterHairs[i];

            if (i == _myVisuals.hair)
                item.SetActive(true);
            else item.SetActive(false);
        }

        for (int i = 0; i < characterHeadGears.Length; i++)
        {
            var item = characterHeadGears[i];

            if (i == _myVisuals.headgear)
                item.SetActive(true);
            else item.SetActive(false);
        }

        for (int i = 0; i < characterBeards.Length; i++)
        {
            var item = characterBeards[i];

            if (i == _myVisuals.beard)
                item.SetActive(true);
            else item.SetActive(false);
        }
    }

}
