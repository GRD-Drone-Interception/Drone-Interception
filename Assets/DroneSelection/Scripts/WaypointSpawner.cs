using System;
using System.Collections.Generic;
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
        private Vector3 _hitPointPosition;
        private List<MeshRenderer> _objectMeshRenderers = new();
        private Color _defaultObjectColour;

        public void OnPointerDown(PointerEventData eventData)
        {
            _objectInHand = Instantiate(waypointPrefab, Input.mousePosition, Quaternion.identity);
            _objectMeshRenderers.AddRange(_objectInHand.GetComponentsInChildren<MeshRenderer>());
            _defaultObjectColour = _objectMeshRenderers[0].material.color;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            _objectMeshRenderers.Clear();
            _offset = Vector3.zero;

            if (!_inSpawnableArea)
            {
                Destroy(_objectInHand);
                return;
            }

            var waypointBobber = _objectInHand.GetComponentInChildren<WaypointBobbing>();
            waypointBobber.SetStartPosition(_objectInHand.transform.position + Vector3.up*2);
            waypointBobber.Play();
            OnWaypointPlaced?.Invoke(_objectInHand.gameObject);
            _objectInHand = null;
        }

        private void Update()
        {
            if (_objectInHand != null)
            {
                if (Input.GetKey(KeyCode.R))
                {
                    if (_objectInHand.transform.position.y > _hitPointPosition.y)
                    {
                        _offset += new Vector3(0, -20.0f * Time.deltaTime, 0);
                    }
                }
                else if (Input.GetKey(KeyCode.T))
                {
                    _offset += new Vector3(0, 20.0f * Time.deltaTime, 0);
                }
            
                // Ignore the Waypoint layer
                if (Physics.Raycast(tacticalCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, Mathf.Infinity, ~LayerMask.GetMask("Waypoint")))
                {
                    var rotation = Quaternion.Euler(90, _objectInHand.transform.rotation.y, _objectInHand.transform.rotation.z);
                    _hitPointPosition = hitInfo.point;
                    _objectInHand.transform.position = hitInfo.point + _offset;
                    _objectInHand.transform.rotation = rotation;

                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("AttSpawnable") ||
                        hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("DffSpawnable"))
                    {
                        _objectMeshRenderers.ForEach(meshRenderer => meshRenderer.material.color = _defaultObjectColour);
                        _inSpawnableArea = true;
                    }
                    else
                    {
                        _objectMeshRenderers.ForEach(meshRenderer => meshRenderer.material.color = Color.red * 10f);
                        _inSpawnableArea = false;
                    }
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
