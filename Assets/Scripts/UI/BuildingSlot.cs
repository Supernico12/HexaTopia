using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BuildingSlot : MonoBehaviour
{
    int index;
    TextMeshProUGUI text;
    ViligerController controller;

    public void SetContent(ViligerController controller, Building building, int index)
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        this.controller = controller;
        text.text = building.name;
        this.index = index;
    }
    public void OnTouch()
    {
        controller.CreateBuilding(index);
    }
}
