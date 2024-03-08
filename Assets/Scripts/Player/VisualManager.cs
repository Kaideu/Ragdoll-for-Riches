using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : MonoBehaviour
{
    [SerializeField] private CustomizeMenu costumeMenu;
    [SerializeField] private CustomizeMenu hairMenu;
    [SerializeField] private CustomizeMenu headgearMenu;

    [SerializeField] private GameObject[] characterCostumes;
    [SerializeField] private GameObject[] characterHairs;
    [SerializeField] private GameObject[] characterHeadGears;

    // Start is called before the first frame update
    void Start()
    {
        if (costumeMenu) costumeMenu.OnItemChanged += CostumeMenu_OnItemChanged;
        if (hairMenu) hairMenu.OnItemChanged += HairMenu_OnItemChanged;
        if (headgearMenu) headgearMenu.OnItemChanged += HeadgearMenu_OnItemChanged;
    }

    private void HeadgearMenu_OnItemChanged(Item obj)
    {
        if (characterCostumes.Length == 0) return;
        foreach (GameObject costume in characterCostumes)
        {
            if (costume.name.Equals(obj.name)) costume.SetActive(true);
            else costume.SetActive(false);
        }
    }

    private void HairMenu_OnItemChanged(Item obj)
    {
        if (characterCostumes.Length == 0) return;
        foreach (GameObject costume in characterCostumes)
        {
            if (costume.name.Equals(obj.name)) costume.SetActive(true);
            else costume.SetActive(false);
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

    // Update is called once per frame
    void Update()
    {

    }
}
