using UnityEngine;

namespace Utilities.Extensions
{
    public static class TransformExtension
    {
        public static void DestroyChildren(this Transform transform)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Object.DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
        
    }
}