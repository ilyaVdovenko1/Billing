namespace Billing.Interfaces;

public interface IIdGenerator
{
    public long NextValue { get; }
}