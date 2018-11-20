using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSlot : MonoBehaviour
{

    ViligerController vilController;
    Resource resource;
    PlayersManager manager;

    public void SetContent(ViligerController cont, Resource res, PlayersManager man)
    {
        resource = res;
        vilController = cont;
        manager = man;
    }

    public virtual void OnTouch()
    {
        //Vil end Turn
        // Add Player Resources 
        manager.GetPlayerResourcesbyTurn().AddResources(resource);

    }
}
