using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSelection : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask clickableLayer;

    [SerializeField] private RectTransform boxVisual;
    private Rect selectionBox;

    private Vector2 startPosition = Vector2.zero;
    private Vector2 endPosition = Vector2.zero;

    void Start()
    {
        DrawVisual();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            startPosition = Input.mousePosition;
            selectionBox = new Rect();

            Ray ray = CameraRigManager.Instance.activeCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickableLayer))
            {
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    UnitManager.Instance.ShiftClickSelect(hit.collider.gameObject.GetComponent<DroneUnit>());
                }
                else
                {
                    UnitManager.Instance.ClickSelect(hit.collider.gameObject.GetComponent<DroneUnit>());
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitManager.Instance.DeselectAll();
                }
            }
        }

        if(Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }

        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();
            startPosition = Vector2.zero;
            endPosition   = Vector2.zero;
            DrawVisual();
        }
    }

    /// <summary>
    /// Selects multiple units based on the selection box UI
    /// </summary>
    private void SelectUnits()
    {
        foreach (DroneUnit unit in UnitManager.Instance.units)
        {
            if (selectionBox.Contains(CameraRigManager.Instance.activeCamera.WorldToScreenPoint(unit.gameObject.transform.position)))
            {
                UnitManager.Instance.DragSelect(unit);
            }
        }
    }

    /// <summary>
    /// Ssets the box's vertex positions
    /// </summary>
    private void DrawSelection()
    {
        if(Input.mousePosition.x < startPosition.x)
        {
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }

        if (Input.mousePosition.y < startPosition.y)
        {
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }

    /// <summary>
    /// Sets the image's values based on created box
    /// </summary>
    private void DrawVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd   = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
        boxVisual.sizeDelta = boxSize;
    }
}
