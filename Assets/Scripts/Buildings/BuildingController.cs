using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField]
    Building building;
    UIController uIController;
    TileController currentTile;
    //List<UnitController> housedUnits = new List<UnitController>();
    float housedAmount;
    public Teams myTeam;
    bool isFocused;

    PlayersManager playersManager;
    Resource myPlayerResources
    {
        get { return playersManager.GetPlayerResources(((int)myTeam)); }
        //get { return playersManager.playerBank[(int)building.teams]; }
    }

    public void OnFocus(TileController tile)
    {
        currentTile = tile;
        if (playersManager.GetTurn == (int)myTeam && currentTile.tile.currentUnit == UnitFlags.None && !isFocused)
        {
            uIController.SetButtonsCreateUnit(building.unitsToProduce, this);
            isFocused = true;

        }
        else
        {
            uIController.SetBuildingDescription(building, housedAmount);
            isFocused = false;
        }

    }
    public void SetBuilding(Building building)
    {
        this.building = building;
    }

    public void DisFocus()
    {
        uIController.CloseUnitMenu();
        uIController.CloseUnitDescriptions();
        isFocused = false;

    }

    public void CreateUnit(int index)
    {
        if (building.maxUnits > housedAmount)
        {
            Unit selectedUnit = building.unitsToProduce[index];
            // This line is usless -->>   line

            if (myPlayerResources.HasResources(selectedUnit.cost))
            {
                myPlayerResources.RemoveResources(selectedUnit.cost);
                uIController.OnResourcesChanged();

                GameObject unit = new GameObject(selectedUnit.name);
                HealthUI healthtxt = unit.AddComponent<HealthUI>();
                healthtxt.CreateText(building.healthUI);
                UnitController controller;
                if (unit.name == "Villager")
                {
                    controller = unit.AddComponent<ViligerController>();
                }
                else
                {
                    controller = unit.AddComponent<UnitController>();
                }
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
                controller.SetStats(new CharacterStats(selectedUnit, controller));
                playersManager.AddUnit(controller);

                DisFocus();
            }
        }

    }

    public void RemoveUnit()
    {
        housedAmount--;
    }
    void Start()
    {
        uIController = UIController.instance;
        playersManager = PlayersManager.instance;

    }
}
