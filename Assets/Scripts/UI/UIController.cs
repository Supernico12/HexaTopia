﻿using System.Collections;
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
    [SerializeField] Transform unitsInfoParent;
    [SerializeField] Transform resourcesParent;
    [SerializeField] Material[] materials;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Sprite[] resourcesIcons;


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
        playerManager.OnTurnEnded += CloseUnitMenu;
        playerManager.OnTurnEnded += CloseUnitDescriptions;

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

    public void SetButtonResource(ViligerController vilController, ResourceController res)
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

        if (resourceSlot != null)
            Destroy(resourceSlot.gameObject);
    }

    public Material GetMaterial(int index)
    {
        return materials[index];
    }


    public void SetDescriptionUnit(Unit unit, float currentHealth)
    {
        unitsDescriptionParent.gameObject.SetActive(true);
        descriptionTexts[0].text = unit.name;
        descriptionTexts[1].text = "Attack: " + unit.damage;
        descriptionTexts[2].text = "Defence: " + unit.defence;
        descriptionTexts[3].text = "Health " + currentHealth + "/" + unit.health;
    }

    public void SetBuildingDescription(Building building, float housed)
    {
        unitsDescriptionParent.gameObject.SetActive(true);
        descriptionTexts[0].text = building.name;
        descriptionTexts[1].text = "";
        descriptionTexts[2].text = "";
        descriptionTexts[3].text = "Housed: " + housed + "/" + building.maxUnits;
    }
    public void SetResourceDescription(string name, ResourceController res)
    {
        int resGive = res.resourceToAdd.GetResourceCost();
        unitsDescriptionParent.gameObject.SetActive(true);
        descriptionTexts[0].text = name;
        descriptionTexts[1].text = "";
        descriptionTexts[3].text = "Uses left:" + res.GetCurrentLifespawn + "/" + res.GetLifespawn;
        descriptionTexts[2].text = "Get: " + resGive + " " + res.resourceToAdd.type;
    }

    public void SetUnitsInFo(Unit unit)
    {
        unitsInfoParent.gameObject.SetActive(true);
        TextMeshProUGUI[] infoTexts = unitsInfoParent.GetComponentsInChildren<TextMeshProUGUI>();
        infoTexts[0].text = unit.name;
        infoTexts[1].text = unit.description;
        infoTexts[2].text = "Health: " + unit.health.ToString();
        infoTexts[3].text = "Damage: " + unit.damage.ToString();
        infoTexts[4].text = "Defence: " + unit.defence.ToString();
        infoTexts[5].text = "Range: " + unit.range.ToString();
        infoTexts[6].text = "Movement: " + unit.movementSpeed.ToString();
    }

    public void SetBuildingsInfo(Building build)
    {
        unitsInfoParent.gameObject.SetActive(true);
        TextMeshProUGUI[] infoTexts = unitsInfoParent.GetComponentsInChildren<TextMeshProUGUI>();
        infoTexts[0].text = build.name;
        infoTexts[1].text = build.description;
        foreach (Unit unit in build.unitsToProduce)
        {
            infoTexts[2].text += unit.name + "\n";
        }

    }

    public void CloseUnitInfo()
    {
        unitsInfoParent.gameObject.SetActive(false);
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

    public Sprite GetResourceSprite(Resource resource)
    {
        int index = resource.GetResourcesIcons();
        if (index > -1)
        {
            return resourcesIcons[index];
        }
        return null;
    }

    public Sprite GetResourceSprite(int index)
    {
        return resourcesIcons[index];
    }


    void OnTurnedChange()
    {
        resourcesText[0].text = ((Teams)playerManager.GetTurn).ToString();
        OnResourcesChanged();
    }
}

