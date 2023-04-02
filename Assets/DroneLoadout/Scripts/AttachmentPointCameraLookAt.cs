using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneLoadout
{
    public class AttachmentPointCameraLookAt : MonoBehaviour
    {
        [SerializeField] private Camera focusCamera;
        [SerializeField] private Camera displayCamera;
        private List<DroneAttachmentSlot> _droneAttachmentSlots = new();
        private Transform _target;
        private Vector3 _displayCameraStartRotationEuler;
        private Vector3 _focusCameraStartRotationEuler;
        
        private void OnEnable() => WorkshopModeController.OnModeChange += OnWorkshopModeChange;
        private void OnDisable() => WorkshopModeController.OnModeChange -= OnWorkshopModeChange;
        
        private void Start()
        {
            _displayCameraStartRotationEuler = displayCamera.transform.rotation.eulerAngles;
            _focusCameraStartRotationEuler = focusCamera.transform.rotation.eulerAngles;
        }

        private void OnWorkshopModeChange(WorkshopModeController.WorkshopMode mode)
        {
            if (mode == WorkshopModeController.WorkshopMode.Edit)
            {
                StartCoroutine(FindAttachmentSlotsCoroutine());
            }
            else
            {
                _target = null;
                _droneAttachmentSlots.ForEach(slot => slot.OnAttachmentSlotSelected -= SetCameraTargetOnAttachmentSlotSelected);
                _droneAttachmentSlots.Clear();
                displayCamera.transform.eulerAngles = _displayCameraStartRotationEuler;
                focusCamera.transform.eulerAngles = _focusCameraStartRotationEuler;
            }
        }

        private IEnumerator FindAttachmentSlotsCoroutine()
        {
            yield return new WaitForSeconds(0.25f);
            _droneAttachmentSlots.AddRange(FindObjectsOfType<DroneAttachmentSlot>());
            _droneAttachmentSlots.ForEach(slot => slot.OnAttachmentSlotSelected += SetCameraTargetOnAttachmentSlotSelected); 
        }

        private void SetCameraTargetOnAttachmentSlotSelected(DroneAttachmentSlot slot)
        {
            if (slot.GetAttachmentPoint().IsVisible())
            {
                _target = slot.GetAttachmentPoint().transform;
            }
        }

        private void Update()
        {
            if (_target == null) { return; }
            
            if (WorkshopModeController.currentWorkshopMode == WorkshopModeController.WorkshopMode.Edit)
            {
                focusCamera.transform.LookAt(_target);
            }
        }
    }
}