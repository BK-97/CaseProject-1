public interface ICollectable
{
    void Initialize();
    void Collect(ICollector collector);
    void Demolish();
}
