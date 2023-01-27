using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjective : MonoBehaviour, IDestructable
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100;
    public float MaxHealth => maxHealth;

    [SerializeField] private float currentHealth;
    public float CurrentHealth => currentHealth;

    public delegate void DestroyObjective();
    public static event DestroyObjective OnDestroyed;

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
        //Debug.Log("Objective Destroyed");
        OnDestroyed?.Invoke();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Damage(10);
        }
    }
}
