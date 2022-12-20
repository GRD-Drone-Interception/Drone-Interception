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
        [SerializeField] private Button carouselMoveLeftButton;
        [SerializeField] private Button carouselMoveRightButton;
        [SerializeField] private PodiumNodeManager podiumNodeManager;
        private Vector3 _endPosition;
        private int _currentNodeIndex;
        private bool _isMoving = false;

        private void Start()
        {
            _endPosition = podiumNodeManager.PodiumNodes[_currentNodeIndex].transform.position;
            StartCoroutine(MoveCarouselCoroutine(_endPosition, moveSpeed));
        }

        private void OnEnable()
        {
            carouselMoveLeftButton.onClick.AddListener(MoveCarouselLeftOnButtonPress);
            carouselMoveRightButton.onClick.AddListener(MoveCarouselRightOnButtonPress);
        }
    
        private void OnDisable()
        {
            carouselMoveLeftButton.onClick.RemoveListener(MoveCarouselLeftOnButtonPress);
            carouselMoveRightButton.onClick.RemoveListener(MoveCarouselRightOnButtonPress);
        }
        
        private void MoveCarouselLeftOnButtonPress()
        {
            var nodeList = podiumNodeManager.PodiumNodes;
            _endPosition = nodeList[--_currentNodeIndex].transform.position;
            StartCoroutine(MoveCarouselCoroutine(_endPosition, moveSpeed));
        }

        private void MoveCarouselRightOnButtonPress()
        {
            // Stop moving the carousel if it exceeds the number of podiums available
            var nodeList = podiumNodeManager.PodiumNodes;
            var nodeCount = nodeList.Count;
            if (_currentNodeIndex < nodeCount-1)
            {
                _endPosition = nodeList[++_currentNodeIndex].transform.position;
                StartCoroutine(MoveCarouselCoroutine(_endPosition, moveSpeed));
            }
        }

        private IEnumerator MoveCarouselCoroutine(Vector3 endPosition, float speed)
        {
            _isMoving = true;
            carouselMoveLeftButton.interactable = false;
            carouselMoveRightButton.interactable = false;
            float scale = (transform.localScale.x + transform.localScale.y + transform.localScale.z)/3;
            float speedAtScale = scale * speed;
            while (Vector3.Distance(transform.position,endPosition) > speedAtScale * Time.deltaTime)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, speedAtScale * Time.deltaTime);
                yield return 0;
            }
            var nodeCount = podiumNodeManager.PodiumNodes.Count;
            carouselMoveLeftButton.gameObject.SetActive(_currentNodeIndex > 0);
            carouselMoveRightButton.gameObject.SetActive(_currentNodeIndex < nodeCount-1);
            carouselMoveLeftButton.interactable = true;
            carouselMoveRightButton.interactable = true;
            transform.position = endPosition;
            _isMoving = false;
        }
    }
}
