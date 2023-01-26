using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    [SerializeField] private Image  unitImage;
    [SerializeField] private Slider unitHealthBar;
    [SerializeField] private Image  unitDeadIcon;

    private void Start()
    {
        unitHealthBar.minValue = 0;
        unitHealthBar.maxValue = 100;

        unitHealthBar.value = unitHealthBar.maxValue;

        unitDeadIcon.enabled = false;
    }

    public void OnHealthValueChanged(float value)
    {
        unitHealthBar.value = value;

        if(unitHealthBar.value == unitHealthBar.minValue)
        {
            unitDeadIcon.enabled = true;
        }
    }
}