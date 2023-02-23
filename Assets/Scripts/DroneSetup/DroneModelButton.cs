using System;
using System.Collections.Generic;
using DroneSetup.DroneWorkbench;
using UnityEngine;
using UnityEngine.UI;

namespace DroneSetup
{
    /// <summary>
    /// Responsible for spawning a drone model onto the workbench.
    /// </summary>
    public class DroneModelButton : MonoBehaviour
    {
        public event Action<DroneModelButton> OnDroneModelSelected;
        
        [SerializeField] private GameObject droneTypePrefab;
        [SerializeField] private DroneTypeButton droneTypeButton;
        [SerializeField] private List<DroneAttachmentSlot> droneComponentSlots;
        private Button _modelButton;
        private Workbench _workbench;

        private void OnEnable() => _modelButton.onClick.AddListener(SpawnDroneModel);
        private void OnDisable() => _modelButton.onClick.RemoveListener(SpawnDroneModel);

        public void Awake()
        {
            _workbench = FindObjectOfType<Workbench>();
            _modelButton = GetComponent<Button>();
        }

        private void Start()
        {
            droneComponentSlots.ForEach(slot => slot.gameObject.SetActive(false));
            droneComponentSlots.ForEach(slot => slot.HideComponentSubMenu());
        }

        private void SpawnDroneModel()
        {
            droneTypeButton.HideModelSubMenu();
            _workbench.SpawnDronePrefab(droneTypePrefab);
            //droneComponentSlots.ForEach(slot => slot.gameObject.SetActive(true));
            droneComponentSlots.ForEach(slot => slot.BindToDrone(_workbench.DroneBeingEdited));
            droneComponentSlots.ForEach(slot => slot.BindToAttachmentPoint(_workbench.DroneBeingEdited.GetAttachmentPoints()[0]));
            OnDroneModelSelected?.Invoke(this);
        }
    }
}