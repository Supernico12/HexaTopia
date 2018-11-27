using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitMotorController
{
    int columns = 5;
    int rows = 5;


    UnitController selectedUnit;
    TileController[] tiles;
    List<int> possibleTiles = new List<int>();


    public UnitMotorController(int rows, int columns, TileController[] tiles)
    {
        this.rows = rows;
        this.columns = columns;
        this.tiles = tiles;
    }


    public int TilesCalculator(int position, Vector2 movement)
    {
        int row = (position) / columns;
        int xrow = (position + (int)movement.x) / columns;

        if (row != xrow)
        {
            return -1;
        }
        int index = position + (int)movement.x + (columns * (int)movement.y);
        if (index < 0 || index > (rows * columns) - 1)
        {
            return -1;
        }

        return index;
    }

    public void CheckMove(UnitController unit)
    {
        ClearTiles();
        selectedUnit = unit;
        TileController starttile = unit.GetComponentInParent<TileController>();


        foreach (Vector2 mov in unit.unit.Movements)
        {
            int index = TilesCalculator(starttile.tile.index, mov);
            if (index > -1)
            {
                TileController tile = tiles[index];

                Material mat = tile.GetComponent<Renderer>().material;

                if (!CheckUnit(tile) && !unit.hasMoved)
                {
                    possibleTiles.Add(index);
                    mat.color = Color.green;
                }

            }
        }
        foreach (Vector2 mov in unit.unit.rangeMov)
        {
            int index = TilesCalculator(starttile.tile.index, mov);
            if (index > -1)
            {
                TileController tile = tiles[index];

                Material mat = tile.GetComponent<Renderer>().material;

                if (CheckUnit(tile))
                {
                    Teams targetTeam = tile.GetComponentInChildren<UnitController>().myTeam;
                    if (unit.myTeam != targetTeam)
                    {
                        possibleTiles.Add(index);
                        mat.color = Color.red;
                    }
                }

            }
        }
        if (possibleTiles.Count == 0)
        {
            if (unit.name != "Villager")
                unit.SetCantMove();
        }
    }
    public void ClearTiles()
    {
        foreach (int pos in possibleTiles)
        {

            Material mat = tiles[pos].GetComponent<Renderer>().material;
            mat.color = Color.white;
        }
        possibleTiles.Clear();
    }
    public void ActionUnit(TileController controller, UnitController unit)
    {


        //if(selectedUnit == unit)

        foreach (int pos in possibleTiles)
        {

            if (tiles[pos].tile.index == controller.tile.index)
            {

                ClearTiles();
                if (!CheckUnit(controller))
                {
                    unit.Move(controller);

                }
                else
                {
                    unit.Attack(controller);
                }
                return;
            }

        }
        ClearTiles();
    }



    bool CheckUnit(TileController controller)
    {
        if (controller.tile.currentUnit == UnitFlags.None)
        {
            return false;
        }
        return true;
    }
}




