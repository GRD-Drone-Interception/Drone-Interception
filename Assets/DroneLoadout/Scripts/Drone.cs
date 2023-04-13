using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using DroneBehaviours.Scripts;
using DroneLoadout.Decorators;
using DroneLoadout.Factory;
using DroneLoadout.Strategies;
using UnityEngine;
using UnityEngine.Serialization;

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
        public Rigidbody Rb { get; private set; }
        public int NumOfMountedAttachments { get; private set; }

        [Header("Drone Configuration")]
        [SerializeField] private DroneConfigData droneConfigData;
        [Space(5)]
        
        [Header("Behaviours")]
        [SerializeField] private List<DroneBehaviour> defaultBehaviours = new();
        [SerializeField] private List<DroneBehaviour> dynamicBehaviours = new();
        [Space(5)]
        
        [Header("Decorations")]
        [FormerlySerializedAs("meshRenderers")] [SerializeField] private List<MeshRenderer> decalMeshRenderers;
        [SerializeField] private Material outlineMaterial;
        private Dictionary<MeshRenderer, Material[]> _originalMaterials = new Dictionary<MeshRenderer, Material[]>();
        private Material[] _outlinedMaterials;
        private MeshRenderer _meshRenderer;

        private readonly List<AttachmentPoint> _attachmentPoints = new();
        private readonly Dictionary<DroneAttachmentType, int> _attachmentTypeCount = new();
        private Dictionary<int, DroneAttachmentType> _attachmentPointTypeIndex = new();
        private readonly List<Color> _originalMaterialColours = new();
        private Color _paintJob;
        private PlayerTeam _playerTeam;

        private void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            DecorableDrone = DroneFactory.CreateDrone(droneConfigData.DroneType, droneConfigData);
            _attachmentPoints.AddRange(GetComponentsInChildren<AttachmentPoint>());
            decalMeshRenderers.ForEach(ctx => _originalMaterialColours.Add(ctx.material.color));
            _paintJob = _originalMaterialColours[0];
            
            MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
            if (meshRenderer != null)
            {
                // Save the original materials for this mesh renderer if they haven't already been saved
                if (!_originalMaterials.ContainsKey(meshRenderer))
                {
                    _originalMaterials[meshRenderer] = meshRenderer.materials;
                }
                
                // Create an array of materials with the outline material to use on this mesh renderer
                _outlinedMaterials = new Material[meshRenderer.materials.Length];
                for (int i = 0; i < _outlinedMaterials.Length; i++) 
                {
                    _outlinedMaterials[i] = outlineMaterial;
                }
                
                // Update the materials on this drone and all of its children to use the outlined materials
                UpdateMaterialsRecursively(transform);
            }
        }

        private void Update()
        {
            foreach (var behaviour in defaultBehaviours)
            {
                behaviour.UpdateBehaviour(this);
            }
            foreach (var behaviour in dynamicBehaviours)
            {
                behaviour.UpdateBehaviour(this);
            }
        }

        private void FixedUpdate()
        {
            foreach (var behaviour in defaultBehaviours)
            {
                behaviour.FixedUpdateBehaviour(this);
            }
            foreach (var behaviour in dynamicBehaviours)
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
                    dynamicBehaviours.Add(droneAttachment.Data.DroneBehaviours[0]);
                }
                // Increment the count for the attachment type
                _attachmentTypeCount[droneAttachment.Data.AttachmentType]++;
            }

            _attachmentPointTypeIndex.Add(_attachmentPoints.IndexOf(attachmentPoint), droneAttachment.Data.AttachmentType);
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
                    dynamicBehaviours.Remove(attachmentPoint.GetDroneAttachment().Data.DroneBehaviours[0]);
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

            _attachmentPointTypeIndex.Remove(_attachmentPoints.IndexOf(attachmentPoint));
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
            _attachmentTypeCount.Clear();
            _attachmentPointTypeIndex.Clear();
            NumOfMountedAttachments = 0;
            ResetPaintJob();

            foreach (var point in _attachmentPoints)
            {
                if (point.GetDroneAttachment())
                {
                    point.GetDroneAttachment().Data.DroneBehaviours.ForEach(behaviour => dynamicBehaviours.Remove(behaviour)); 
                    OnDroneDecorationRemoved?.Invoke(this, point.GetDroneAttachment());
                    point.RemoveDroneAttachment();
                }
            }
        }
        
        public void Select()
        {
            foreach (var meshRenderer in _originalMaterials.Keys)
            {
                meshRenderer.materials = _outlinedMaterials;
            }
        }

        public void Unselect()
        {
            foreach (var meshRenderer in _originalMaterials.Keys)
            {
                meshRenderer.materials = _originalMaterials[meshRenderer];
            }
        }

        public void Paint(Color colour)
        {
            _paintJob = colour;
            foreach (var meshRenderer in decalMeshRenderers)
            {
                meshRenderer.material.color = colour;
            }
        }

        public void ResetPaintJob()
        {
            foreach (var meshRenderer in decalMeshRenderers)
            {
                foreach (var colour in _originalMaterialColours)
                {
                    meshRenderer.material.color = colour;
                }
            }
            _paintJob = _originalMaterialColours[0];
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

        /// <summary>
        /// Returns the name of the drone model
        /// </summary>
        public string GetName()
        {
            return droneConfigData.DroneName;
        }

        /// <summary>
        /// Returns the drone type of the drone model
        /// </summary>
        public DroneType GetDroneType()
        {
            return droneConfigData.DroneType;
        }

        public List<AttachmentPoint> GetAttachmentPoints() => _attachmentPoints;

        public Dictionary<int, DroneAttachmentType> GetAttachmentPointTypeIndex() => _attachmentPointTypeIndex;
        
        private void UpdateMaterialsRecursively(Transform currentTransform)
        {
            var meshRenderer = currentTransform.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                if (!_originalMaterials.ContainsKey(meshRenderer))
                {
                    _originalMaterials[meshRenderer] = meshRenderer.materials;
                }
                meshRenderer.materials = _outlinedMaterials;
            }
            
            // Recurse over children
            foreach (Transform child in currentTransform)
            {
                UpdateMaterialsRecursively(child);
            }
        }
    }
}
