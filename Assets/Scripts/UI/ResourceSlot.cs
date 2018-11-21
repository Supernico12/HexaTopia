using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = " Get resource ";

    }

    public virtual void OnTouch()
    {
        //Vil end Turn
        // Add Player Resources 

        manager.GetPlayerResourcesbyTurn().AddResources(resource);
        UIController.instance.OnResourcesChanged();

    }
}
