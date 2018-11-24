using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMotorController : MonoBehaviour
{
    int columns = 5;
    int rows = 5;

    TileController[] tiles;

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


    public void OnUnitSelect(UnitController controller)
    {
        foreach (Vector2 movement in controller.unit.Movements)
        {
            int index = TilesCalculator(controller.currentTile.tile.index, movement);

            CheckEnums(tiles[index]);
        }
    }



    public void CheckEnums(TileController tile)
    {
        // Attack
        if (tile.tile.currentUnit != UnitFlags.None)
        {
            Material mat = tile.GetComponent<Renderer>().material;
            mat.color = Color.red;
        }
        else
        {
            Material mat = tile.GetComponent<Renderer>().material;
            mat.color = Color.green;

        }



    }
}
