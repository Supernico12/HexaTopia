using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayersManager : MonoBehaviour
{

    List<UnitController>[] playersUnits = new List<UnitController>[1];
    List<UnitController> currentUnit
    {
        get { return playersUnits[turn]; }
    }

    #region Singleton
    public static PlayersManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Debug.LogWarning("Trying To Create Two Player Managers"); }
    }
    #endregion

    [SerializeField] [Range(1, 8)] int playerQuantity = 2;
    Resource[] playerBank;

    public event System.Action OnTurnEnded;

    [SerializeField] TextMeshProUGUI txtTurn;
    int turn;

    void Start()
    {
        playerBank = new Resource[playerQuantity];
        playersUnits = new List<UnitController>[playerQuantity];
        for (int i = 0; i < playersUnits.Length; i++)
        {
            playersUnits[i] = new List<UnitController>();
        }
        OnTurnEnded += RefreshUnits;

        for (int i = 0; i < playerBank.Length; i++)
        {
            playerBank[i] = new Resource(100, 100, 100);
        }
        newMovement = new Vector2[100];
        TransformNumToMovement(3);
    }


    public Resource GetPlayerResources(int index)
    {
        return playerBank[index];

    }

    public Resource GetPlayerResourcesbyTurn()
    {
        return playerBank[turn];
    }

    public void FinishTurn()
    {
        turn = (turn + 1) % playerQuantity;
        if (OnTurnEnded != null)
        {
            OnTurnEnded.Invoke();
        }



    }

    void RefreshUnits()
    {
        foreach (UnitController unit in currentUnit)
        {

            unit.SetCanMove();

        }
    }

    public void AddUnit(UnitController cont)
    {
        currentUnit.Add(cont);
    }
    public void AddUnit(UnitController cont, int index)
    {
        playersUnits[index].Add(cont);
    }

    public void RemoveUnit(UnitController cont, int index)
    {
        bool succesfullyremoved = playersUnits[index].Remove(cont);

    }



    public int GetTurn
    {
        get { return turn; }
    }

    Vector2[] newMovement;
    public void TransformNumToMovement(int num)
    {
        newMovement[0] = new Vector2(0, 1);
        newMovement[1] = new Vector2(1, 0);
        newMovement[2] = new Vector2(0, -1);
        newMovement[3] = new Vector2(-1, 0);

        for (int y = 1; y <= num; y++)
        {
            for (int x = 1; x <= num; x++)
            {

                newMovement[x * 4] = new Vector2(x, y);
                //newMovement[y * 4 + x] = new Vector2(-x, y);
                //newMovement[y * 4 + x + 1] = new Vector2(x, -y);
                //newMovement[y * 4 + x + 2] = new Vector2(-x, -y);
            }
        }
    }
}



