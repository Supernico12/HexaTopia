using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitSlot : MonoBehaviour
{


    int index;
    TextMeshProUGUI text;
    BuildingController controller;

    public void SetContent(Unit unit, BuildingController control, int index)
    {

        //text.text = unit.name;

        controller = control;
        this.index = index;
    }

    public void OnTouch()
    {
        controller.CreateUnit(index);
    }

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

}
