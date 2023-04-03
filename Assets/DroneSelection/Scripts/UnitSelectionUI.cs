using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject unitButtonContainer;
    [SerializeField] private GameObject unitButtonPrefab;

    void Start()
    {
        for (int i = 0; i < TacticalMap.Instance.units.Count; i++)
        {
            GameObject button = Instantiate(unitButtonPrefab, unitButtonContainer.transform);
            button.GetComponent<UnitButton>().SetID(i);
        }
    }
}
