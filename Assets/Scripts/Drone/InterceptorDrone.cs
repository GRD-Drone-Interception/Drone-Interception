using System.Collections.Generic;
using Drone.Component;
using Drone.Decorators;
using Drone.Strategies;
using UnityEngine;

namespace Drone
{
    public class InterceptorDrone : MonoBehaviour
    {
        public IDrone Drone => _drone;
        public DroneConfigSO DroneConfigSo => droneConfigSo;
        public int NumOfAttachments => _numOfAttachments;
        
        [SerializeField] private DroneConfigSO droneConfigSo; 
        [SerializeField] private List<AttachmentPoint> attachmentPoints;
        private IDrone _drone;
        private int _numOfAttachments;

        private void Start() => _drone = DroneFactory.CreateDrone(droneConfigSo.droneType, droneConfigSo);

        public void ApplyStrategy(IDroneManeuverBehaviour strategy)
        {
            strategy.Maneuver(this);
        }

        public void Decorate(DroneAttachment droneAttachment, AttachmentPoint attachmentPoint)
        {
            _drone = new DroneDecorator(_drone, droneAttachment.AttachmentSo);
            droneAttachment.transform.SetParent(attachmentPoint.transform);
            attachmentPoint.AddAttachment(droneAttachment);
            _numOfAttachments++;
        }
        
        public void ResetConfiguration()
        {
            _drone = DroneFactory.CreateDrone(droneConfigSo.droneType, droneConfigSo);
            attachmentPoints.ForEach(point => point.RemoveAttachment());
            _numOfAttachments = 0;
        }
    }
}