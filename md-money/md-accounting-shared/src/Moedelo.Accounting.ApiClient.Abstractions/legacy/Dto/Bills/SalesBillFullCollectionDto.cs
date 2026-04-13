namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.Bills
{
    public class SalesBillFullCollectionDto
    {
        public int Count { get; set; }

        public SalesBillFullDto[] ResourceList { get; set; }

        public int TotalCount { get; set; }
    }
}