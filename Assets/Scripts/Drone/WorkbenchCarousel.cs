using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Drone
{
    public class WorkbenchCarousel : MonoBehaviour
    {
        public int PodiumCount => podiumNodeManager.PodiumNodes.Count;
        public List<PodiumNode> PodiumNodes => podiumNodeManager.PodiumNodes;
        public int CurrentPodiumNodeIndex => _currentNodeIndex;
        public bool IsMoving => _isMoving;
        
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private Button addDroneButton;
        [SerializeField] private Button carouselMoveLeftButton;
        //[SerializeField] private Button carouselMoveRightButton;
        [SerializeField] private PodiumNodeManager podiumNodeManager;
        private Vector3 _endPosition;
        private int _currentNodeIndex;
        private bool _isMoving = false;

        private void Start()
        {
            _currentNodeIndex = 0;
            _endPosition = podiumNodeManager.PodiumNodes[_currentNodeIndex].transform.position;
            StartCoroutine(MoveCarouselCoroutine(_endPosition, moveSpeed));
        }

        private void OnEnable()
        {
            addDroneButton.onClick.AddListener(MoveCarouselRightOnButtonPress);
            //carouselMoveRightButton.onClick.AddListener(MoveCarouselLeftOnButtonPress);
            carouselMoveLeftButton.onClick.AddListener(MoveCarouselLeftOnButtonPress);
        }
    
        private void OnDisable()
        {
            addDroneButton.onClick.RemoveListener(MoveCarouselRightOnButtonPress);
            //carouselMoveRightButton.onClick.RemoveListener(MoveCarouselLeftOnButtonPress);
            carouselMoveLeftButton.onClick.RemoveListener(MoveCarouselLeftOnButtonPress);
        }

        private void MoveCarouselRightOnButtonPress()
        {
            // Stop moving the carousel if it exceeds the number of podiums available
            if (_currentNodeIndex < podiumNodeManager.PodiumNodes.Count-1)
            {
                _endPosition = podiumNodeManager.PodiumNodes[++_currentNodeIndex].transform.position;
                StartCoroutine(MoveCarouselCoroutine(_endPosition, moveSpeed));
            }
        }
    
        private void MoveCarouselLeftOnButtonPress()
        {
            _endPosition = podiumNodeManager.PodiumNodes[--_currentNodeIndex].transform.position;
            StartCoroutine(MoveCarouselCoroutine(_endPosition, moveSpeed));
        }

        private IEnumerator MoveCarouselCoroutine(Vector3 endPosition, float speed)
        {
            _isMoving = true;
            addDroneButton.gameObject.SetActive(false);
            carouselMoveLeftButton.gameObject.SetActive(false);
            //carouselMoveRightButton.gameObject.SetActive(false);
            float scale = (transform.localScale.x + transform.localScale.y + transform.localScale.z)/3;
            float speedAtScale = scale * speed;
            while (Vector3.Distance(transform.position,endPosition) > speedAtScale * Time.deltaTime)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, speedAtScale * Time.deltaTime);
                yield return 0;
            }

            addDroneButton.gameObject.SetActive(_currentNodeIndex <= podiumNodeManager.PodiumNodes.Count - 1);
            
            // If next drone in dictionary is already occupied
                // Set addDroneButton text to >>
            
            carouselMoveLeftButton.gameObject.SetActive(true);
            //carouselMoveRightButton.gameObject.SetActive(true);

            transform.position = endPosition;
            _isMoving = false;
        }
    }
}
