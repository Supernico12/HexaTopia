using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{

    Building building;
    UIController controller;

    void OnFocus()
    {
        controller.SetButtonsCreateUnit(building.unitsToProduce);
    }


    void Start()
    {
        controller = UIController.instance;

    }
}
