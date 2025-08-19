namespace ClientApi.Models;

public class Client
{
    public int ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
} 