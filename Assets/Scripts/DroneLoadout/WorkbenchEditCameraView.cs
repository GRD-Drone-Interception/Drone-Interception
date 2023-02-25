using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneLoadout
{
    public class WorkbenchEditCameraView : MonoBehaviour
    {
        private readonly List<DroneModelSpawner> _droneModelSpawners = new();
        private List<DroneAttachmentSlot> _droneAttachmentSlots = new();
        private Transform _target;

        private void Awake() => _droneModelSpawners.AddRange(FindObjectsOfType<DroneModelSpawner>());
        private void OnEnable() => _droneModelSpawners.ForEach(ctx => ctx.OnDroneModelSpawned += OnDroneModelSpawned);
        private void OnDisable() => _droneModelSpawners.ForEach(ctx => ctx.OnDroneModelSpawned -= OnDroneModelSpawned);

        private void OnDroneModelSpawned(Drone drone) => StartCoroutine(SetAttachmentSlots());
        
        private IEnumerator SetAttachmentSlots()
        {
            _droneAttachmentSlots.Clear();
            yield return new WaitForSeconds(0.25f);
            _droneAttachmentSlots.AddRange(FindObjectsOfType<DroneAttachmentSlot>());
            _droneAttachmentSlots.ForEach(slot => slot.OnAttachmentSlotSelected += OnAttachmentSlotSelected);
        }

        private void OnAttachmentSlotSelected(DroneAttachmentSlot slot)
        {
            if (slot.GetAttachmentPoint().IsVisible())
            {
                _target = slot.GetAttachmentPoint().transform;
            }
        }

        private void Update()
        {
            if (_target == null) { return; }
            
            if (DroneLoadoutCameraMode.CurrentCameraMode == DroneLoadoutCameraMode.CameraMode.Edit)
            {
                Camera.main.transform.LookAt(_target);
            }
        }
    }
}