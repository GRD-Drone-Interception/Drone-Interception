using System;
using System.Collections.Generic;
using UnityEngine;

namespace Drone
{
    public class QuadcopterDrone : MonoBehaviour
    {
        public DroneConfig droneConfig; // SO data
        public DroneAttachment primaryAttachment; // SO data
        public DroneAttachment secondaryAttachment; // SO data

        [SerializeField] private AttachmentPoint attachmentPoint;
        //private List<AttachmentPoint> attachmentPoints;

        private IDrone _drone;
        private bool _isDecorated;

        private void Start()
        {
            _drone = new Drone(droneConfig);
        }

        [ContextMenu("Decorate")]
        public void Decorate()
        {
            if (!_isDecorated)
            {
                _drone = new DroneDecorator(_drone, primaryAttachment);
                _drone = new DroneDecorator(_drone, secondaryAttachment);
                var attachment = Instantiate(primaryAttachment.attachmentPrefab, attachmentPoint.transform.position, Quaternion.identity);
                attachment.transform.SetParent(attachmentPoint.transform);
                _isDecorated = true;
            }
            else
            {
                Debug.LogWarning("This Drone already has an attachment!");
            }
            //_drone = new DroneDecorator(new DroneDecorator(_drone, primaryAttachment), secondaryAttachment);
        }

        [ContextMenu("ResetConfig")]
        public void ResetConfiguration()
        {
            _drone = new Drone(droneConfig);
            Destroy(attachmentPoint.transform.GetChild(0).gameObject);
            _isDecorated = false;
        }

        private void Update()
        {
            print($"Drone: {_drone}, Range: {_drone.Range}");
        }
    }
}
