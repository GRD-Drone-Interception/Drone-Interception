using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Drone
{
    public class WorkbenchCarousel : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private Button carouselMoveLeftButton;
        [SerializeField] private Button carouselMoveRightButton;
        [SerializeField] private PodiumNodeManager podiumNodeManager;
        private Vector3 _endPosition;
        private int _currentNodeIndex;
        private bool _isMoving = false;

        private void Start()
        {
            _currentNodeIndex = 3;
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
            if(Vector3.Distance(transform.position, _endPosition) >= 0.05f) { return; }
            _endPosition = podiumNodeManager.PodiumNodes[++_currentNodeIndex].transform.position;
            StartCoroutine(MoveCarouselCoroutine(_endPosition, moveSpeed));
        }
    
        private void MoveCarouselRightOnButtonPress()
        {
            if(Vector3.Distance(transform.position, _endPosition) >= 0.05f) { return; }
            _endPosition = podiumNodeManager.PodiumNodes[--_currentNodeIndex].transform.position;
            StartCoroutine(MoveCarouselCoroutine(_endPosition, moveSpeed));
        }

        private IEnumerator MoveCarouselCoroutine(Vector3 endPosition, float speed)
        {
            carouselMoveLeftButton.gameObject.SetActive(false);
            carouselMoveRightButton.gameObject.SetActive(false);
            float scale = (transform.localScale.x + transform.localScale.y + transform.localScale.z)/3;
            float speedAtScale = scale * speed;
            while (Vector3.Distance(transform.position,endPosition) > speedAtScale * Time.deltaTime)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, speedAtScale * Time.deltaTime);
                yield return 0;
            }
            carouselMoveLeftButton.gameObject.SetActive(true);
            carouselMoveRightButton.gameObject.SetActive(true);
            transform.position = endPosition;
        }
    }
}
