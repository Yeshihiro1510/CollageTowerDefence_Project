namespace Yeshi_Pool
{
    public interface IPoolable
    {
        PoolHolder Pool { get; set; }
        void Release();
        void Reset();
    }
}