using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayersManager : MonoBehaviour
{


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

        for (int i = 0; i < playerBank.Length; i++)
        {
            playerBank[i] = new Resource(300, 300, 0);
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

    public int GetTurn
    {
        get { return turn; }
    }
}



