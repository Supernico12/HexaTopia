using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapCreator))]
public class GridEditor : Editor
{

    Grid grid
    {
        get
        {
            return creator.grid;
        }
    }
    MapCreator creator;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();
        if (GUILayout.Button("Create new"))
        {
            Undo.RecordObject(creator, "Create new");
            creator.CreateGrid();
        }

        if (GUILayout.Button("Create Mesh"))
        {
            Undo.RecordObject(creator, "Create new Mesh");
            creator.CreateMesh();
        }


    }

    void Draw()
    {
        for (int i = 0; i < grid.NumPoints; i++)
        {
            //Handles.DrawSolidDisc(grid[i],Vector3.up,1);
            Handles.DrawWireCube(grid[i], new Vector3(1, 1, 1));
            //Handles.CircleCap
        }


    }
    void OnSceneGUI()
    {
        //Input();
        Draw();
    }
    void OnEnable()
    {

        creator = (MapCreator)target;
        if (creator.grid == null)
        {

            creator.CreateGrid();

        }
        Debug.Log(creator.transform.name);
    }

}
