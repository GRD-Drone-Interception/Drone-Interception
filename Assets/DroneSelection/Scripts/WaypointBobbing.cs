using UnityEngine;

namespace DroneSelection.Scripts
{
    public class WaypointBobbing : MonoBehaviour
    {
        [SerializeField] private float amplitude = 0.5f; // How much the arrow should bob up and down
        [SerializeField] private float speed = 1f;
        private Camera _tacticalCamera;
        private Vector3 _startingPosition;
        private bool _isBobbing;

        private void Awake()
        {
            _tacticalCamera = Camera.main;
        }

        private void Start()
        {
            _startingPosition = transform.position;
            Pause();
        }

        private void Update()
        {
            if(!_isBobbing)
            {
                return;
            }
            
            // Bobbing calculation
            float y = _startingPosition.y + amplitude * Mathf.Sin(speed * Time.time);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

            // Get the direction from the game object to the camera
            Vector3 directionToCamera = _tacticalCamera.transform.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(directionToCamera, Vector3.up) * Quaternion.Euler(0, 0, 90);
            transform.rotation = newRotation;
        }

        public void SetStartPosition(Vector3 startPos)
        {
            _startingPosition = startPos;
        }

        public void Play()
        {
            _isBobbing = true;
        }

        private void Pause()
        {
            _isBobbing = false;
        }
    }
}