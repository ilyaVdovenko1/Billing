namespace Billing.Models;

public class Balance
{
    public Balance(IList<CoinBusinessModel> coins)
    {
        this.Coins = coins;
    }

    public Balance()
    {
        this.Coins = new List<CoinBusinessModel>();
    }

    public void AddCoins(IEnumerable<CoinBusinessModel> coins)
    {
        foreach (var coin in coins)
        {
            this.Coins.Add(coin);
        }
    }

    public IList<CoinBusinessModel> RemoveCoins(long count)
    {
        var tempCoinList = new List<CoinBusinessModel>();
        for (long i = 0; i < count; i++)
        {
            if (i < this.Coins.Count)
            {
                tempCoinList.Add(this.Coins[(Index) i]);
            }
        }
        
        if (tempCoinList.Count != count)
        {
            throw new ArgumentException("Coin amount on balance not match with requested amount");
        }
        
        foreach (var deletedCoin in tempCoinList)
        {
            this.Coins.Remove(deletedCoin);
        }

        return tempCoinList;
    }

    public CoinBusinessModel CoinsWithLongestHistoryOnBalance()
    {
        var maxCoinHistoryLengthCoinId = 0;
        var maxCoinHistoryLength = this.Coins[maxCoinHistoryLengthCoinId].History.Count;
        
        for (var i = 0; i < this.Coins.Count; i++)
        {
            if (this.Coins[i].History.Count > maxCoinHistoryLength)
            {
                maxCoinHistoryLengthCoinId = i;
                maxCoinHistoryLength = this.Coins[i].History.Count;
            }
        }

        return this.Coins[maxCoinHistoryLengthCoinId];
    }

    public IList<CoinBusinessModel> Coins { get; }
    
    
    
    
}

