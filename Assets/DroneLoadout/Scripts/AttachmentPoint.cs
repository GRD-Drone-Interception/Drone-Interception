using UnityEngine;

namespace DroneLoadout.Scripts
{
    public class AttachmentPoint : MonoBehaviour
    {
        public bool HasAttachment => _hasAttachment;

        [Range(0.025f, 2f)] [SerializeField] private float pointSize = 0.25f;
        [SerializeField] private DroneAttachmentType droneAttachmentType;
        private DroneAttachment _droneAttachment;
        private MeshRenderer _meshRenderer;
        private MeshFilter _meshFilter;
        private bool _isVisible;
        private bool _hasAttachment;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();

            // If this attachment point object has a child, add it's drone attachment and decorate the drone
            if (transform.childCount > 0)
            {
                var childDroneAttachment = transform.GetChild(0).GetComponent<DroneAttachment>();
                GetComponentInParent<Drone>().Decorate(childDroneAttachment, this);
            }
        }

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

        public void AddDroneAttachment(DroneAttachment newDroneAttachment)
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

        public void RemoveDroneAttachment()
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

        public void SetMesh(Mesh mesh)
        {
            _meshFilter.mesh = mesh;
        }
    }
}