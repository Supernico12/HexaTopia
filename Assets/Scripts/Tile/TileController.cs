using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public TileManager manager;
    public Tile tile;


    bool unitSelected;


    void OnMouseDown()
    {
        //Debug.Log(name);
        manager.OnFocus(this);



    }

    void Start()
    {
        this.gameObject.AddComponent<BoxCollider>();
        manager = TileManager.instance;
        tile.name = gameObject.name;
    }
}
