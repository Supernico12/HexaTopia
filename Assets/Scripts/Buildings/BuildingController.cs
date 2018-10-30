using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField]
    Building building;
    UIController controller;
    TileController currentTile;
    List<UnitController> housedUnits = new List<UnitController>();

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
        if (building.maxUnits > housedUnits.Count)
        {
            Unit selectedUnit = building.unitsToProduce[index];
            GameObject unit = new GameObject(selectedUnit.name);
            UnitController controller = unit.AddComponent<UnitController>();
            controller.SetUnit(selectedUnit);

            currentTile.tile.currentUnit = selectedUnit.type;

            unit.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
            MeshFilter filter = unit.AddComponent<MeshFilter>();
            filter.sharedMesh = selectedUnit.mesh;

            unit.transform.parent = currentTile.transform;
            unit.transform.position = currentTile.transform.position + new Vector3(0, 1, 0);
            unit.transform.localScale /= 2;
            housedUnits.Add(controller);
            DisFocus();
        }

    }

    void Start()
    {
        controller = UIController.instance;

    }
}
