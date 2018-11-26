using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    TileManager manager;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
        float distance = Vector3.Distance(dragOrigin, Input.mousePosition);


        if (distance > 3)
        {
            Vector3 move = new Vector3(pos.x, 0, pos.y);
            move *= dragSpeed;


            transform.Translate(move, Space.World);
            dragOrigin = Input.mousePosition;
            manager.hasDragged = true;
        }
    }

    void Start()
    {
        manager = TileManager.instance;
    }


}