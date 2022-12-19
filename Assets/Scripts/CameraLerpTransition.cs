using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraLerpTransition : MonoBehaviour
{
    [SerializeField] private Button editButton;
    [SerializeField] private Button displayButton;
    [Space]
    [SerializeField] private Camera displayCamera;
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
        button2.gameObject.SetActive(false);
        
        while (Vector3.Distance(displayCamera.transform.position,endPosition) > speed * Time.deltaTime)
        {
            displayCamera.transform.position = Vector3.Lerp(displayCamera.transform.position, endPosition, speed * Time.deltaTime);
            yield return 0;
        }
        
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(true);
        //displayCamera.transform.position = endPosition;
    }
}
