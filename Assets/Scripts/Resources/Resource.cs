using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource
{



    public int food = 0;
    public int wood = 0;
    public int gold = 0;
    public ResourceType type;


    public Resource(int food = 0, int wood = 0, int gold = 0)
    {
        this.food = food;
        this.wood = wood;
        this.gold = gold;
    }

    public bool HasResources(Resource hisResources)
    {

        if (food - hisResources.food < 0)
        {
            return false;
        }
        if (wood - hisResources.wood < 0)
        {
            return false;
        }
        if (gold - hisResources.gold < 0)
        {
            return false;
        }

        return true;


    }

    public void AddResources(Resource resourcetoAdd)
    {
        food += resourcetoAdd.food;
        wood += resourcetoAdd.wood;
        gold += resourcetoAdd.gold;
    }

    public void RemoveResources(Resource resourcetoAdd)
    {
        food -= resourcetoAdd.food;
        wood -= resourcetoAdd.wood;
        gold -= resourcetoAdd.gold;


    }

    public int GetResourcesIcons()
    {

        if (food > 0)
        {
            return 0;
        }
        if (wood > 0)
        {
            return 1;
        }
        if (gold > 0)
        {
            return 2;
        }

        return -1;

    }
    public int GetResourceCost()
    {

        if (food > 0)
        {
            return food;
        }
        if (wood > 0)
        {
            return wood;
        }
        if (gold > 0)
        {
            return gold;
        }

        return -1;
    }

}
