using Billing.Models;

namespace Billing.Interfaces;

public interface IAppService
{
    public IEnumerable<User> ListUsers();
    
    public void CoinsEmission(long emissionAmount);
    
    public void MoveCoins(User sender, User recipient, long amount);
    
    public CoinBusinessModel CoinsWithLongestHistory();

    public User FindUserByName(string name);
}