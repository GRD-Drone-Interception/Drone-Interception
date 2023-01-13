using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject unitButtonPrefab;

    void Start()
    {
        for (int i = 0; i < TacticalMap.Instance.units.Count; i++)
        {
            GameObject button = Instantiate(unitButtonPrefab, panel.transform);
            button.GetComponent<UnitButton>().SetID(i);
        }
    }
}
