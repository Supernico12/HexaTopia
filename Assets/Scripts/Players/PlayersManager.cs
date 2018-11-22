using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayersManager : MonoBehaviour
{
    [SerializeField]
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
            playerBank[i] = new Resource(3, 3, 3);
        }
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
        playersUnits[index].Remove(cont);
    }



    public int GetTurn
    {
        get { return turn; }
    }
}



