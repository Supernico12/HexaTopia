using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{

    [HideInInspector]
    public Grid grid;
    public int columns = 10;
    public int rows = 10;
    [Range(2, 256)]
    [SerializeField] int resolution = 4;
    [SerializeField] float size = 0.5f;



    TerrainFace[] blocks;
    MeshFilter[] meshFiltrer;

    public void CreateGrid()
    {

        grid = new Grid(transform.position, columns, rows);
    }



    public void CreateMesh()
    {
        if (meshFiltrer == null || meshFiltrer.Length == 0)
        {
            meshFiltrer = new MeshFilter[columns * rows];
        }
        blocks = new TerrainFace[columns * rows];

        if (grid != null)
        {
            GameObject parent = new GameObject("Tiles");
            for (int i = 0; i < blocks.Length; i++)
            {
                if (meshFiltrer[i] == null)
                {
                    GameObject meshObj = new GameObject("Tile" + i);
                    meshObj.transform.parent = parent.transform;
                    meshObj.transform.position = grid[i] + Vector3.down;
                    TilesComponents(meshObj, i);

                    meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                    meshFiltrer[i] = meshObj.AddComponent<MeshFilter>();
                    meshFiltrer[i].sharedMesh = new Mesh();


                }
                blocks[i] = new TerrainFace(meshFiltrer[i].sharedMesh, resolution, Vector3.up);
            }

        }

        foreach (TerrainFace face in blocks)
        {
            face.MeshGenerator();

        }


    }

    public void TilesComponents(GameObject tile, int index)
    {

        tile.transform.localScale = new Vector3(size, size, size);
        TileController controller = tile.AddComponent<TileController>();
        controller.tile = new Tile(TilesTypes.Plain, index);





    }

}
