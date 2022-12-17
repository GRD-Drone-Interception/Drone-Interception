using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Drone
{
    public class DroneCarousel : MonoBehaviour
    {
        public static DroneCarousel Instance; 
        
        [SerializeField] private float moveSpeed = 1f;
        [Range(1, 10)][SerializeField] private float distanceToMove = 5.0f;
        [SerializeField] private Button carouselMoveLeftButton;
        [SerializeField] private Button carouselMoveRightButton;
        //[SerializeField] private PodiumNodeManager podiumNodeManager;
        private Vector3 _endPosition;
        private bool _isMoving = false;

        private void Start()
        {
            _endPosition = transform.position;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
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
            if(Vector3.Distance(transform.position, _endPosition) >= 0.05f) { return;}
            _endPosition = transform.position - Vector3.left*distanceToMove;
            StartCoroutine(MoveCarouselCoroutine(_endPosition, moveSpeed));
        }
    
        private void MoveCarouselRightOnButtonPress()
        {
            if(Vector3.Distance(transform.position, _endPosition) >= 0.05f) { return;}
            _endPosition = transform.position - Vector3.right*distanceToMove;
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
