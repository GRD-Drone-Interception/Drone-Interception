using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    public Image  icon;
    public Slider healthBar;
    [SerializeField] private Image deathIcon;
    public DroneUnit unit;

    private void Start()
    {

    }

    public void SetUnit(DroneUnit unit)
    {
        this.unit = unit;
        SetData();
    }

    private void SetData()
    {
        healthBar.minValue = 0;
        healthBar.maxValue = unit.MaxHealth;

        healthBar.value = unit.CurrentHealth;

        switch (unit.droneClass)
        {
            case UnitType.Suicider:
                icon.color = Color.red;
                break;
            case UnitType.Recon:
                icon.color = Color.grey;
                break;
            case UnitType.Swarmer:
                icon.color = Color.green;
                break;
            case UnitType.Assault:
                icon.color = Color.blue;
                break;
            default:
                break;
        }

        deathIcon.enabled = false;
    }

    public void OnHealthValueChanged(float value)
    {
        healthBar.value = value;
    }

    public void EnableDeathIcon()
    {
        deathIcon.enabled = true;
    }
}