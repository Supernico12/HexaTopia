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
    Building building;

    public void SetContent(ViligerController controller, Building building, int index)
    {
        base.SetContent(building.cost);
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();

        this.controller = controller;
        text.text = building.name;
        this.index = index;
        this.building = building;

    }
    public void OnTouch()
    {
        controller.CreateBuilding(index);
        tileManager.Diselect();


    }
    public override void OnInfoButton()
    {
        UIController.instance.SetBuildingsInfo(building);
    }
    void Start()
    {
        tileManager = TileManager.instance;

    }
}
