using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Extensions;
using Object = UnityEngine.Object;
namespace Game.Characters.Spawners
{
    [Serializable]
    public class ObjectsPool<T> where T : MonoBehaviour
    {
        [SerializeField]
        Transform root;
        
        [SerializeField]
        T prefab;
        
        [SerializeField]
        int initialCount;
        
        Queue<T> queue = new Queue<T>();
        
        public void Init()
        {
            root.DestroyChildren();
            for (int i = 0; i < initialCount; i++)
            {
                var obj = Object.Instantiate(prefab, root);
                obj.gameObject.SetActive(false);
                queue.Enqueue(obj);
            }
        }
        
        
        public T Spawn(bool setActive)
        {
            T obj;
            
            if(queue.Count > 0)
            {
                obj = queue.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }

            obj = Object.Instantiate(prefab, root);
            obj.gameObject.SetActive(setActive);
            return obj;
        }

        public void Despawn(T obj)
        {
            obj.gameObject.SetActive(false);
            queue.Enqueue(obj);
        }
    }
}