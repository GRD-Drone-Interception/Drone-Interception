using System;
using UnityEngine;

namespace Drone
{
    public class AttachmentPoint : MonoBehaviour
    {
        public bool HasAttachment => _hasAttachment;
        private bool _hasAttachment = false;

        [Range(0.15f, 0.5f)] [SerializeField] private float pointSize = 0.25f;  
        [SerializeField] private GameObject _attachment;
        private MeshRenderer _meshRenderer;
        private bool _isVisible;

        private void Awake() => _meshRenderer = GetComponent<MeshRenderer>();
        private void Start() => _meshRenderer.enabled = false;

        private void OnDrawGizmos()
        {
            //Gizmos.color = Color.green;
            //Gizmos.DrawWireCube(transform.position, transform.localScale * pointSize);
            transform.localScale = new Vector3(pointSize, pointSize, pointSize);
        }
        
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

        public void AddAttachment(GameObject attachment)
        {
            if (!_hasAttachment)
            {
                attachment.transform.parent = transform;
                _attachment = attachment;
                _hasAttachment = true;
            }
            else
            {
                Debug.Log("Attachment slot already occupied");
            }
        }

        public void RemoveAttachment()
        {
            Destroy(_attachment);
            _attachment = null;
            _hasAttachment = false;
        }

        public void SetVisibility(bool visibility)
        {
            _isVisible = visibility;
        }

        public bool IsVisible()
        {
            return _isVisible;
        }
    }
}