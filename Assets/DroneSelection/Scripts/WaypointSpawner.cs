using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DroneSelection.Scripts
{
    public class WaypointSpawner : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public event Action<GameObject> OnWaypointPlaced;
        
        [SerializeField] private GameObject waypointPrefab;
        [SerializeField] private Camera tacticalCamera;
        private GameObject _objectInHand;
        private bool _inSpawnableArea;
        private Vector3 _offset;

        public void OnPointerDown(PointerEventData eventData)
        {
            _objectInHand = Instantiate(waypointPrefab, Input.mousePosition, Quaternion.identity);
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_inSpawnableArea)
            {
                Destroy(_objectInHand);
                return;
            }

            var waypointBobber = _objectInHand.GetComponentInChildren<WaypointBobbing>();
            waypointBobber.SetStartPosition(_objectInHand.transform.position + Vector3.up*2);
            waypointBobber.Play();
            OnWaypointPlaced?.Invoke(_objectInHand.gameObject);
            _offset = Vector3.zero;
            _objectInHand = null;
        }

        private void Update()
        {
            if (_objectInHand != null)
            {
                // disable camera elevation input
                Debug.Log($"Object in hand: {_objectInHand}", _objectInHand);
                
                if (Input.GetKey(KeyCode.R))
                {
                    _offset += new Vector3(0, -20.0f * Time.deltaTime, 0);
                }
                else if (Input.GetKey(KeyCode.T))
                {
                    _offset += new Vector3(0, 20.0f * Time.deltaTime, 0);
                }
            
                // Ignore the Waypoint layer
                if (Physics.Raycast(tacticalCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, Mathf.Infinity, ~LayerMask.GetMask("Waypoint")))
                {
                    var rotation = Quaternion.Euler(90, _objectInHand.transform.rotation.y, _objectInHand.transform.rotation.z);
                    _objectInHand.transform.position = hitInfo.point + _offset;
                    _objectInHand.transform.rotation = rotation;
                    _inSpawnableArea = hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("AttSpawnable") ||
                                       hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("DffSpawnable");
                }
                else // if no colliders detected, lock z position of selected object in hand
                {
                    var v3 = Input.mousePosition; 
                    v3.z = 8.0f; 
                    v3 = tacticalCamera.ScreenToWorldPoint(v3); 
                    _objectInHand.transform.position = v3;
                }
            }
        }
    }
}
