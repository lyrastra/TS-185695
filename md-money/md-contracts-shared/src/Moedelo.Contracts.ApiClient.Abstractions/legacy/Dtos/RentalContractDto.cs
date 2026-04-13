namespace Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos
{
    public class RentalContractDto : ContractDto
    {
        public bool IsBuyout { get; set; }
        public bool IsUseNds { get; set; }
    }
}