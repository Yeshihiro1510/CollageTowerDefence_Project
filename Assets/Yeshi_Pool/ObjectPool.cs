using System;
using System.Collections.Generic;

namespace Yeshi_Pool
{
    public class ObjectPool
    {
        private readonly Dictionary<Type, PoolHolder> _poolHolders = new();

        public T Get<T>(T prototype) where T : IPoolable, IPrototype, new()
        {
            var type = typeof(T);

            if (_poolHolders.TryGetValue(type, out var holder))
            {
                var obj = holder.Get(prototype);
                obj.Reset();
                return obj;
            }

            _poolHolders.Add(type, new PoolHolder());
            return _poolHolders[type].Get(prototype);
        }

        public T Get<T>() where T : IPoolable, new()
        {
            var type = typeof(T);

            if (_poolHolders.TryGetValue(type, out var holder))
            {
                var obj = holder.Get<T>();
                obj.Reset();
                return obj;
            }

            _poolHolders.Add(type, new PoolHolder());
            return _poolHolders[type].Get<T>();
        }

        public void Release<T>(T obj) where T : IPoolable
        {
            var type = typeof(T);

            if (_poolHolders.TryGetValue(type, out var holder))
            {
                holder.Release(obj);
            }
            
            _poolHolders.Add(type, new PoolHolder());
            _poolHolders[type].Release(obj);
        }
    }
}