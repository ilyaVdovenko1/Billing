using Billing.Repository.Models;

namespace Billing.Repository.Interfaces;

public interface IRepositoryService
{
    public Task<IEnumerable<UserModel>> GetUsersAsync();

    public IEnumerable<UserModel> UserModels { get; }
}