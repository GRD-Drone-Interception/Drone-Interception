using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class TransformUtility
    {
        /// <summary>
        /// Performs an iterative depth-first search traversal on a given transform hierarchy
        /// in order to assign each of it's children with a given layer
        /// </summary>
        public static void SetChildLayersIteratively(Transform transform, string layer)
        {
            var stack = new Stack<Transform>();
            stack.Push(transform);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                current.gameObject.layer = LayerMask.NameToLayer(layer);
                for (int i = 0; i < current.childCount; i++)
                {
                    stack.Push(current.GetChild(i));
                }
            }
        }
        
        /// <summary>
        /// Recursively gets the materials used by all MeshRenderers in the given Transform and its children,
        /// and adds them to the provided dictionary of original materials.
        /// </summary>
        /// <param name="currentTransform">The Transform to search for MeshRenderers.</param>
        /// <param name="originalMaterials">The dictionary of original materials to add the MeshRenderer materials to.</param>
        public static void GetMaterialsRecursively(Transform currentTransform, Dictionary<MeshRenderer, Material[]> originalMaterials)
        {
            var meshRenderer = currentTransform.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                if (!originalMaterials.ContainsKey(meshRenderer))
                {
                    originalMaterials[meshRenderer] = meshRenderer.materials;
                }
            }

            // Recurse over children
            foreach (Transform child in currentTransform)
            {
                GetMaterialsRecursively(child, originalMaterials);
            }
        }
    }
}