using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjective : MonoBehaviour, IDestructable
{
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;


    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Destory();
        }
    }

    public void Destory()
    {
        Debug.Log("Objective Destroyed");
    }

    void Update()
    {
        
    }
}
