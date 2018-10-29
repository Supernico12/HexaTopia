using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] Unit unit;

    TileManager manager;
    TileController currentTile;


    List<TileController> possibleTiles = new List<TileController>();
    // Deecha , Abajo , Izquierda , Arriba
    public void SetController(TileController controller)
    {
        currentTile = controller;
    }
    public void OnSelected()
    {

        foreach (Vector2 movement in unit.Movements)
        {
            TileController posTile = manager.TilesCalculator(currentTile.tile.index, movement);
            if (posTile != null)
            {
                Material mat = posTile.GetComponent<Renderer>().material;
                mat.color = Color.red;
                possibleTiles.Add(posTile);


            }
        }
    }

    public void OnDiselected()
    {
        foreach (TileController markedTile in possibleTiles)
        {
            Material mat = markedTile.GetComponent<Renderer>().material;
            mat.color = Color.white;
        }
        possibleTiles.Clear();
    }

    public void OnMove(TileController target)
    {
        foreach (TileController possibility in possibleTiles)
        {
            if (target.tile.index == possibility.tile.index)
            {
                if (target.tile.currentUnit == UnitFlags.None)
                {
                    Move(target);
                }
                else
                {
                    Attack();
                }

            }

        }

    }

    void Move(TileController newController)
    {

        //Previous Tile
        currentTile.tile.currentUnit = UnitFlags.None;

        //New Tile
        currentTile = newController;

        transform.parent = currentTile.transform;
        transform.position = currentTile.transform.position + new Vector3(0, 1, 0);

        currentTile.tile.currentUnit = unit.type;
    }
    void Start()
    {
        manager = TileManager.instance;
    }
    void Attack()
    {

    }

}
