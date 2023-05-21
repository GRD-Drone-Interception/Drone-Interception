using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private readonly List<Rigidbody> _rigidbodies = new();

    private void Awake()
    {
        _rigidbodies.AddRange(GetComponentsInChildren<Rigidbody>());
    }

    public List<Rigidbody> GetRigidbodies()
    {
        return _rigidbodies;
    }
}
