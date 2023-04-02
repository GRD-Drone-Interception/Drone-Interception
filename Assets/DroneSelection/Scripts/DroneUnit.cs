using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public enum UnitType
{
    Suicider,
    Recon,
    Swarmer,
    Assault
}

public enum UnitOrder
{
    Idle,
    Move,
    Recon,
    Attack,
    Defend,
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


    [Header("UI")]
    public UnitUI unitUI;
    public GameObject worldSelectedIcon;
    public GameObject mapSelectedIcon;

    [Header("Misc")]
    [SerializeField] private UnitOrder currentOrder = UnitOrder.Idle;

    private void Awake()
    {
        health = maxHealth;
    }

    void Start()
    {
        UnitManager.Instance.units.Add(this);
    }

    void Update()
    {

    }

    /// <summary>
    /// Reduces the health of this unit by damage
    /// </summary>
    /// <param name="damage">The amount of health to be removed</param>
    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destory();
        }

        unitUI.OnHealthValueChanged(health);
    }

    /// <summary>
    /// Called when the unit has no remaining health
    /// </summary>
    public void Destory()
    {
        Debug.Log("Unit has been destroyed");
        unitUI.EnableDeathIcon();
    }

    private void OnDestroy()
    {
        UnitManager.Instance.units.Remove(this);
    }

    public void Command(UnitOrder command)
    {
        currentOrder = command;

        switch (command)
        {
            case UnitOrder.Move:
                Move();
                break;
            case UnitOrder.Recon:
                Recon();
                break;
            case UnitOrder.Attack:
                Attack();
                break;
            case UnitOrder.Defend:
                Defend();
                break;
            default:
                Idle();
                break;
        }
    }

    private void Idle()
    {
        Debug.Log($"{this.gameObject.name} is idling");
    }

    private void Move()
    {
        Debug.Log($"{this.gameObject.name} is moving");
    }

    private void Attack()
    {
        Debug.Log($"{this.gameObject.name} is attacking");
    }

    private void Defend()
    {
        Debug.Log($"{this.gameObject.name} is defending");
    }

    private void Recon()
    {
        Debug.Log($"{this.gameObject.name} is surveying");
    }


    public void SetSelectionIcon(bool active)
    {
        worldSelectedIcon.SetActive(active);
        mapSelectedIcon.SetActive(active);
    }
}
