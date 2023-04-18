using DroneLoadout.Decorators;
using UnityEngine;

namespace DroneLoadout.Scripts
{
    /// <summary>
    /// Attached to every drone attachment prefab, and should be added to any new ones that are made.
    /// </summary>
    public class DroneAttachment : MonoBehaviour
    {
        public DroneAttachmentData Data => droneData;
        
        [SerializeField] private DroneAttachmentData droneData;
        private Material _material;
        private Color _initialColor;
        private const float PulseSpeed = 1.75f;
        private bool _pulsate;

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
            _initialColor = GetComponent<Renderer>().material.color;
        }

        private void Update()
        {
            if (_pulsate)
            {
                float pulse = Mathf.PingPong(Time.time * PulseSpeed, 1f);
                _material.color = Color.Lerp(new Color(0, 0.5f, 0, 1f), new Color(0, 1f, 0.67f, 1f), pulse);
            }
        }
        
        public void Pulsate(bool pulsate)
        {
            _pulsate = pulsate;
        }

        public void ResetColour()
        {
            _pulsate = false;
            _material.color = _initialColor;
        }

        /*public void Highlight()
        {
            _material.color = new Color(0, 1f, 0.67f, 0.60f);
        }

        public void Unhighlight()
        {
            _material.color = new Color(1, 1, 1, 1f);
        }*/
    }
}
