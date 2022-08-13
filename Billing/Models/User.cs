namespace Billing.Models;

public class User
{
    public User()
    {
        this.Balance = new Balance();
    }

    public User(Balance balance)
    {
        this.Balance = balance;
    }

    public string? Name { get; set; }
    
    public int Rating { get; set; }
    
    public Balance Balance { get; set; }
}