using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{

    public UnitController controller;

    public float damage;
    public float defence;

    public float currentHealth;

    public CharacterStats(float damage, float defence, float currentHealth, UnitController controller)
    {
        this.damage = damage;
        this.defence = defence;
        this.currentHealth = currentHealth;
        this.controller = controller;
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
