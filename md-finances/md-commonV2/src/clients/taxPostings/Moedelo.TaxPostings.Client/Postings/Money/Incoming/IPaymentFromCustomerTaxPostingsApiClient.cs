using Moedelo.TaxPostings.Dto.Postings.Dto;
using Moedelo.TaxPostings.Dto.Postings.Money.Incoming.Dto;
using System.Threading.Tasks;

namespace Moedelo.TaxPostings.Client.Postings.Money.Incoming
{
    public interface IPaymentFromCustomerTaxPostingsApiClient
    {
        Task<ITaxPostingsResponseDto<ITaxPostingDto>> GenerateAsync(int firmId, int userId, PaymentFromCustomerGenerateRequestDto request);
    }
}
