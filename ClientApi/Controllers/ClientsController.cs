using Microsoft.AspNetCore.Mvc;
using ClientApi.Models;
using ClientApi.Services;
using ClientApi.Attributes;

namespace ClientApi.Controllers;

[ApiController]
[Route("[controller]")]
[ApiKey] // Add authentication requirement
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly ILogger<ClientsController> _logger;

    public ClientsController(IClientService clientService, ILogger<ClientsController> logger)
    {
        _clientService = clientService;
        _logger = logger;
    }

    /// <summary>
    /// Gets clients filtered by country code
    /// </summary>
    /// <param name="country_code">The country code to filter by (e.g., US, CA, DE, AU, UK)</param>
    /// <returns>Structured response with clients data or error information</returns>
    /// <response code="200">Returns the structured response with clients data</response>
    /// <response code="400">If the country_code parameter is invalid</response>
    /// <response code="401">If the API key is missing or invalid</response>
    /// <response code="500">If there was an internal server error</response>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<Client>>), 200)]
    [ProducesResponseType(typeof(ApiResponse<object>), 400)]
    [ProducesResponseType(typeof(ApiResponse<object>), 401)]
    [ProducesResponseType(typeof(ApiResponse<object>), 500)]
    public async Task<ActionResult<ApiResponse<IEnumerable<Client>>>> GetClients([FromQuery] string country_code)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(country_code))
            {
                _logger.LogWarning("Country code parameter is missing or empty");
                return BadRequest(ApiResponse<object>.CreateError("Country code parameter is required", 400));
            }

            var clients = await _clientService.GetClientsByCountryCodeAsync(country_code);
            
            if (!clients.Any())
            {
                _logger.LogInformation("No clients found for country code: {CountryCode}", country_code);
                return Ok(ApiResponse<IEnumerable<Client>>.Success(
                    Enumerable.Empty<Client>(), 
                    $"No records found for country code: {country_code}", 
                    200));
            }

            _logger.LogInformation("Successfully retrieved {Count} clients for country code: {CountryCode}", 
                clients.Count(), country_code);

            return Ok(ApiResponse<IEnumerable<Client>>.Success(
                clients, 
                $"Successfully retrieved {clients.Count()} clients for country code: {country_code}", 
                200));
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "CSV file not found");
            return StatusCode(500, ApiResponse<object>.CreateError("Data source not available", 500, ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while retrieving clients");
            return StatusCode(500, ApiResponse<object>.CreateError("An unexpected error occurred", 500, ex.Message));
        }
    }
} 