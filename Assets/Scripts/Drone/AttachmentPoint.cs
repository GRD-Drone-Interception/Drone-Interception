using System;
using UnityEngine;

namespace Drone
{
    public class AttachmentPoint : MonoBehaviour
    {
        private bool _hasAttachment = false;
        
        [Range(0.15f, 0.5f)] [SerializeField] private float pointSize = 0.25f;  
        [SerializeField] private GameObject _attachment;
        private MeshRenderer _meshRenderer;

        private void Awake() => _meshRenderer = GetComponent<MeshRenderer>();
        private void Start() => _meshRenderer.enabled = false;

        private void OnDrawGizmos()
        {
            //Gizmos.color = Color.green;
            //Gizmos.DrawWireCube(transform.position, transform.localScale * pointSize);
            transform.localScale = new Vector3(pointSize, pointSize, pointSize);
        }

        private void OnMouseOver()
        {
            _meshRenderer.enabled = true;
            _meshRenderer.material.color = new Color(0,1,0,0.8f);
        }

        private void OnMouseExit()
        {
            _meshRenderer.enabled = false;
            _meshRenderer.material.color = Color.white;
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
    }
}