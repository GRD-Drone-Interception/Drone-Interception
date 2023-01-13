using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject unitButtonPrefab;

    [SerializeField] private int unitCount = 3;

    void Start()
    {
        for (int i = 0; i < unitCount; i++)
        {
            Instantiate(unitButtonPrefab, panel.transform);
        }
    }

    void Update()
    {
        
    }
}
