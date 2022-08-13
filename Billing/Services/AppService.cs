using Billing.Extensions;
using Billing.Interfaces;
using Billing.Models;
using Billing.Repository.Interfaces;

namespace Billing.Services;

public class AppService : IAppService
{
    private readonly IRepositoryService repositoryService;

    private readonly List<User> users;

    private readonly IIdGenerator idGenerator;


    public AppService(IRepositoryService repositoryService, IIdGenerator idGenerator)
    {
        this.idGenerator = idGenerator;
        this.repositoryService = repositoryService;
        var userModels = this.repositoryService.UserModels;
        this.users = new List<User>(userModels.Select(model => model.UserModelToUser()));
    }
    

    public  IEnumerable<User> ListUsers()
    {
        
        return this.users;
    }

    public void CoinsEmission(long emissionAmount)
    {
        var distribute = UserExtensions.Distribute(this.users.Select(user => (double) user.Rating), emissionAmount).ToList();
        var distributeId = 0;

        foreach (var user in this.users)
        {
            if (distributeId >= 0 && distribute.Count > distributeId)
            {
                user.AddCoins(distribute[distributeId], this.idGenerator);
            }
                
            
            distributeId++;
        }


    }

    public void MoveCoins(User sender, User recipient, long amount)
    {
        var coins = new List<CoinBusinessModel>(sender.Balance.RemoveCoins(amount));
        foreach (var coin in coins)
        {
            coin.History.Add(recipient);
        }
        
        recipient.Balance.AddCoins(coins);
    }

    public CoinBusinessModel CoinsWithLongestHistory()
    {
        var coinWithMaxHistory = this.users[0].Balance.CoinsWithLongestHistoryOnBalance();
        foreach (var currentMaxCoin in this.users.Select(user => user.Balance.CoinsWithLongestHistoryOnBalance()).Where(currentMaxCoin => currentMaxCoin.History.Count > coinWithMaxHistory.History.Count))
        {
            coinWithMaxHistory = currentMaxCoin;
        }

        return coinWithMaxHistory;
    }

    public User FindUserByName(string name)
    {
        return this.users.Find(user => string.Equals(user.Name, name, StringComparison.InvariantCultureIgnoreCase)) 
               ?? throw new ArgumentException("No user with this name");
    }
}