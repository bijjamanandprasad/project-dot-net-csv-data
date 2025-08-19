using ClientApi.Models;

namespace ClientApi.Services;

public interface IClientService
{
    Task<IEnumerable<Client>> GetClientsByCountryCodeAsync(string countryCode);
} 