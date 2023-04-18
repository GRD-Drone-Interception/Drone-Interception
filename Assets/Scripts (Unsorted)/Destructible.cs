using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private List<Rigidbody> _rigidbodies = new();
    //private Dictionary<MeshRenderer, Material[]> _originalMaterials = new();

    private void Awake()
    {
        _rigidbodies.AddRange(GetComponentsInChildren<Rigidbody>());
        
        /*MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
        if (meshRenderer != null)
        {
            // Save the original materials for this mesh renderer if they haven't already been saved
            if (!_originalMaterials.ContainsKey(meshRenderer))
            {
                _originalMaterials[meshRenderer] = meshRenderer.materials;
            }
            
            TransformUtility.GetMaterialsRecursively(transform, _originalMaterials);
        }*/
    }
    
    /*public void Paint(Color colour)
    {
        foreach (var meshRenderer in _originalMaterials.Keys)
        {
            meshRenderer.material.color = colour;
        }
    }*/

    public List<Rigidbody> GetRigidbodies()
    {
        return _rigidbodies;
    }
}
