using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    #region Singleton



    public static TileManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    PlayersManager playersManager;
    [SerializeField] MapCreator creator;
    [SerializeField] Transform tilesParent;
    TileController[] tiles;
    TileController currentController;
    Tile currentTile
    {
        get
        {
            return currentController.tile;
        }
    }
    BuildingController currentSelectedBuilding;
    UnitController currentSelectedUnit;
    ResourceController currentSelectedResource;
    bool isFocused;
    bool isUnitSelected;
    bool isBuildingSelected;
    [SerializeField] int columns = 10;
    [SerializeField] int rows = 10;

    int timesClicked = 0;




    public void OnFocus(TileController tileController)
    {
        //Previous Tile

        if (isUnitSelected)
        {
            if (playersManager.GetTurn == (int)currentSelectedUnit.myTeam)
            {
                unitMotorController.ActionUnit(tileController, currentSelectedUnit);
            }
        }

        Diselect();


        if (tileController != currentController)
        {
            //New Tile


            if (!isFocused)
            {

                timesClicked = 0;
            }
            else { timesClicked = (timesClicked + 1) % 2; }
        }


        currentController = tileController;
        CheckEnums();
        Material mat = currentController.GetComponent<Renderer>().material;
        mat.color = Color.green;



        //GetComponent<Renderer>().material = mat;

    }

    public void Diselect()
    {
        if (currentController != null)
        {
            Material oldmat = currentController.GetComponent<Renderer>().material;
            oldmat.color = Color.white;
        }

        if (isUnitSelected)
        {

            isUnitSelected = false;

            isFocused = false;
            currentSelectedUnit.OnDiselected();
            unitMotorController.ClearTiles();

            return;
        }
        if (isBuildingSelected)
        {
            isBuildingSelected = false;
            currentSelectedBuilding.DisFocus();
        }
        if (currentSelectedResource != null)
        {
            currentSelectedResource.OnDisFocus();
            currentSelectedResource = null;
        }


    }



    void CheckEnums()
    {
        if (currentTile.currentUnit != UnitFlags.None && timesClicked == 0)
        {
            //Debug.Log(currentTile.name + " Has a: " + currentTile.currentUnit);
            currentSelectedUnit = currentController.GetComponentInChildren<UnitController>();
            currentSelectedUnit.SetController(currentController);
            currentSelectedUnit.OnSelected();

            isFocused = true;
            isUnitSelected = true;


        }
        else if (currentTile.currentBuilding != Buildings.None)
        {
            currentSelectedBuilding = currentController.GetComponentInChildren<BuildingController>();
            currentSelectedBuilding.OnFocus(currentController);
            isBuildingSelected = true;

        }
        else if (currentTile.currentResource != ResourceType.None)
        {
            currentSelectedResource = currentController.GetComponentInChildren<ResourceController>();
            currentSelectedResource.OnFocus();


        }


    }




    void OnTurnEnded()
    {
        Diselect();
        currentController = null;
    }

    public void SetMarkedTiles()
    {

    }


    void Start()
    {

        rows = creator.rows;
        columns = creator.columns;
        tiles = tilesParent.GetComponentsInChildren<TileController>();
        playersManager = PlayersManager.instance;
        playersManager.OnTurnEnded += OnTurnEnded;
        unitMotorController = new UnitMotorController(rows, columns, tiles);

    }


    #region UnitMotorController
    public UnitMotorController unitMotorController;
    #endregion
}
