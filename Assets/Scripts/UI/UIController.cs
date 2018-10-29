using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    #region Singleton
    public static UIController instance;
    void Awake()
    {
        instance = this;
    }

    #endregion


    [SerializeField] Transform unitsButtonsParent;

    [SerializeField] GameObject buttonPrefab;
    List<UnitSlot> Unitbuttons = new List<UnitSlot>();

    public void SetButtonsCreateUnit(Unit[] units)
    {
        foreach (UnitSlot slot in Unitbuttons)
        {
            Destroy(slot.gameObject);
        }
        Unitbuttons.Clear();
        foreach (Unit unit in units)
        {
            GameObject button = Instantiate(buttonPrefab, unitsButtonsParent);
            UnitSlot slot = button.GetComponent<UnitSlot>();


            Unitbuttons.Add(slot);
        }
    }
}
