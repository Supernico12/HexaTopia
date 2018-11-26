using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViligerController : UnitController
{



    //public Building[] constructableBuildings;


    Resource myPlayerResources
    {
        get { return playersManager.GetPlayerResources(((int)myTeam)); }
        //get { return playersManager.playerBank[(int)building.teams]; }
    }

    public override void OnSelected()
    {
        base.OnSelected();
        if (!hasAttack)
            if (playersManager.GetTurn == (int)myTeam)
            {
                if (currentTile.tile.currentResource != ResourceType.None)
                {
                    ResourceController resController = currentTile.GetComponentInChildren<ResourceController>();

                    uIController.SetButtonResource(this, resController);

                }
                else
                {
                    uIController.SetButtonsCreateBuilding(unit.constructableBuildings, this);
                }
            }
    }

    public override void OnDiselected()
    {
        base.OnDiselected();
        uIController.CloseUnitMenu();

    }

    public override void Start()
    {
        base.Start();
        manager = TileManager.instance;
        uIController = UIController.instance;
        playersManager = PlayersManager.instance;
        myStats = new CharacterStats(unit.damage, unit.defence, unit.health, this);
        //playersManager.AddUnit(this, (int)myTeam);


    }
    public void CreateBuilding(int index)
    {
        Building selectedBuilding = unit.constructableBuildings[index];
        //If I hAD eNOUGHT mONEY



        if (currentTile.tile.currentBuilding == Buildings.None)
        {
            if (myPlayerResources.HasResources(selectedBuilding.cost))
            {
                myPlayerResources.RemoveResources(selectedBuilding.cost);
                uIController.OnResourcesChanged();
                currentTile.tile.currentBuilding = selectedBuilding.type;
                GameObject building = new GameObject(selectedBuilding.name);
                BuildingController controller = building.AddComponent<BuildingController>();
                controller.myTeam = myTeam;
                controller.SetBuilding(selectedBuilding);
                Material mat = gameObject.GetComponent<MeshRenderer>().material;
                building.AddComponent<MeshRenderer>().sharedMaterial = mat;

                MeshFilter filter = building.AddComponent<MeshFilter>();
                filter.sharedMesh = selectedBuilding.mesh;
                building.transform.parent = currentTile.transform;
                building.transform.position = currentTile.transform.position + new Vector3(0, 0.5f, 0);
                building.transform.localScale /= 2;

                SetCantMove();


            }
        }

    }
}
