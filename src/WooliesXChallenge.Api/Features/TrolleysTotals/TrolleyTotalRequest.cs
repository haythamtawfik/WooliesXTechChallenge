using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WooliesXChallenge.Api.Features.Common;

namespace WooliesXChallenge.Api.Features.TrolleysTotals
{
    public class TrolleyTotalRequest
    {
        [Required]
        public List<Product> Products { get; set; }
        [Required]
        public List<ProductQuantity> Quantities { get; set; }
        [Required]
        public List<Special> Specials { get; set; }
    }
    
    public class ProductQuantity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }

    public class Special
    {
        public List<ProductQuantity> Quantities { get; set; }
        public decimal Total { get; set; }
    }
}