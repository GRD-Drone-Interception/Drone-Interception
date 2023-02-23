using System;
using System.Collections.Generic;
using DroneSetup.Component;
using DroneSetup.Decorators;
using DroneSetup.Factory;
using DroneSetup.Strategies;
using UnityEngine;

namespace DroneSetup
{
    /// <summary>
    /// The core drone class component that should be attached to every drone class prefab object.
    /// </summary>
    public class Drone : MonoBehaviour
    {
        public event Action<Drone> OnDroneDecorated;
        public IDrone DecorableDrone => _decorableDrone;
        public DroneConfigSO DroneConfigSo => droneConfigSo;
        public int NumOfAttachments => _numOfAttachments;
    
        [SerializeField] private DroneConfigSO droneConfigSo; 
        [SerializeField] private List<AttachmentPoint> attachmentPoints;
        private IDrone _decorableDrone;
        private int _numOfAttachments;

        private void Awake() => _decorableDrone = DroneFactory.CreateDrone(droneConfigSo.droneType, droneConfigSo);
        
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
            _decorableDrone = new DroneDecorator(_decorableDrone, droneAttachment.AttachmentSo);
            droneAttachment.transform.SetParent(attachmentPoint.transform);
            droneAttachment.transform.position = attachmentPoint.transform.position; // new
            droneAttachment.gameObject.layer = LayerMask.NameToLayer("Focus");
            attachmentPoint.AddAttachment(droneAttachment);
            _numOfAttachments++;
            OnDroneDecorated?.Invoke(this);
        }

        /// <summary>
        /// Resets all currently mounted attachments on a drone.
        /// </summary>
        public void ResetConfiguration()
        {
            _decorableDrone = DroneFactory.CreateDrone(droneConfigSo.droneType, droneConfigSo);
            attachmentPoints.ForEach(point => point.RemoveAttachment());
            _numOfAttachments = 0;
            OnDroneDecorated?.Invoke(this);
        }

        public List<AttachmentPoint> GetAttachmentPoints()
        {
            return attachmentPoints;
        }
    }
}
