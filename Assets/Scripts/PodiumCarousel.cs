using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PodiumCarousel : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float angleToRotate = 90.0f;
    [SerializeField] private Button carouselSpinLeftButton;
    [SerializeField] private Button carouselSpinRightButton;
    private bool _isRotating = false;

    private void OnEnable()
    {
        carouselSpinLeftButton.onClick.AddListener(SpinCarouselLeft);
        carouselSpinRightButton.onClick.AddListener(SpinCarouselRight);
    }

    private void OnDisable()
    {
        carouselSpinLeftButton.onClick.RemoveListener(SpinCarouselLeft);
        carouselSpinRightButton.onClick.RemoveListener(SpinCarouselRight);
    }

    private void SpinCarouselLeft()
    {
        if(_isRotating) { return; }
        StartCoroutine(RotateCarousel(Vector3.up * -angleToRotate, rotationSpeed));
    }
    private void SpinCarouselRight()
    {
        if(_isRotating) { return; }
        StartCoroutine(RotateCarousel(Vector3.up * angleToRotate, rotationSpeed));
    }
    
    private IEnumerator RotateCarousel(Vector3 angle, float duration)
    {
        _isRotating = true;
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + angle);
        for(var t = 0f; t < 1; t += Time.deltaTime/duration) 
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        transform.rotation = toAngle;
        _isRotating = false;
    }
}
