using System.Collections;
using UnityEngine;

public class InterceptorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boxLidJoint;
    private bool _rotating;
    private Quaternion _startRotation;
    private Quaternion _targetRotation;

    private void Start()
    {
        // Set the start and target rotations
        _startRotation = boxLidJoint.transform.rotation;
        _targetRotation = Quaternion.Euler(_startRotation.eulerAngles + new Vector3(120, 0, 0));
    }

    private void Update()
    {
        if (_rotating)
        {
            // Rotate towards the target rotation
            boxLidJoint.transform.rotation = Quaternion.RotateTowards(boxLidJoint.transform.rotation, _targetRotation, 120.0f * Time.deltaTime);

            // If we reach the target rotation, switch to rotating back to the starting rotation
            if (boxLidJoint.transform.rotation == _targetRotation)
            {
                StartCoroutine(RotateBackCoroutine());
            }
        }
    }
    
    [ContextMenu("Test")]
    public void SpawnInterceptor()
    {
        StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        _rotating = true;
        yield return new WaitForSeconds(5f);
        StartCoroutine(RotateBackCoroutine());
    }

    private IEnumerator RotateBackCoroutine()
    {
        yield return new WaitForSeconds(2f);

        _rotating = true;
        _targetRotation = _startRotation;
    }
}



