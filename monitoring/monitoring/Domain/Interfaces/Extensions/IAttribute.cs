namespace monitoring.Domain.Interfaces.Extensions
{
    public interface IAttribute<out T>
    {
        T Value { get; }
    }
}
