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

    public void SetButtonsCreateUnit(Unit[] units, BuildingController controller)
    {
        foreach (UnitSlot slot in Unitbuttons)
        {
            Destroy(slot.gameObject);
        }
        Unitbuttons.Clear();
        for (int i = 0; i < units.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, unitsButtonsParent);
            UnitSlot slot = button.GetComponent<UnitSlot>();
            slot.SetContent(units[i], controller, i);


            Unitbuttons.Add(slot);
        }

        unitsButtonsParent.gameObject.SetActive(true);
    }

    public void CloseUnitMenu()
    {
        unitsButtonsParent.gameObject.SetActive(false);
    }
}
