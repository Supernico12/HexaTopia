using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceSlot : Slot
{

    ViligerController vilController;
    Resource resource;
    PlayersManager manager;
    TileManager tileManager;

    public void SetContent(ViligerController cont, Resource res, PlayersManager man)
    {
        base.SetContent(res);
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

        vilController.SetCantMove();
        manager.GetPlayerResourcesbyTurn().AddResources(resource);
        UIController.instance.OnResourcesChanged();
        tileManager.Diselect();

    }
    void Start()
    {
        tileManager = TileManager.instance;
    }
}
