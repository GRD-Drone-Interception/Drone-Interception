using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using DroneBehaviours.Scripts;
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
        public IDrone DecorableDrone { get; private set; }
        public DroneConfigData DroneConfigData => droneConfigData;
        public Rigidbody Rb { get; private set; }
        public int NumOfMountedAttachments { get; private set; }

        [SerializeField] private DroneConfigData droneConfigData; 
        [SerializeField] private List<DroneBehaviour> behaviours = new();
        private readonly List<AttachmentPoint> _attachmentPoints = new();
        private readonly Dictionary<AttachmentPoint, DroneAttachment> _attachmentPointDictionary = new();
        private PlayerTeam _playerTeam;

        private void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            DecorableDrone = DroneFactory.CreateDrone(droneConfigData.droneType, droneConfigData);
            _attachmentPoints.AddRange(GetComponentsInChildren<AttachmentPoint>());
        }
        
        private void Update()
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.UpdateBehaviour(this);
            }
        }

        private void FixedUpdate()
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.FixedUpdateBehaviour(this);
            }
        }

        public void AddBehaviour(DroneBehaviour behaviour)
        {
            behaviours.Add(behaviour);
        }

        public void RemoveBehaviour(DroneBehaviour behaviour)
        {
            behaviours.Remove(behaviour);
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
            _attachmentPointDictionary.Add(attachmentPoint, droneAttachment);
            DecorableDrone = new DroneDecorator(DecorableDrone, droneAttachment.Data);
            droneAttachment.transform.SetParent(attachmentPoint.transform);
            droneAttachment.transform.SetPositionAndRotation(attachmentPoint.transform.position, attachmentPoint.transform.rotation);
            droneAttachment.gameObject.layer = LayerMask.NameToLayer("Focus");
            droneAttachment.Data.DroneBehaviours.ForEach(AddBehaviour);
            attachmentPoint.AddDroneAttachment(droneAttachment);
            NumOfMountedAttachments++;
            OnDroneDecorationAdded?.Invoke(this, attachmentPoint.GetDroneAttachment());
        }

        public void Undecorate(AttachmentPoint attachmentPoint)
        {
            if (attachmentPoint == null || !attachmentPoint.HasAttachment)
            {
                Debug.Log("No attachment to remove, or the attachment point is null!");
                return;
            }
            
            attachmentPoint.GetDroneAttachment().Data.DroneBehaviours.ForEach(RemoveBehaviour); // Here?

            // Remove ALL decorations
            DecorableDrone = DroneFactory.CreateDrone(droneConfigData.droneType, droneConfigData);
            
            //Redecorate ALL other attachments besides the one being queried (not ideal) // TODO: Clean this
            foreach (var ap in _attachmentPoints.Where(ap => ap != attachmentPoint))
            {
                if (ap.HasAttachment)
                {
                    DecorableDrone = new DroneDecorator(DecorableDrone, ap.GetDroneAttachment().Data);
                }
            }

            _attachmentPointDictionary.Remove(attachmentPoint);
            NumOfMountedAttachments--;
            OnDroneDecorationRemoved?.Invoke(this, attachmentPoint.GetDroneAttachment());
            attachmentPoint.RemoveDroneAttachment();
        }

        /// <summary>
        /// Resets all currently mounted attachments on a drone.
        /// </summary>
        public void ResetConfiguration()
        {
            DecorableDrone = DroneFactory.CreateDrone(droneConfigData.droneType, droneConfigData);
            _attachmentPointDictionary.Clear();
            NumOfMountedAttachments = 0;
            foreach (var point in _attachmentPoints)
            {
                if (point.GetDroneAttachment())
                {
                    point.GetDroneAttachment().Data.DroneBehaviours.ForEach(RemoveBehaviour); // Here?
                    OnDroneDecorationRemoved?.Invoke(this, point.GetDroneAttachment());
                    point.RemoveDroneAttachment();
                }
            }
        }

        public void SetTeam(PlayerTeam team)
        {
            _playerTeam = team;
        }

        public PlayerTeam GetTeam()
        {
            return _playerTeam;
        }

        public List<AttachmentPoint> GetAttachmentPoints() => _attachmentPoints;
    }
}
