using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthUI))]

public class UnitController : MonoBehaviour
{
    [SerializeField] public Unit unit;

    protected TileManager manager;
    public TileController currentTile;
    public Teams myTeam;
    public CharacterStats myStats;
    protected UIController uIController;

    public HealthUI myhealthUI;

    protected PlayersManager playersManager;
    BuildingController myHouse;


    public bool hasMoved = false;
    public bool hasAttack = false;
    // Deecha , Abajo , Izquierda , Arriba

    public void SetUnit(Unit unit, BuildingController myHouse)
    {
        this.unit = unit;
        this.myHouse = myHouse;
    }
    public void SetStats(CharacterStats stats)
    {
        myStats = stats;
    }
    public void SetController(TileController controller)
    {
        currentTile = controller;
    }
    public virtual void OnSelected()
    {
        //If Selected Unit = Current Turn
        if (playersManager.GetTurn == (int)myTeam)
        {
            if (!hasAttack)
            {
                //Check Posible Move / Attack
                manager.unitMotorController.CheckMove(this);


            }



        }
        uIController.SetDescriptionUnit(unit, myStats.currentHealth);
    }

    public virtual void OnDiselected()
    {


        uIController.CloseUnitDescriptions();
    }



    Material mat;
    public void SetCantMove()
    {
        if (!hasAttack)
        {
            hasAttack = true;
            Renderer ren = GetComponent<Renderer>();
            mat = ren.material;
            Material newMat = new Material(ren.material);
            Color newCol = newMat.color;
            //newCol *= 1.6f;
            newCol *= Color.gray;
            newCol.a = 1;
            newMat.color = newCol;

            ren.material = newMat;
        }
        /*
            var red = Color.red;
                var lightRed = red * 1.5f;
                var darkRed = red * 0.5f;

                // Correct alpha.
                lightRed.a = 1;
                darkRed.a = 1;
                */
    }

    public void SetCanMove()
    {

        hasAttack = false;
        hasMoved = false;
        Renderer ren = GetComponent<Renderer>();
        ren.material = mat;

    }

    public void Move(TileController newController)
    {
        hasMoved = true;
        //Previous Tile
        currentTile.tile.currentUnit = UnitFlags.None;

        //New Tile
        currentTile = newController;

        transform.parent = currentTile.transform;
        transform.position = currentTile.transform.position + new Vector3(0, 1, 0);

        currentTile.tile.currentUnit = unit.type;


        myhealthUI.OnChangePosition();
    }
    public virtual void Start()
    {
        manager = TileManager.instance;
        myhealthUI = GetComponent<HealthUI>();
        uIController = UIController.instance;
        playersManager = PlayersManager.instance;
        currentTile = GetComponentInParent<TileController>();
        SetCantMove();
        myhealthUI.OnChangeValue(unit.health);
    }
    public void Attack(TileController target)
    {
        SetCantMove();
        CharacterStats targetStats = target.GetComponentInChildren<UnitController>().myStats;
        float attackForce = (myStats.currentHealth / unit.health) * myStats.damage;
        float defenceForce = (targetStats.currentHealth / targetStats.controller.unit.health) * targetStats.defence;
        float totalForce = attackForce + defenceForce;
        float myDamage = Mathf.RoundToInt((attackForce / totalForce) * 4.5f * myStats.damage);
        float hisDamage = Mathf.RoundToInt((defenceForce / totalForce) * 4.5f * targetStats.defence);
        targetStats.TakeDamage(myDamage);
        targetStats.controller.myhealthUI.OnChangeValue(targetStats.currentHealth);
        if (targetStats.currentHealth > 0)
        {
            if (targetStats.range >= myStats.range)
                myStats.TakeDamage(hisDamage);
            myhealthUI.OnChangeValue(myStats.currentHealth);

        }
        else
        {
            if (myStats.range < 2)
                Move(target);
        }

    }

    public void Die()
    {

        Debug.Log(unit.name + " Has Died");
        currentTile.tile.currentUnit = UnitFlags.None;
        if (myHouse != null)
            myHouse.RemoveUnit();
        myhealthUI.OnDestroyed();
        playersManager.RemoveUnit(this, (int)myTeam);
        uIController.CloseUnitDescriptions();
        Destroy(gameObject);

    }

}
