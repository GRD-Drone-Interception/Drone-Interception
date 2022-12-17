﻿using System.Collections.Generic;
using Drone.Component;
using Drone.Decorators;
using UnityEngine;

namespace Drone
{
    public class InterceptorDrone : MonoBehaviour // rename to Drone?
    {
        public DroneConfig DroneConfig => droneConfig;
        [SerializeField] private DroneConfig droneConfig; // SO data
        [SerializeField] private List<AttachmentPoint> attachmentPoints; // handle in a different class?
        public IDrone Drone => _drone;
        private IDrone _drone;
        public int NumOfAttachments => _numOfAttachments;
        private int _numOfAttachments;

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
            _numOfAttachments++;
        }

        [ContextMenu("ResetConfig")]
        public void ResetConfiguration()
        {
            _drone = DroneFactory.CreateDrone(droneConfig.droneType, droneConfig);
            foreach (var point in attachmentPoints)
            {
                point.RemoveAttachment();
            }
            _numOfAttachments = 0;
        }
    }
}