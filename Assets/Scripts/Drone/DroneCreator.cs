﻿using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Drone
{
    public class DroneCreator : MonoBehaviour, IPointerDownHandler
    {
        public event Action OnDroneSpawned;
        
        [SerializeField] private GameObject prefabToSpawn;
        private Workbench _workbench; // Unnecessary dependency
        private bool _isActivated = true;

        private void Awake() => _workbench = FindObjectOfType<Workbench>();

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isActivated)
            {
                var carousel = _workbench.Carousel;
                if (carousel.IsMoving)
                {
                    Debug.LogWarning("Carousel is in motion, drone cannot be spawned!");
                    return;
                }

                var currentNode = carousel.PodiumNodes[carousel.CurrentPodiumNodeIndex];
                if (_workbench.DronesOnPodiumDict.ContainsValue(currentNode))
                {
                    Debug.LogWarning("A drone already occupies this node!");
                    return;
                }

                SpawnDrone();
            }
        }

        private void SpawnDrone()
        {
            var drone = Instantiate(prefabToSpawn);
            drone.transform.position = _workbench.DroneSpawnpoint.position;
            OnDroneSpawned?.Invoke();
        }

        public void SetActive(bool active)
        {
            _isActivated = active;
        }
    }
}