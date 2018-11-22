using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthUI))]
public class UnitController : MonoBehaviour
{
    [SerializeField] public Unit unit;

    protected TileManager manager;
    protected TileController currentTile;
    public Teams myTeam;
    public CharacterStats myStats;
    protected UIController uIController;

    public HealthUI myhealthUI;

    protected PlayersManager playersManager;
    BuildingController myHouse;
    List<TileController> possibleTiles = new List<TileController>();

    protected bool hasMoved = false;
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
            if (!hasMoved)
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
        }
        uIController.SetDescriptionUnit(unit.name, unit.damage.ToString(), unit.defence.ToString());
    }

    public virtual void OnDiselected()
    {
        foreach (TileController markedTile in possibleTiles)
        {
            Material mat = markedTile.GetComponent<Renderer>().material;
            mat.color = Color.white;
        }
        possibleTiles.Clear();
        uIController.CloseUnitDescriptions();
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
                    SetCantMove();
                }
                else
                {
                    UnitController targetController = target.GetComponentInChildren<UnitController>();
                    if (myTeam != targetController.myTeam)
                    {
                        Attack(target);
                        SetCantMove();
                    }
                }

            }


        }

    }

    Material mat;
    public void SetCantMove()
    {
        hasMoved = true;
        Renderer ren = GetComponent<Renderer>();
        mat = ren.material;
        Material newMat = new Material(ren.material);
        Color newCol = newMat.color;
        //newCol *= 1.6f;
        newCol *= Color.gray;
        newCol.a = 1;
        newMat.color = newCol;

        ren.material = newMat;

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
        if (hasMoved)
        {
            hasMoved = false;
            Renderer ren = GetComponent<Renderer>();
            ren.material = mat;
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
        uIController = UIController.instance;
        playersManager = PlayersManager.instance;
        currentTile = GetComponentInParent<TileController>();
        SetCantMove();
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
        if (myHouse != null)
            myHouse.RemoveUnit();
        myhealthUI.OnDestroyed();
        playersManager.RemoveUnit(this, (int)myTeam);
        Destroy(gameObject);
    }

}
