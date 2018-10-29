using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building", fileName = "New Building")]
public class Building : ScriptableObject
{

    public Unit[] unitsToProduce;
    public Buildings type;
    public float cost;




    public void OnSelected()
    {


    }


}
