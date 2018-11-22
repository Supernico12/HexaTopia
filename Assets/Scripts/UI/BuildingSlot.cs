using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BuildingSlot : MonoBehaviour
{
    int index;

    ViligerController controller;
    TileManager tileManager;

    public void SetContent(ViligerController controller, Building building, int index)
    {
        TextMeshProUGUI[] text = GetComponentsInChildren<TextMeshProUGUI>();
        this.controller = controller;
        text[0].text = building.name;
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
