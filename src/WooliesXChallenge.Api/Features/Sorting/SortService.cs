using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;
using WooliesXChallenge.Api.Features.Common;

namespace WooliesXChallenge.Api.Features.Sorting
{
    public interface ISortService
    {
        Task<(List<Product>,ModelStateDictionary)> GetSortedProducts(string sort);
    }

    public class SortService : ISortService
    {
        private readonly IResourcesApiClient _resourcesApiClient;
        private readonly ILogger _logger;

        public SortService(IResourcesApiClient resourcesApiClient)
        {
            _resourcesApiClient = resourcesApiClient;
            _logger = Log.ForContext<SortService>();
        }

        public async Task<(List<Product>, ModelStateDictionary)> GetSortedProducts(string sort)
        {
            SortOptions sortOption;

            if (string.IsNullOrWhiteSpace(sort))
            {
                sortOption = SortOptions.None;
            }
            else if (!Enum.TryParse(sort, true, out sortOption))
            {
                var errors = new ModelStateDictionary();
                errors.AddModelError("Sort", "Invalid Sort Option");
                return (null, errors);
            }

            var products = await _resourcesApiClient.GetProducts();
            _logger.Information("Sorting products by {SortOption}", sortOption);

            switch (sortOption)
            {
                case SortOptions.Low:
                    return (products.OrderBy(p => p.Price).ToList(), null);

                case SortOptions.High:
                    return (products.OrderByDescending(p => p.Price).ToList(), null);

                case SortOptions.Ascending:
                    return (products.OrderBy(p => p.Name).ToList(),null);

                case SortOptions.Descending:
                    return (products.OrderByDescending(p => p.Name).ToList(),null);

                case SortOptions.Recommended:
                    return (await SortProductsByPopularity(products),null);

                default:
                    return (products,null);
            }
        }

        private async Task<List<Product>> SortProductsByPopularity(List<Product> products)
        {
            var customersProducts = await _resourcesApiClient.GetCustomersProducts();

            var popularProductsQuantities = customersProducts.SelectMany(c => c.Products).GroupBy(g => g.Name)
                .ToDictionary(k => k.Key, v => v.Sum(p => p.Quantity)).OrderByDescending(kv => kv.Value);

            var popularProducts = popularProductsQuantities.Join(products,
                prodQuant => prodQuant.Key,
                product => product.Name,
                (prodQuant, product) => product);

            return popularProducts.Union(products).ToList();

        }
    }
}
