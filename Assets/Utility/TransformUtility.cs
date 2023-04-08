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
    }
}