using Billing.Interfaces;

namespace Billing.Models;

public class IdGenerator : IIdGenerator
{
    private long state;
    
    public IdGenerator()
    {
        this.state = long.MinValue;
    }
    
    public IdGenerator(long startValue)
    {
        this.state = startValue;
    }

    public long NextValue
    {
        get
        {
            this.state += 1;
            return this.state;
        }
    }
}