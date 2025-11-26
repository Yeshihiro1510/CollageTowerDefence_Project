using System;
using System.Collections.Generic;

namespace Yeshi_Pool
{
    public class PoolHolder
    {
        private readonly Stack<IPoolable> pool = new();

        public T Get<T>(T prototype) where T : IPoolable, IPrototype, new()
        {
            if (!pool.TryPop(out var obj)) return Create(prototype);
            if (obj is not T result) throw new Exception($"[YESHI POOL] Invalid pool type of {typeof(T)}");
            result.Reset();
            return result;
        }

        public T Get<T>() where T : IPoolable, new()
        {
            if (!pool.TryPop(out var obj)) return Create<T>();
            if (obj is not T result) throw new Exception($"[YESHI POOL] Invalid pool type of {typeof(T)}");
            result.Reset();
            return result;
        }

        public void Release(IPoolable obj)
        {
            pool.Push(obj);
        }

        private T Create<T>(T prototype) where T : IPoolable, IPrototype, new()
        {
            var obj = prototype.Clone<T>();
            obj.Pool = this;
            return obj;
        }

        private T Create<T>() where T : IPoolable, new()
        {
            var obj = new T
            {
                Pool = this
            };
            return obj;
        }
    }
}