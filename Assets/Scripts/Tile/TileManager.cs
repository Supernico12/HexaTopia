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
    UnitController currentSelectedUnit;
    bool isFocused;
    bool isUnitSelected;
    int columns = 5;
    int rows = 5;




    public void OnFocus(TileController tileController)
    {

        //Previous Tile
        if (currentController != null)
        {
            Material oldmat = currentController.GetComponent<Renderer>().material;
            oldmat.color = Color.white;
        }


        //New Tile
        if (isUnitSelected)
        {

            isUnitSelected = false;

            currentSelectedUnit.OnMove(tileController);
            currentSelectedUnit.OnDiselected();
            isFocused = false;
            return;

        }
        if (!isFocused)
        {


            currentController = tileController;

            CheckEnums();

            Material mat = currentController.GetComponent<Renderer>().material;
            mat.color = Color.green;
        }

        //GetComponent<Renderer>().material = mat;

    }



    void CheckEnums()
    {
        if (currentTile.currentUnit != UnitFlags.None)
        {
            Debug.Log(currentTile.name + " Has a: " + currentTile.currentUnit);
            currentSelectedUnit = currentController.GetComponentInChildren<UnitController>();
            currentSelectedUnit.SetController(currentController);
            currentSelectedUnit.OnSelected();
            isFocused = true;
            isUnitSelected = true;

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

    public void SetMarkedTiles()
    {

    }


    void Start()
    {
        tiles = tilesParent.GetComponentsInChildren<TileController>();
        rows = creator.rows;
        columns = creator.columns;

    }
}
