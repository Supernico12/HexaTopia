using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField]
    Building building;
    UIController controller;
    TileController currentTile;

    public void OnFocus(TileController tile)
    {
        currentTile = tile;
        controller.SetButtonsCreateUnit(building.unitsToProduce, this);

    }

    public void DisFocus()
    {
        controller.CloseUnitMenu();
    }

    public void CreateUnit(int index)
    {
        Unit selectedUnit = building.unitsToProduce[index];
        GameObject unit = new GameObject(selectedUnit.name);
        UnitController controller = unit.AddComponent<UnitController>();
        controller.SetUnit(selectedUnit);

        currentTile.tile.currentUnit = selectedUnit.type;
        unit.transform.parent = currentTile.transform;
        unit.transform.position = currentTile.transform.position;

    }

    void Start()
    {
        controller = UIController.instance;

    }
}
