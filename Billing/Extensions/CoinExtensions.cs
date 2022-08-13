using System.Text;
using Billing.Models;

namespace Billing.Extensions;

public static class CoinExtensions
{
    public static Coin CoinBusinessModelToCoin(this CoinBusinessModel coinBusinessModel)
    {
        return new Coin()
        {
            History = coinBusinessModel.History.CoinHistoryToString(),
            Id = coinBusinessModel.Id.GetHashCode(),
        };
    }

    private static string CoinHistoryToString(this IList<User> users)
    {
        var historyBuilder = new StringBuilder();
        foreach (var user in users)
        {
            historyBuilder.Append($"{user.Name} - ");
        }

        return historyBuilder.ToString();
    }
}