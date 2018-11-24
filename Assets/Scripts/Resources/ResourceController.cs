using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{

    public Resource resourceToAdd;
    UIController uIController;
    TileController controller;
    [SerializeField] int lifeSpawn = 3;

    int currentLifeSpawn;


    public int GetLifespawn
    {
        get { return lifeSpawn; }
    }
    public int GetCurrentLifespawn
    {
        get { return currentLifeSpawn; }
    }
    public void OnFocus()
    {
        uIController.SetResourceDescription(name, this);
    }

    public void OnDisFocus()
    {
        uIController.CloseUnitDescriptions();
    }

    void Start()
    {
        uIController = UIController.instance;
        controller = GetComponentInParent<TileController>();
        currentLifeSpawn = lifeSpawn;
    }

    public void ReduceLifeTime(int num = 1)
    {
        currentLifeSpawn -= num;
        if (currentLifeSpawn <= 0)
        {
            Die();


        }

    }

    void Die()
    {
        controller.tile.currentResource = ResourceType.None;
        Destroy(gameObject);
    }

}
