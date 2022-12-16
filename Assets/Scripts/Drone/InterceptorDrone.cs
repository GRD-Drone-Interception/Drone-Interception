using System.Collections.Generic;
using Drone.Component;
using Drone.Concrete;
using Drone.Decorators;
using UnityEngine;

namespace Drone
{
    public class InterceptorDrone : MonoBehaviour // rename to Drone?
    {
        [SerializeField] private DroneConfig droneConfig; // SO data
        [SerializeField] private List<AttachmentPoint> attachmentPoints; // handle in a different class?
        private IDrone _drone;

        private void Start()
        {
            _drone = DroneFactory.CreateDrone(droneConfig.droneType, droneConfig);
        }
        
        private void Update()
        {
            print($"Drone: {_drone}, Range: {_drone.Range}");
        }

        // TODO: Tidy method arguments
        public void Decorate(GameObject attachment, AttachmentPoint attachmentPoint, DroneAttachment droneAttachment)
        {
            _drone = new DroneDecorator(_drone, droneAttachment);
            attachment.transform.SetParent(attachmentPoint.transform);
            attachmentPoint.AddAttachment(attachment);
        }

        [ContextMenu("ResetConfig")]
        public void ResetConfiguration()
        {
            _drone = DroneFactory.CreateDrone(droneConfig.droneType, droneConfig);
            foreach (var point in attachmentPoints)
            {
                point.RemoveAttachment();
            }
        }
    }
}