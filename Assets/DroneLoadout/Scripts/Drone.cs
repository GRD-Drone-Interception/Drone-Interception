﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using DroneBehaviours.Scripts;
using DroneLoadout.Decorators;
using DroneLoadout.Factory;
using DroneLoadout.Strategies;
using UnityEngine;

namespace DroneLoadout.Scripts
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
        [SerializeField] private List<MeshRenderer> meshRenderers;
        private readonly List<AttachmentPoint> _attachmentPoints = new();
        private readonly Dictionary<AttachmentPoint, DroneAttachment> _mountedAttachmentPointsDictionary = new();
        private readonly Dictionary<DroneAttachmentType, int> _attachmentTypeCount = new();
        private readonly List<Color> _originalMaterialColours = new();
        private Color _paintJob;
        private PlayerTeam _playerTeam;

        private void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            DecorableDrone = DroneFactory.CreateDrone(droneConfigData.DroneType, droneConfigData);
            _attachmentPoints.AddRange(GetComponentsInChildren<AttachmentPoint>());
            meshRenderers.ForEach(ctx => _originalMaterialColours.Add(ctx.material.color));
            _paintJob = _originalMaterialColours[0];
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

        public void MoveToTarget(Vector3 targetDestination)
        {
            
        }

        public void AttackTarget(Drone drone)
        {
            
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
            _mountedAttachmentPointsDictionary.Add(attachmentPoint, droneAttachment);
            DecorableDrone = new DroneDecorator(DecorableDrone, droneAttachment.Data);
            droneAttachment.transform.SetParent(attachmentPoint.transform);
            droneAttachment.transform.SetPositionAndRotation(attachmentPoint.transform.position, attachmentPoint.transform.rotation);
            droneAttachment.gameObject.layer = LayerMask.NameToLayer("Focus");
            
            if (droneAttachment.Data.DroneBehaviours.Count > 0)
            {
                // Check if a behaviour has already been added for the given droneAttachmentType
                if (!_attachmentTypeCount.ContainsKey(droneAttachment.Data.AttachmentType))
                {
                    _attachmentTypeCount.Add(droneAttachment.Data.AttachmentType, 0);
                    behaviours.Add(droneAttachment.Data.DroneBehaviours[0]);
                }
                // Increment the count for the attachment type
                _attachmentTypeCount[droneAttachment.Data.AttachmentType]++;
            }

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

            // Decrement the count for the attachment type
            if (_attachmentTypeCount.ContainsKey(attachmentPoint.GetAttachmentType()))
            {
                _attachmentTypeCount[attachmentPoint.GetAttachmentType()]--;
                if (_attachmentTypeCount[attachmentPoint.GetAttachmentType()] <= 0)
                {
                    // If there are no more attachments of this type, remove the associated behaviour
                    _attachmentTypeCount.Remove(attachmentPoint.GetAttachmentType());
                    behaviours.Remove(attachmentPoint.GetDroneAttachment().Data.DroneBehaviours[0]);
                }
            }

            // Remove ALL decorations
            DecorableDrone = DroneFactory.CreateDrone(droneConfigData.DroneType, droneConfigData);
            
            //Redecorate ALL other attachments besides the one being queried (not ideal) // TODO: Clean this
            foreach (var ap in _attachmentPoints.Where(ap => ap != attachmentPoint))
            {
                if (ap.HasAttachment)
                {
                    DecorableDrone = new DroneDecorator(DecorableDrone, ap.GetDroneAttachment().Data);
                }
            }

            _mountedAttachmentPointsDictionary.Remove(attachmentPoint);
            NumOfMountedAttachments--;
            OnDroneDecorationRemoved?.Invoke(this, attachmentPoint.GetDroneAttachment());
            attachmentPoint.RemoveDroneAttachment();
        }

        /// <summary>
        /// Resets all currently mounted attachments on a drone.
        /// </summary>
        public void ResetConfiguration()
        {
            DecorableDrone = DroneFactory.CreateDrone(droneConfigData.DroneType, droneConfigData);
            _mountedAttachmentPointsDictionary.Clear();
            _attachmentTypeCount.Clear();
            NumOfMountedAttachments = 0;
            ResetPaintJob();

            foreach (var point in _attachmentPoints)
            {
                if (point.GetDroneAttachment())
                {
                    point.GetDroneAttachment().Data.DroneBehaviours.ForEach(behaviour => behaviours.Remove(behaviour)); 
                    OnDroneDecorationRemoved?.Invoke(this, point.GetDroneAttachment());
                    point.RemoveDroneAttachment();
                }
            }
        }

        public void Paint(Color colour)
        {
            _paintJob = colour;
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.material.color = colour;
            }
        }

        public void ResetPaintJob()
        {
            foreach (var meshRenderer in meshRenderers)
            {
                foreach (var colour in _originalMaterialColours)
                {
                    meshRenderer.material.color = colour;
                }
            }
        }

        public Color GetPaintJob()
        {
            return _paintJob;
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
        
        public Dictionary<AttachmentPoint, DroneAttachment> MountedAttachmentPointsDictionary() => _mountedAttachmentPointsDictionary;
    }
}
