using System;
using System.Collections.Generic;

namespace Yeshi_Pool
{
    public class ObjectPool
    {
        private readonly Dictionary<Type, PoolHolder> _poolHolders = new();

        public T Get<T>(T prototype) where T : IPoolable, new()
        {
            var type = typeof(T);

            if (_poolHolders.TryGetValue(type, out var holder))
            {
                var obj = holder.Get(prototype);
                obj.SetupObject();
                return obj;
            }

            _poolHolders.Add(type, new PoolHolder());
            return _poolHolders[type].Get(prototype);
        }

        // public T Get<T>() where T : IPoolable, new()
        // {
        //     var type = typeof(T);
        //
        //     if (_poolHolders.TryGetValue(type, out var holder))
        //     {
        //         var obj = holder.Get<T>();
        //         obj.Setup();
        //         return obj;
        //     }
        //
        //     _poolHolders.Add(type, new PoolHolder());
        //     return _poolHolders[type].Get<T>();
        // }
        
        // public void Release

        public void Clear()
        {
            _poolHolders.Clear();
        }
    }
}