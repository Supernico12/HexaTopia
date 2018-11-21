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

    PlayersManager playersManager;
    Resource myPlayerResources
    {
        get { return playersManager.GetPlayerResources(((int)myTeam)); }
        //get { return playersManager.playerBank[(int)building.teams]; }
    }

    public void OnFocus(TileController tile)
    {
        if (playersManager.GetTurn == (int)myTeam)
        {
            currentTile = tile;
            uIController.SetButtonsCreateUnit(building.unitsToProduce, this);
        }

    }
    public void SetBuilding(Building building)
    {
        this.building = building;
    }

    public void DisFocus()
    {
        uIController.CloseUnitMenu();
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
                Debug.Log(myPlayerResources.wood);
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
