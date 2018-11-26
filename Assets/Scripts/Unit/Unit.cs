using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Unit/Solider", fileName = "New Unit")]
public class Unit : ScriptableObject
{
    public new string name = "New Unit";
    public float health = 10;
    public float damage = 1;
    public float defence = 1;
    public int range = 1;
    public int movementSpeed = 1;
    public Resource cost;
    public Mesh mesh;
    public UnitFlags type = UnitFlags.None;
    public Vector2[] Movements = { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, -1),
    new Vector2(0, 1) , new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1) };

    public Building[] constructableBuildings;



}
public enum Teams { Player1, Player2, Player3, Player4 };
