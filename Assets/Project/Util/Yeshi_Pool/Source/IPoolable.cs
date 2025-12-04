namespace Yeshi_Pool
{
    public interface IPoolable
    {
        PoolHolder Holder { get; set; }
        void ResetObject();
        void SetupObject();
        IPoolable Clone();
    }
}