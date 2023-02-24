using System;
using System.Collections.Generic;
using DroneLoadout.Component;
using DroneLoadout.Decorators;
using DroneLoadout.Factory;
using DroneLoadout.Strategies;
using UnityEngine;

namespace DroneLoadout
{
    /// <summary>
    /// The core drone class component that should be attached to every drone class prefab object.
    /// </summary>
    public class Drone : MonoBehaviour
    {
        public event Action<Drone, DroneAttachment> OnDroneDecorationAdded;
        public event Action<Drone, DroneAttachment> OnDroneDecorationRemoved;
        public IDrone DecorableDrone => _decorableDrone;
        public DroneConfigData DroneConfigData => droneConfigData;
        public int NumOfMountedAttachments => _numOfMountedAttachments;
    
        [SerializeField] private DroneConfigData droneConfigData; 
        private List<AttachmentPoint> _attachmentPoints = new();
        private Stack<IDrone> _decorableDroneHistory = new();
        private IDrone _decorableDrone;
        private int _numOfMountedAttachments;

        private void Awake()
        {
            _decorableDrone = DroneFactory.CreateDrone(droneConfigData.droneType, droneConfigData);
            _attachmentPoints.AddRange(GetComponentsInChildren<AttachmentPoint>());
        }

        /// <summary>
        /// Applies a given strategy for handling the drones movement behaviour so that it can be changed
        /// dynamically at runtime.
        /// </summary>
        /// <param name="strategy">Movement algorithm i.e. Seek, Flee, Wander, Flock?</param>
        public void ApplyStrategy(IDroneManeuverBehaviour strategy)
        {
            strategy.Maneuver(this);
        }

        /// <summary>
        /// Outfits a drone with given attachment at the designated mount point. 
        /// </summary>
        /// <param name="droneAttachment">A component attached to every drone attachment object (i.e. emp), containing
        /// Scriptable Object data on said drone attachment.</param>
        /// <param name="attachmentPoint">The attachment point to which an attachment should be mounted to.</param>
        public void Decorate(DroneAttachment droneAttachment, AttachmentPoint attachmentPoint)
        {
            _decorableDroneHistory.Push(_decorableDrone);
            _decorableDrone = new DroneDecorator(_decorableDrone, droneAttachment.Data);
            droneAttachment.transform.SetParent(attachmentPoint.transform);
            droneAttachment.transform.position = attachmentPoint.transform.position; // new
            droneAttachment.gameObject.layer = LayerMask.NameToLayer("Focus");
            attachmentPoint.AddAttachment(droneAttachment);
            _numOfMountedAttachments++;
            OnDroneDecorationAdded?.Invoke(this, attachmentPoint.GetDroneAttachment());
        }

        public void RemoveAttachment(AttachmentPoint attachmentPoint)
        {
            _decorableDrone = _decorableDroneHistory.Pop();
            _numOfMountedAttachments--;
            OnDroneDecorationRemoved?.Invoke(this, attachmentPoint.GetDroneAttachment());
            attachmentPoint.RemoveAttachment();
        }

        /// <summary>
        /// Resets all currently mounted attachments on a drone.
        /// </summary>
        public void ResetConfiguration()
        {
            _decorableDrone = DroneFactory.CreateDrone(droneConfigData.droneType, droneConfigData);
            _numOfMountedAttachments = 0;
            foreach (var point in _attachmentPoints)
            {
                OnDroneDecorationRemoved?.Invoke(this, point.GetDroneAttachment());
                point.RemoveAttachment();
            }
        }

        public List<AttachmentPoint> GetAttachmentPoints() => _attachmentPoints;
    }
}
