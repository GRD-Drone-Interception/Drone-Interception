using System;
using DroneBehaviours.Scripts;
using DroneLoadout.Decorators;
using DroneLoadout.Scripts;
using Testing;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace DroneSelection
{
    /// <summary>
    /// Used to drag and drop drone prefabs from UI buttons into world-space 
    /// </summary>
    public class AttSpawn : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public event Action OnDroneSpawned;

        [SerializeField] private DroneConfigData droneConfigData;
        [SerializeField] private Camera tacticalCamera;
        private static Drone _drone;
        private bool _inSpawnableArea;

        public void OnPointerDown(PointerEventData eventData)
        {
            var objectInHand = Instantiate(droneConfigData.DronePrefab, Input.mousePosition, Quaternion.identity);
            _drone = objectInHand.GetComponent<Drone>();
            _drone.Rb.constraints = RigidbodyConstraints.FreezeAll;
            _drone.Rb.detectCollisions = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_inSpawnableArea)
            {
                Destroy(_drone.gameObject);
                return;
            }

            _drone.RemoveBlueprintShader();
            _drone.SetTeam(TurnManager.Instance.CurrentTeam);
            DroneManager.Instance.AddDrone(_drone);
            OnDroneSpawned?.Invoke();

            _drone.Rb.constraints = RigidbodyConstraints.None;
            _drone.Rb.detectCollisions = true;
            _drone = null;
        }

        private void Update()
        {
            if (_drone != null)
            {
                if (Input.GetKey(KeyCode.R))
                {
                    _drone.transform.Rotate(new Vector3(0, -20.0f * Time.deltaTime, 0));
                }
                else if (Input.GetKey(KeyCode.T))
                {
                    _drone.transform.Rotate(new Vector3(0, 20.0f * Time.deltaTime, 0));
                }

                // Ignore the Unit layer
                if (Physics.Raycast(tacticalCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, Mathf.Infinity, ~LayerMask.GetMask("Unit")))
                {
                    _drone.transform.position = hitInfo.point + Vector3.up;

                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("AttSpawnable"))
                    {
                        _drone.ApplyBlueprintShader(_drone.BlueprintColour);
                        _inSpawnableArea = true;
                    }
                    else
                    {
                        _drone.ApplyBlueprintShader(Color.red);
                        _inSpawnableArea = false;
                    }
                }
                else // if no colliders detected, lock z position of selected drone in hand
                {
                    _drone.ApplyBlueprintShader(Color.red);
                    var v3 = Input.mousePosition;
                    v3.z = 8.0f;
                    v3 = tacticalCamera.ScreenToWorldPoint(v3);
                    _drone.transform.position = v3;
                }
            }
        }

        public static bool IsObjectInHand()
        {
            return _drone;
        }
    }
}