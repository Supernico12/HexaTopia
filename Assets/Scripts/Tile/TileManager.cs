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
    int columns = 5;
    int rows = 5;

    int timesClicked = 0;




    public void OnFocus(TileController tileController)
    {
        if (tileController != currentController)
        {
            //New Tile
            if (isUnitSelected)
            {
                currentSelectedUnit.OnMove(tileController);
            }
            Diselect();
            if (!isFocused)
            {


                currentController = tileController;
                timesClicked = 0;
                CheckEnums();

                Material mat = currentController.GetComponent<Renderer>().material;
                mat.color = Color.green;
            }
        }
        else
        {
            Diselect();
            timesClicked = (timesClicked + 1) % 2;
            CheckEnums();
            Material mat = currentController.GetComponent<Renderer>().material;
            mat.color = Color.green;

        }

        //GetComponent<Renderer>().material = mat;

    }

    public void Diselect()
    {
        //Previous Tile
        if (currentController != null)
        {
            Material oldmat = currentController.GetComponent<Renderer>().material;
            oldmat.color = Color.white;
        }

        if (isUnitSelected)
        {

            isUnitSelected = false;


            currentSelectedUnit.OnDiselected();
            isFocused = false;
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
            Debug.Log(currentTile.name + " Has a: " + currentTile.currentUnit);
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


    public TileController TilesCalculator(int position, Vector2 movement)
    {
        int row = (position) / columns;
        int xrow = (position + (int)movement.x) / columns;

        if (row != xrow)
        {
            return null;
        }
        int index = position + (int)movement.x + (columns * (int)movement.y);
        if (index < 0 || index > (rows * columns) - 1)
        {
            return null;
        }
        return tiles[index];
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
        tiles = tilesParent.GetComponentsInChildren<TileController>();
        rows = creator.rows;
        columns = creator.columns;
        playersManager = PlayersManager.instance;
        playersManager.OnTurnEnded += OnTurnEnded;

    }
}
