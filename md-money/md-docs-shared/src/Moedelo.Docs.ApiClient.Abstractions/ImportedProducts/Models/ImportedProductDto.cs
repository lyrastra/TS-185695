using System;

namespace Moedelo.Docs.ApiClient.Abstractions.ImportedProducts.Models
{
    public class ImportedProductDto
    {
        public DateTime ImportDate { get; set; }
        public string Country { get; set; }
        public string Declaration { get; set; }
        public int Remains { get; set; }
    }
}