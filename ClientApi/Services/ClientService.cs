using CsvHelper;
using CsvHelper.Configuration;
using ClientApi.Models;
using System.Globalization;

namespace ClientApi.Services;

public class ClientService : IClientService
{
    private readonly ILogger<ClientService> _logger;
    private readonly string _csvFilePath;

    public ClientService(ILogger<ClientService> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _csvFilePath = Path.Combine(environment.ContentRootPath, "..", "Data", "clients.csv");
    }

    public async Task<IEnumerable<Client>> GetClientsByCountryCodeAsync(string countryCode)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(countryCode))
            {
                _logger.LogWarning("Country code parameter is null or empty");
                return Enumerable.Empty<Client>();
            }

            if (!File.Exists(_csvFilePath))
            {
                _logger.LogError("CSV file not found at path: {FilePath}", _csvFilePath);
                throw new FileNotFoundException($"CSV file not found at path: {_csvFilePath}");
            }

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null
            };

            using var reader = new StreamReader(_csvFilePath);
            using var csv = new CsvReader(reader, config);

            var clients = await csv.GetRecordsAsync<Client>().ToListAsync();
            
            var filteredClients = clients
                .Where(c => string.Equals(c.CountryCode, countryCode, StringComparison.OrdinalIgnoreCase))
                .ToList();

            _logger.LogInformation("Found {Count} clients for country code: {CountryCode}", 
                filteredClients.Count, countryCode);

            return filteredClients;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while reading clients for country code: {CountryCode}", countryCode);
            throw;
        }
    }
} 