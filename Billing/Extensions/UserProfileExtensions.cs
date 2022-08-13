using Billing.Models;

namespace Billing.Extensions;

public static class UserProfileExtensions
{
    public static UserProfile UserToUserProfile(this User user)
    {
        return new UserProfile()
        {
            Amount = user.Balance.Coins.Count,
            Name = user.Name,
        };
    }
}