using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building", fileName = "New Building")]
public class Building : ScriptableObject
{
    new public string name;
    public Unit[] unitsToProduce;
    public Buildings type;
    public float cost;
    public int maxUnits;
    public Teams teams;
    public Mesh mesh;
    public GameObject healthUI;






}
