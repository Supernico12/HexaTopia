using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{

    [SerializeField] int vertices = 4;


    Vector2[] points;


    //{1,0,-1,0} ,{0,-1,0,1});
    public int NumPoints
    {
        get
        {
            return points.Length;
        }
    }
    public Vector3 this[int i]
    {
        get
        {
            return new Vector3(points[i].x, 0, points[i].y);
        }
    }
    public Grid(Vector2 center, int columns, int rows)
    {
        points = new Vector2[columns * rows];
        for (int i = 0; i < columns; i++)
        {


            for (int n = 0; n < rows; n++)
            {
                points[i * rows + n].x = n + center.x;
                points[i * rows + n].y = i + center.y;
            }
        }
    }
    // Derecha , Abajo , Izquierda , Arriba

    //[SerializeField]

}
