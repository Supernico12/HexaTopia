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
    [SerializeField] Transform unitsDescriptionParent;
    [SerializeField] Transform resourcesParent;
    [SerializeField] Material[] materials;
    [SerializeField] GameObject buttonPrefab;
    List<UnitSlot> unitbuttons = new List<UnitSlot>();
    List<BuildingSlot> buildingButtons = new List<BuildingSlot>();

    ResourceSlot resourceSlot;



    TextMeshProUGUI[] descriptionTexts;
    TextMeshProUGUI[] resourcesText;
    PlayersManager playerManager;
    void Start()
    {
        descriptionTexts = unitsDescriptionParent.GetComponentsInChildren<TextMeshProUGUI>();
        resourcesText = resourcesParent.GetComponentsInChildren<TextMeshProUGUI>();
        playerManager = PlayersManager.instance;
        playerManager.OnTurnEnded += OnTurnedChange;
        OnResourcesChanged();
    }

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
        unitsDescriptionParent.gameObject.SetActive(false);
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

    public void SetButtonGetResource(ViligerController vilController, Resource res)
    {
        GameObject button = Instantiate(buttonPrefab, unitsButtonsParent);
        Button buttonScript = button.GetComponent<Button>();

        ResourceSlot slot = button.AddComponent<ResourceSlot>();

        slot.SetContent(vilController, res, playerManager);
        buttonScript.onClick.AddListener(slot.OnTouch);


        resourceSlot = slot;


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


    public void SetDescriptionUnit(string name, string attack, string defence)
    {
        unitsDescriptionParent.gameObject.SetActive(true);
        descriptionTexts[0].text = name;
        descriptionTexts[1].text = "Attack: " + attack;
        descriptionTexts[2].text = "Defence: " + defence;
    }

    public void CloseUnitDescriptions()
    {
        unitsDescriptionParent.gameObject.SetActive(false);
    }

    public void OnResourcesChanged()
    {
        Resource rec = playerManager.GetPlayerResourcesbyTurn();
        resourcesText[1].text = "Food: " + rec.food.ToString();
        resourcesText[2].text = "Wood: " + rec.wood.ToString();
        resourcesText[3].text = "Gold: " + rec.gold.ToString();
    }




    void OnTurnedChange()
    {
        resourcesText[0].text = ((Teams)playerManager.GetTurn).ToString();
        OnResourcesChanged();
    }
}

