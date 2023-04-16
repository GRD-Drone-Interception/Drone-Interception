using DroneBehaviours.Scripts;
using DroneLoadout.Scripts;
using UnityEngine;

namespace DroneSelection.Scripts
{
    public class DroneUnitSelection : MonoBehaviour
    {
        [SerializeField] private Camera tacticalCamera;
        [SerializeField] private LayerMask clickableLayer;
        [SerializeField] private RectTransform boxVisual;
        private Rect _selectionBox;
        private Vector2 _startPosition = Vector2.zero;
        private Vector2 _endPosition = Vector2.zero;

        private void Start()
        {
            DrawVisual();
        }

        private void Update()
        {
            if (DroneDragAndDropSpawner.IsObjectInHand())
            {
                return;
            }
            
            if(Input.GetMouseButtonDown(0))
            {
                _startPosition = Input.mousePosition;
                _selectionBox = new Rect();

                Ray ray = tacticalCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickableLayer))
                {
                    if(Input.GetKey(KeyCode.LeftShift))
                    {
                        DroneManager.Instance.ShiftClickSelect(hit.collider.gameObject.GetComponent<Drone>());
                    }
                    else
                    {
                        DroneManager.Instance.ClickSelect(hit.collider.gameObject.GetComponent<Drone>());
                    }
                }
                else
                {
                    if (!Input.GetKey(KeyCode.LeftShift))
                    {
                        DroneManager.Instance.DeselectAll();
                    }
                }
            }

            if(Input.GetMouseButton(0))
            {
                _endPosition = Input.mousePosition;
                DrawVisual();
                DrawSelection();
                SelectUnits();
            }

            if (Input.GetMouseButtonUp(0))
            {
                //SelectUnits();
                _startPosition = Vector2.zero;
                _endPosition   = Vector2.zero;
                DrawVisual();
            }
        }

        /// <summary>
        /// Selects multiple units based on the selection box UI
        /// </summary>
        private void SelectUnits()
        {
            foreach (Drone unit in DroneManager.Drones)
            {
                if (_selectionBox.Contains(tacticalCamera.WorldToScreenPoint(unit.gameObject.transform.position)))
                {
                    DroneManager.Instance.DragSelect(unit);
                }
                else if(!Input.GetKey(KeyCode.LeftShift))
                {
                    DroneManager.Instance.Deselect(unit);
                }
            }
        }

        /// <summary>
        /// Sets the box's vertex positions
        /// </summary>
        private void DrawSelection()
        {
            if(Input.mousePosition.x < _startPosition.x)
            {
                _selectionBox.xMin = Input.mousePosition.x;
                _selectionBox.xMax = _startPosition.x;
            }
            else
            {
                _selectionBox.xMin = _startPosition.x;
                _selectionBox.xMax = Input.mousePosition.x;
            }

            if (Input.mousePosition.y < _startPosition.y)
            {
                _selectionBox.yMin = Input.mousePosition.y;
                _selectionBox.yMax = _startPosition.y;
            }
            else
            {
                _selectionBox.yMin = _startPosition.y;
                _selectionBox.yMax = Input.mousePosition.y;
            }
        }

        /// <summary>
        /// Sets the image's values based on created box
        /// </summary>
        private void DrawVisual()
        {
            Vector2 boxStart = _startPosition;
            Vector2 boxEnd   = _endPosition;

            Vector2 boxCenter = (boxStart + boxEnd) / 2;
            boxVisual.position = boxCenter;

            Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
            boxVisual.sizeDelta = boxSize;
        }
    }
}