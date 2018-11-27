using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{

    public UnitController controller;

    public float damage;
    public float defence;
    public int range;

    public float currentHealth;

    public CharacterStats(Unit unit, UnitController controller)
    {
        this.damage = unit.damage;
        this.defence = unit.defence;
        this.currentHealth = unit.health;
        this.controller = controller;
        this.range = unit.range;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            controller.Die();
        }
    }



}
