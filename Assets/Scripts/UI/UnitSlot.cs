using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitSlot : MonoBehaviour
{


    int index;
    [SerializeField]
    TextMeshProUGUI text;
    BuildingController controller;

    public virtual void SetContent(Unit unit, BuildingController control, int index)
    {


        text = GetComponentInChildren<TextMeshProUGUI>();
        controller = control;
        this.index = index;
        text.text = unit.name;
    }

    public virtual void OnTouch()
    {
        controller.CreateUnit(index);
    }




}
