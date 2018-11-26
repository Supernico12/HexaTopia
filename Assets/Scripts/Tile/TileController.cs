using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileController : MonoBehaviour
{
    public TileManager manager;
    public Tile tile;





    void OnMouseUp()
    {
        //Debug.Log(name);
        if (!IsPointerOverGameObject())
            manager.OnFocus(this);



    }
    public static bool IsPointerOverGameObject()
    {
        //check mouse
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        if (EventSystem.current.IsPointerOverGameObject(0))
            return true;

        //check touch
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                return true;
        }

        return false;
    }

    void Start()
    {
        this.gameObject.AddComponent<BoxCollider>();
        manager = TileManager.instance;
        tile.name = gameObject.name;
    }
}
