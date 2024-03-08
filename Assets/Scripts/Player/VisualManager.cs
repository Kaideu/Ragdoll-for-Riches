using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : MonoBehaviour
{
    [SerializeField] private CustomizeMenu costumeMenu;
    [SerializeField] private CustomizeMenu hairMenu;
    [SerializeField] private CustomizeMenu headgearMenu;
    [SerializeField] private CustomizeMenu beardMenu;

    [SerializeField] private GameObject[] characterCostumes;
    [SerializeField] private GameObject[] characterHairs;
    [SerializeField] private GameObject[] characterHeadGears;
    [SerializeField] private GameObject[] characterBeards;

    // Start is called before the first frame update
    void Start()
    {
        if (costumeMenu) costumeMenu.OnItemChanged += CostumeMenu_OnItemChanged;
        if (hairMenu) hairMenu.OnItemChanged += HairMenu_OnItemChanged;
        if (headgearMenu) headgearMenu.OnItemChanged += HeadgearMenu_OnItemChanged;
        if (beardMenu) beardMenu.OnItemChanged += BeardMenu_OnItemChanged;
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
}
