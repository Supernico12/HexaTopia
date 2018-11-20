using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField]
    Building building;
    UIController controller;
    TileController currentTile;
    //List<UnitController> housedUnits = new List<UnitController>();
    float housedAmount;
    public Teams myTeam;

    public void OnFocus(TileController tile)
    {
        currentTile = tile;
        controller.SetButtonsCreateUnit(building.unitsToProduce, this);

    }
    public void SetBuilding(Building building)
    {
        this.building = building;
    }

    public void DisFocus()
    {
        controller.CloseUnitMenu();
    }

    public void CreateUnit(int index)
    {
        if (building.maxUnits > housedAmount)
        {
            Unit selectedUnit = building.unitsToProduce[index];
            // This line is usless -->>   line

            GameObject unit = new GameObject(selectedUnit.name);
            HealthUI healthtxt = unit.AddComponent<HealthUI>();
            healthtxt.CreateText(building.healthUI);
            UnitController controller = unit.AddComponent<UnitController>();
            controller.SetUnit(selectedUnit, this);
            controller.myTeam = myTeam;
            currentTile.tile.currentUnit = selectedUnit.type;
            Material mat = GetComponent<MeshRenderer>().material;
            unit.AddComponent<MeshRenderer>().sharedMaterial = mat;
            MeshFilter filter = unit.AddComponent<MeshFilter>();
            filter.sharedMesh = selectedUnit.mesh;


            unit.transform.parent = currentTile.transform;
            unit.transform.position = currentTile.transform.position + new Vector3(0, 1, 0);
            unit.transform.localScale /= 2;
            housedAmount++;
            controller.SetStats(new CharacterStats(selectedUnit.damage, selectedUnit.defence, selectedUnit.health, controller));

            DisFocus();
        }

    }

    public void RemoveUnit()
    {
        housedAmount--;
    }
    void Start()
    {
        controller = UIController.instance;

    }
}
