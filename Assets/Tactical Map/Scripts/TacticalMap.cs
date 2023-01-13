using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TacticalMap : MonoBehaviour
{
    [SerializeField] private GameObject UnitPrefab;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())    // is the touch on the GUI
            {
                return;
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                Debug.Log($"Raycast hit {hit.point}");
                Instantiate(UnitPrefab, hit.point, Quaternion.identity, hit.transform);
            }
        }
    }

    public void OnUnitSelected(int unitID)
    {

    }
}
