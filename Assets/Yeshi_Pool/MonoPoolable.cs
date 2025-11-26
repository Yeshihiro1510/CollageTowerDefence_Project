using System;
using UnityEngine;

namespace Yeshi_Pool
{
    public class MonoPoolable : MonoBehaviour, IPoolable, IPrototype
    {
        public PoolHolder Pool { get; set; }

        public virtual void Release()
        {
            if (Pool == null) throw new NullReferenceException($"[YESHI POOL] Pool is null for object {gameObject.name}.");
            
            Pool.Release(this);
            gameObject.SetActive(false);
        }

        public virtual void Reset()
        {
            gameObject.SetActive(true);
        }

        public T Clone<T>() where T : IPrototype, new()
        {
            var obj = Instantiate(gameObject);
            var component = obj.GetComponent<T>();
            return component;
        }
    }
}