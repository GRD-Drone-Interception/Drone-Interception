using System;
using System.Collections.Generic;
using Drone.Component;
using Drone.Decorators;
using UnityEngine;

namespace Drone
{
    public class InterceptorDrone : MonoBehaviour // rename to Drone?
    {
        public event Action<InterceptorDrone> OnDroneDecorated;
        public DroneConfigSO DroneConfigSo => droneConfigSo;
        [SerializeField] private DroneConfigSO droneConfigSo; // SO data
        [SerializeField] private List<AttachmentPoint> attachmentPoints; // handle in a different class?
        public IDrone Drone => _drone;
        private IDrone _drone;
        public int NumOfAttachments => _numOfAttachments;
        private int _numOfAttachments;

        private void Start()
        {
            _drone = DroneFactory.CreateDrone(droneConfigSo.droneType, droneConfigSo);
        }

        // TODO: Tidy method arguments
        public void Decorate(GameObject attachment, AttachmentPoint attachmentPoint, DroneAttachmentSO droneAttachmentSo)
        {
            _drone = new DroneDecorator(_drone, droneAttachmentSo);
            attachment.transform.SetParent(attachmentPoint.transform);
            attachmentPoint.AddAttachment(attachment);
            _numOfAttachments++;
            OnDroneDecorated?.Invoke(this);
        }
        
        public void ResetConfiguration()
        {
            _drone = DroneFactory.CreateDrone(droneConfigSo.droneType, droneConfigSo);
            foreach (var point in attachmentPoints)
            {
                point.RemoveAttachment();
            }
            _numOfAttachments = 0;
        }
    }
}