using System.Collections;
using UnityEngine;

public class ExplodeOnDelay : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
