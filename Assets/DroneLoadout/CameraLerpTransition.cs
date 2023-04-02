using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout
{
    /// <summary>
    /// Responsible for smoothly lerping the drone loadout camera from display
    /// view to edit view.
    /// </summary>
    public class CameraLerpTransition : MonoBehaviour
    {
        [SerializeField] private Button editButton;
        [SerializeField] private Button displayButton;
        [Space]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera focusCamera;
        [Space]
        [SerializeField] private Transform editCameraPosition;
        [SerializeField] private Transform displayCameraPosition;
    
        private void OnEnable()
        {
            editButton.onClick.AddListener(ZoomIn);
            displayButton.onClick.AddListener(ZoomOut);
        }

        private void OnDisable()
        {
            editButton.onClick.RemoveListener(ZoomIn);
            displayButton.onClick.RemoveListener(ZoomOut);
        }
    
        private void ZoomIn() // LerpToEditCameraPosition
        {
            StartCoroutine(LerpCameraToPosition(editCameraPosition.position, 5.0f, editButton, displayButton));
        }
    
        private void ZoomOut() // LerpToDisplayCameraPosition
        {
            StartCoroutine(LerpCameraToPosition(displayCameraPosition.position, 5.0f, displayButton, editButton));
        }

        private IEnumerator LerpCameraToPosition(Vector3 endPosition, float speed, Button button1, Button button2)
        {
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(true);
            button2.interactable = false;
        
            if (mainCamera.gameObject.activeSelf)
            {
                mainCamera.gameObject.SetActive(false);
                focusCamera.gameObject.SetActive(true);
            }
            else
            {
                mainCamera.gameObject.SetActive(true);
                focusCamera.gameObject.SetActive(false);
            }
            while (Vector3.Distance(focusCamera.transform.position,endPosition) > speed * Time.deltaTime)
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, endPosition, speed * Time.deltaTime);
                focusCamera.transform.position = Vector3.Lerp(focusCamera.transform.position, endPosition, speed * Time.deltaTime);
                yield return 0;
            }
            button2.interactable = true;
            //displayCamera.transform.position = endPosition;
        }
    }
}
