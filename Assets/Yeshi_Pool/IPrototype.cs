namespace Yeshi_Pool
{
    public interface IPrototype
    {
        T Clone<T>() where T : IPrototype, new();
    }
}