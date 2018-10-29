using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
    public string name;
    public TilesTypes type;
    public Buildings currentBuilding;
    public UnitFlags currentUnit;
    public int index;


    public Tile(TilesTypes type, int index)
    {
        this.type = type;
        this.index = index;

    }
    public bool HasAttachFlag(UnitFlags flag)
    {
        return (currentUnit & flag) == flag;
    }

}
public enum Buildings { None, Barracks, ArcheryRange, Stable, TownCenter }
public enum TilesTypes { Plain, Forest, Mountain, Water }
public enum UnitFlags { None, Villiger, SwordsMan, Archer, Scout }
