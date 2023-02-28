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
    /// The core drone class component that should be attached to every drone class prefab.
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
        private Dictionary<AttachmentPoint, DroneAttachment> _attachmentPointDictionary = new();

        //private Dictionary<AttachmentPoint, IDrone> _decorableDroneHistory = new();
        private Stack<IDrone> _decorableDroneHistory = new();
        private IDrone _decorableDrone;
        private int _numOfMountedAttachments;

        private void Awake()
        {
            _decorableDrone = DroneFactory.CreateDrone(droneConfigData.droneType, droneConfigData);
            _attachmentPoints.AddRange(GetComponentsInChildren<AttachmentPoint>());
        }

        private void Update()
        {
            Debug.Log($"Drone Cost: {_decorableDrone.Cost}");
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
            //_decorableDroneHistory.Add(attachmentPoint, _decorableDrone);
            _attachmentPointDictionary.Add(attachmentPoint, droneAttachment);
            _decorableDrone = new DroneDecorator(_decorableDrone, droneAttachment.Data);
            droneAttachment.transform.SetParent(attachmentPoint.transform);
            droneAttachment.transform.position = attachmentPoint.transform.position;
            droneAttachment.gameObject.layer = LayerMask.NameToLayer("Focus");
            attachmentPoint.AddDroneAttachment(droneAttachment);
            _numOfMountedAttachments++;
            OnDroneDecorationAdded?.Invoke(this, attachmentPoint.GetDroneAttachment());
        }

        public void RemoveAttachment(AttachmentPoint attachmentPoint)
        {
            if (attachmentPoint == null || !attachmentPoint.HasAttachment)
            {
                Debug.Log("No attachment to remove, or the attachment point is null!");
                return;
            }
            
            // TODO: Fix this?
            // Works if components all cost the same amount, however if a component is removed that costs a different
            // amount to the one that was last added then when the drone is returned to the state the drone was
            // before the last component was added the calculation is wrong.
            // Example
            // Purchase component 1 at a cost of 10
            // 10 is pushed to the stack (Drone cost goes up from 100 to 110)
            // Purchase component 2 at a cost of 5
            // 5 is pushed to the stack (Drone cost goes up from 110 to 115)
            // Remove component 1 (which should deposit 10 back into the player budget and make drone cost now 105)
            // Pop stack (5)
            // Drone cost is now 110 because 5 was the last value put into the stack and so is the first out (LIFO)
            _decorableDrone = _decorableDroneHistory.Pop();
            //_decorableDrone = _decorableDroneHistory[attachmentPoint];

            _attachmentPointDictionary.Remove(attachmentPoint);
            _numOfMountedAttachments--;
            OnDroneDecorationRemoved?.Invoke(this, attachmentPoint.GetDroneAttachment());
            attachmentPoint.RemoveDroneAttachment();
        }

        /// <summary>
        /// Resets all currently mounted attachments on a drone.
        /// </summary>
        public void ResetConfiguration()
        {
            _decorableDrone = DroneFactory.CreateDrone(droneConfigData.droneType, droneConfigData);
            _decorableDroneHistory.Clear();
            _attachmentPointDictionary.Clear();
            _numOfMountedAttachments = 0;
            foreach (var point in _attachmentPoints)
            {
                if (point.GetDroneAttachment())
                {
                    OnDroneDecorationRemoved?.Invoke(this, point.GetDroneAttachment());
                    point.RemoveDroneAttachment();
                }
            }
        }

        public List<AttachmentPoint> GetAttachmentPoints() => _attachmentPoints;
    }
}
