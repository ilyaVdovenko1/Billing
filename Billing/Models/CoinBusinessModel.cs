namespace Billing.Models;

public class CoinBusinessModel
{

    public CoinBusinessModel(User user, long id)
    {
        this.History = new List<User>();
        this.History.Add(user);
        this.Id = id;

    }

    public CoinBusinessModel(long id)
    {
        this.History = new List<User>();

        this.Id = id;
    }

    public long Id { get; }
    
    public IList<User> History { get; }
}