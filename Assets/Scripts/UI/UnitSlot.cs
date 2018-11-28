using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnitSlot : Slot
{


    int index;
    [SerializeField]
    TextMeshProUGUI text;
    BuildingController controller;
    UIController uIController;
    Unit unit;
    Button info;

    public void SetContent(Unit unit, BuildingController control, int index)
    {
        base.SetContent(unit.cost);
        info = transform.Find("Info").GetComponent<Button>();
        info.onClick.AddListener(OnInfoButton);
        info.gameObject.SetActive(true);
        this.unit = unit;
        uIController = UIController.instance;

        text = GetComponentInChildren<TextMeshProUGUI>();
        controller = control;
        this.index = index;
        text.text = unit.name;
    }

    public virtual void OnTouch()
    {
        controller.CreateUnit(index);
    }

    public void OnInfoButton()
    {
        uIController.SetUnitsInFo(unit);
    }




}
