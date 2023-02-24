using DroneLoadout.Decorators;
using UnityEngine;

namespace DroneLoadout
{
    public class AttachmentPoint : MonoBehaviour
    {
        public bool HasAttachment => _hasAttachment;

        [Range(0.025f, 2f)] [SerializeField] private float pointSize = 0.25f;
        [SerializeField] private DroneAttachmentType droneAttachmentType;
        private DroneAttachment _droneAttachment;
        private MeshRenderer _meshRenderer;
        private bool _isVisible;
        private bool _hasAttachment;

        private void Awake() => _meshRenderer = GetComponent<MeshRenderer>();
        private void Start() => _meshRenderer.enabled = false;

        private void OnDrawGizmos() => transform.localScale = new Vector3(pointSize, pointSize, pointSize);

        private void Update()
        {
            if (!_hasAttachment && IsVisible())
            {
                _meshRenderer.enabled = true;
                _meshRenderer.material.color = new Color(0, 1, 0, 0.8f);
            }
            else
            {
                _meshRenderer.enabled = false;
            }
        }

        public void AddAttachment(DroneAttachment newDroneAttachment)
        {
            if (!_hasAttachment)
            {
                newDroneAttachment.transform.parent = transform;
                _droneAttachment = newDroneAttachment;
                _hasAttachment = true;
            }
            else
            {
                Debug.Log("Attachment slot already occupied");
            }
        }

        public void RemoveAttachment()
        {
            if(_droneAttachment == null) { return; }
            Destroy(_droneAttachment.gameObject);
            _droneAttachment = null;
            _hasAttachment = false;
        }

        public void SetVisibility(bool visibility) => _isVisible = visibility;

        public bool IsVisible() => _isVisible;
        
        public DroneAttachmentType GetAttachmentType() => droneAttachmentType;
        public DroneAttachment GetDroneAttachment() => _droneAttachment;
    }
}