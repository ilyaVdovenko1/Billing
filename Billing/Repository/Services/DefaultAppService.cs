using Billing.Repository.Interfaces;
using Billing.Repository.Models;
using Newtonsoft.Json;

namespace Billing.Repository.Services;

public class DefaultAppService : IRepositoryService
{
    private readonly FileStream fileStream;

    public DefaultAppService(FileStream fileStream)
    {
        this.fileStream = fileStream;
    }

    public async Task<IEnumerable<UserModel>> GetUsersAsync()
    {
        await using var stream = fileStream;
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();
        return JsonConvert.DeserializeObject<IEnumerable<UserModel>>(json) ?? new List<UserModel>();
    }
    
    private IEnumerable<UserModel> GetUsers()
    {
        using var stream = fileStream;
        using var reader = new StreamReader(stream);
        var json =  reader.ReadToEnd();
        return JsonConvert.DeserializeObject<IEnumerable<UserModel>>(json) ?? new List<UserModel>();
    }

    public IEnumerable<UserModel> UserModels => GetUsers();
}