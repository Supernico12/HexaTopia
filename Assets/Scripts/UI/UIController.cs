using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] Material[] materials;
    [SerializeField] GameObject buttonPrefab;
    List<UnitSlot> unitbuttons = new List<UnitSlot>();
    List<BuildingSlot> buildingButtons = new List<BuildingSlot>();

    public void SetButtonsCreateUnit(Unit[] units, BuildingController controller)
    {

        for (int i = 0; i < units.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, unitsButtonsParent);
            Button buttonScript = button.GetComponent<Button>();

            UnitSlot slot = button.AddComponent<UnitSlot>();
            slot.SetContent(units[i], controller, i);
            buttonScript.onClick.AddListener(slot.OnTouch);



            unitbuttons.Add(slot);
        }

        unitsButtonsParent.gameObject.SetActive(true);
    }

    public void SetButtonsCreateBuilding(Building[] buildings, ViligerController controller)
    {


        for (int i = 0; i < buildings.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, unitsButtonsParent);
            Button buttonScript = button.GetComponent<Button>();

            BuildingSlot slot = button.AddComponent<BuildingSlot>();

            slot.SetContent(controller, buildings[i], i);
            buttonScript.onClick.AddListener(slot.OnTouch);


            buildingButtons.Add(slot);
        }

        unitsButtonsParent.gameObject.SetActive(true);
    }

    public void CloseUnitMenu()
    {
        unitsButtonsParent.gameObject.SetActive(false);
        foreach (UnitSlot slot in unitbuttons)
        {
            Destroy(slot.gameObject);
        }

        unitbuttons.Clear();

        foreach (BuildingSlot slot in buildingButtons)
        {
            Destroy(slot.gameObject);

        }
        buildingButtons.Clear();
    }

    public Material GetMaterial(int index)
    {
        return materials[index];
    }
}
