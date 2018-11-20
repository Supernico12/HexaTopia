using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthUI))]
public class UnitController : MonoBehaviour
{
    [SerializeField] public Unit unit;

    TileManager manager;
    protected TileController currentTile;
    public Teams myTeam;
    public CharacterStats myStats;

    public HealthUI myhealthUI;

    BuildingController myHouse;
    List<TileController> possibleTiles = new List<TileController>();
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

        foreach (Vector2 movement in unit.Movements)
        {
            TileController posTile = manager.TilesCalculator(currentTile.tile.index, movement);
            if (posTile != null)
            {
                Material mat = posTile.GetComponent<Renderer>().material;
                mat.color = Color.red;
                possibleTiles.Add(posTile);


            }
        }
    }

    public virtual void OnDiselected()
    {
        foreach (TileController markedTile in possibleTiles)
        {
            Material mat = markedTile.GetComponent<Renderer>().material;
            mat.color = Color.white;
        }
        possibleTiles.Clear();
    }

    public void OnMove(TileController target)
    {
        foreach (TileController possibility in possibleTiles)
        {
            if (target.tile.index == possibility.tile.index)
            {
                if (target.tile.currentUnit == UnitFlags.None)
                {
                    Move(target);
                }
                else
                {
                    UnitController targetController = target.GetComponentInChildren<UnitController>();
                    if (myTeam != targetController.myTeam)
                    {
                        Attack(target);
                    }
                }

            }

        }

    }

    void Move(TileController newController)
    {

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
    }
    void Attack(TileController target)
    {
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
            myStats.TakeDamage(hisDamage);
            myhealthUI.OnChangeValue(myStats.currentHealth);

        }
        else
        {
            Move(target);
        }

    }

    public void Die()
    {
        currentTile.tile.currentUnit = UnitFlags.None;
        myHouse.RemoveUnit();
        myhealthUI.OnDestroyed();
        Destroy(gameObject);
    }

}
