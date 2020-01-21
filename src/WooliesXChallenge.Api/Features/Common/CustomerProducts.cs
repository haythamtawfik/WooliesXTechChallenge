using System.Collections.Generic;

namespace WooliesXChallenge.Api.Features.Common
{
    public class CustomerProducts
    {
        public int CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}