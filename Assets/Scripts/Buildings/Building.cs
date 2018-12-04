using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building", fileName = "New Building")]
public class Building : ScriptableObject
{
    new public string name;
    public Unit[] unitsToProduce;
    public Buildings type;
    public string description;
    public int maxUnits;
    public Resource cost;

    public Mesh mesh;
    public GameObject healthUI;







}
