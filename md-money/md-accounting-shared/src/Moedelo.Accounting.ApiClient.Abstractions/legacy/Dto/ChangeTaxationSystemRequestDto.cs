using Moedelo.Accounting.Enums;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto
{
    public class ChangeTaxationSystemRequestDto
    {
        public long DocumentBaseId { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }

        public long? PatentId { get; set; }
    }
}
