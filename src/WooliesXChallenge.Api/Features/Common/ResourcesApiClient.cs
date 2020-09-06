using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Serilog;
using WooliesXChallenge.Api.Features.TrolleysTotals;


namespace WooliesXChallenge.Api.Features.Common
{
    public interface IResourcesApiClient
    {
        Task<List<Product>> GetProducts();
        Task<List<CustomerProducts>> GetCustomersProducts();
        Task<decimal> CalculateMinTrolleyTotal(TrolleyTotalRequest request);
    }

    public class ResourcesApiClient : IResourcesApiClient
    {
        private readonly ResourceApiSettings _resourceApiSettings;
        private readonly ApiSettings _apiSettings;
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public ResourcesApiClient(IOptions<ResourceApiSettings> resourceApiSettings, IOptions<ApiSettings> apiSettings, HttpClient httpClient)
        {
            _resourceApiSettings = resourceApiSettings.Value;
            _apiSettings = apiSettings.Value;
            _httpClient = httpClient;
            _logger = Log.ForContext<ResourcesApiClient>();
        }
        public async Task<List<Product>> GetProducts()
        {
            var response = await _httpClient.GetAsync($"{_resourceApiSettings.Url}/products?token={_apiSettings.Token}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.Warning("Call to Get Products failed with status code: {StatusCode}", response.StatusCode);
                response.EnsureSuccessStatusCode();
            }

            var products = await response.Content.ReadAsAsync<List<Product>>();
            
            return products;
        }

        public async Task<List<CustomerProducts>> GetCustomersProducts()
        {
            var response = await _httpClient.GetAsync($"{_resourceApiSettings.Url}/shopperHistory?token={_apiSettings.Token}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.Warning("Call to Get Customers Products failed with status code: {StatusCode}", response.StatusCode);
                response.EnsureSuccessStatusCode();
            }

            var customersProducts = await response.Content.ReadAsAsync<List<CustomerProducts>>();

            return customersProducts;
        }

        public async Task<decimal> CalculateMinTrolleyTotal(TrolleyTotalRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_resourceApiSettings.Url}/trolleyCalculator?token={_apiSettings.Token}",request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.Warning("Call to Calculate trolley total failed with status code: {StatusCode}", response.StatusCode);
                response.EnsureSuccessStatusCode();
            }

            _logger.Information("Call to Calculate trolley total succeeded");
            var total = await response.Content.ReadAsAsync<decimal>();

            return total;
        }
    }
}
