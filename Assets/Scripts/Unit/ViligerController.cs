using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViligerController : UnitController
{
    UIController controller;
    [SerializeField] Villager vil;


    public override void OnSelected()
    {
        base.OnSelected();
        controller.SetButtonsCreateBuilding(vil.constructableBuildings, this);
    }

    public override void OnDiselected()
    {
        base.OnDiselected();
    }

    public override void Start()
    {
        base.Start();
        controller = UIController.instance;
    }
}
