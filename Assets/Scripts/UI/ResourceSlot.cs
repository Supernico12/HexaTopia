using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceSlot : Slot
{

    ViligerController vilController;
    ResourceController resource;
    PlayersManager manager;
    TileManager tileManager;

    public void SetContent(ViligerController cont, ResourceController res, PlayersManager man)
    {
        base.SetContent(res.resourceToAdd);
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
        manager.GetPlayerResourcesbyTurn().AddResources(resource.resourceToAdd);
        UIController.instance.OnResourcesChanged();
        tileManager.Diselect();
        resource.ReduceLifeTime();

    }
    void Start()
    {
        tileManager = TileManager.instance;
    }
}
