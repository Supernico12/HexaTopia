using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BuildingSlot : Slot
{
    int index;

    ViligerController controller;
    TileManager tileManager;

    public void SetContent(ViligerController controller, Building building, int index)
    {
        base.SetContent(building.cost);
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();

        this.controller = controller;
        text.text = building.name;
        this.index = index;


    }
    public void OnTouch()
    {
        controller.CreateBuilding(index);
        tileManager.Diselect();


    }
    void Start()
    {
        tileManager = TileManager.instance;

    }
}
