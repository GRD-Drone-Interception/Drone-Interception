using System.Collections.Generic;
using Drones.Component;
using Drones.Decorators;
using Drones.Factory;
using Drones.Strategies;
using UnityEngine;

namespace Drones
{
    /// <summary>
    /// The core drone class component that is attached to every drone object.
    /// </summary>
    public class Drone : MonoBehaviour
    {
        public IDrone DecorableDrone => _decorableDrone;
        public DroneConfigSO DroneConfigSo => droneConfigSo;
        public int NumOfAttachments => _numOfAttachments;
    
        [SerializeField] private DroneConfigSO droneConfigSo; 
        [SerializeField] private List<AttachmentPoint> attachmentPoints;
        private IDrone _decorableDrone;
        private int _numOfAttachments;

        private void Start() => _decorableDrone = DroneFactory.CreateDrone(droneConfigSo.droneType, droneConfigSo);
        
        /// <summary>
        /// Applies a given strategy for handling the drones movement behaviour.
        /// </summary>
        /// <param name="strategy">Movement algorithm</param>
        public void ApplyStrategy(IDroneManeuverBehaviour strategy)
        {
            strategy.Maneuver(this);
        }

        /// <summary>
        /// Outfits a drone with given attachment at the designated mount point. 
        /// </summary>
        /// <param name="droneAttachment">A component attached to every drone attachment object (i.e. emp), containing
        /// Scriptable Object data on said drone attachment.</param>
        /// <param name="attachmentPoint">The attachment point to which an attachment should be mounted.</param>

        public void Decorate(DroneAttachment droneAttachment, AttachmentPoint attachmentPoint)
        {
            _decorableDrone = new DroneDecorator(_decorableDrone, droneAttachment.AttachmentSo);
            droneAttachment.transform.SetParent(attachmentPoint.transform);
            attachmentPoint.AddAttachment(droneAttachment);
            _numOfAttachments++;
        }

        /// <summary>
        /// Resets all currently mounted attachments on a drone.
        /// </summary>
        public void ResetConfiguration()
        {
            _decorableDrone = DroneFactory.CreateDrone(droneConfigSo.droneType, droneConfigSo);
            attachmentPoints.ForEach(point => point.RemoveAttachment());
            _numOfAttachments = 0;
        }
    }
}
