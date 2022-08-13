using Billing.Interfaces;
using Billing.Models;
using Billing.Repository.Models;

namespace Billing.Extensions;


public static class UserExtensions
{
    public static User UserModelToUser(this UserModel model)
    {
        var user = new User()
        {
            Name = model.Name,
            Rating = model.Rating,
            
        };

        return user;
    }

    public static void AddCoins(this User user, long coinsCount, IIdGenerator idGenerator)
    {
        var coins = new List<CoinBusinessModel>();
        for (var i = 0; i < coinsCount; i++)
        {
            coins.Add(new (user, idGenerator.NextValue));
        }
        user.Balance.AddCoins(coins);
    }
    
    public static IEnumerable<int> Distribute(IEnumerable<double> weights, long amount)
    {
        var weightsList = weights.ToList();
        var totalWeight = weightsList.Sum();
        if (totalWeight == 0)
        {
            throw new ArgumentException("Sum of users rating equal 0.");
        }
        
        var length = weightsList.Count;   

        var actual = new double[length];
        var error = new double[length];
        var rounded = new int[length];

        var added = 0;

        var i = 0;
        foreach (var w in weightsList)
        {
            actual[i] = amount * (w / totalWeight);
            rounded[i] = (int)Math.Floor(actual[i]);
            error[i] = actual[i] - rounded[i];
            added += rounded[i];
            i += 1;
        }

        while (added < amount)
        {
            var maxError = 0.0;
            var maxErrorIndex = -1;
            for(var e = 0; e  < length; ++e)
            {
                if (rounded[e] < 1)
                {
                    rounded[e] += 1;
                    added += 1;
                }
                if (error[e] > maxError)
                {
                    maxError = error[e];
                    maxErrorIndex = e;
                }
            }

            rounded[maxErrorIndex] += 1;
            error[maxErrorIndex] -= 1;

            added += 1;
        }

        return rounded;
    }
}