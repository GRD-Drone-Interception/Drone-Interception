using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Suicider,
    Recon,
    Swarmer,
    Assault
}

[RequireComponent(typeof(Collider))]
public class DroneUnit : MonoBehaviour, IDestructable
{
    [Header("Team")]
    [SerializeField] private PlayerTeam team;
    public PlayerTeam Team => team;


    [Header("Class")]
    public UnitType droneClass;


    [Header("Health")]
    [SerializeField] private float maxHealth = 100;
    public float MaxHealth => maxHealth;

    private float health;
    public float CurrentHealth => health;


    [Header("Misc")]
    public UnitUI ui;

    private void Awake()
    {
        health = maxHealth;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destory();
        }

        ui.OnHealthValueChanged(health);
    }

    public void Destory()
    {
        Debug.Log("Unit has been destroyed");
        ui.EnableDeathIcon();
    }
}
