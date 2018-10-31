using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViligerController : UnitController
{
    UIController controller;


    public Building[] constructableBuildings;


    public override void OnSelected()
    {
        base.OnSelected();
        controller.SetButtonsCreateBuilding(constructableBuildings, this);
    }

    public override void OnDiselected()
    {
        base.OnDiselected();
        controller.CloseUnitMenu();
    }

    public override void Start()
    {
        base.Start();
        controller = UIController.instance;

    }
    public void CreateBuilding(int index)
    {
        //If I hAD eNOUGHT mONEY
        if (currentTile.tile.currentBuilding == Buildings.None)
        {
            Building selectedBuilding = constructableBuildings[index];

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

        }

    }
}
