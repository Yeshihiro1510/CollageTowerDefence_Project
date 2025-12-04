using System;
using UnityEngine;

namespace Yeshi_Pool
{
    [DisallowMultipleComponent]
    public class MonoPoolable : MonoBehaviour, IPoolable
    {
        public PoolHolder Holder { get; set; }

        public virtual void Release()
        {
            if (Holder == null) throw new NullReferenceException($"[YESHI POOL] Pool is null for object {gameObject.name}.");
            Holder.Release(this);
        }

        public virtual void ResetObject()
        {
            transform.SetParent(null);
            gameObject.SetActive(false);
        }

        public virtual void SetupObject()
        {
            gameObject.SetActive(true);
        }

        public virtual IPoolable Clone()
        {
            var obj = Instantiate(gameObject);
            var component = obj.GetComponent<IPoolable>();
            return component;
        }
    }
}