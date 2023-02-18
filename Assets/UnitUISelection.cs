using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUISelection : MonoBehaviour
{
    [SerializeField] private Button droneButton;

    private void OnEnable()
    {
        droneButton.onClick.AddListener(UnitSelectDeselect);
    }

    private void OnDisable()
    {
        droneButton.onClick.RemoveListener(UnitSelectDeselect);
    }

    void Start()
    {
        
    }

    private void UnitSelectDeselect()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            UnitManager.Instance.ShiftClickSelect(this.GetComponent<UnitUI>().unit);
        }
        else
        {
            UnitManager.Instance.ClickSelect(this.GetComponent<UnitUI>().unit);
        }
    }

    void Update()
    {
        
    }
}
