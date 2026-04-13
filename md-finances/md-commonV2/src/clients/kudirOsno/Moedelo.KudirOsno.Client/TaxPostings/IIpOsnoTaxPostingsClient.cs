using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.KudirOsno.Client.TaxPostings.Dto;

namespace Moedelo.KudirOsno.Client.TaxPostings
{
    public interface IIpOsnoTaxPostingsClient : IDI
    {
        Task<PaymentTaxPostingsResponseDto> GetByPaymentBaseIdAsync(int firmId, int userId, long paymentBaseId);

        Task<DocumentTaxPostingsResponseDto> GetByDocumentBaseIdAsync(int firmId, int userId, long documentBaseId);

        Task<TaxSumsDto[]> GetPaymentsTaxSumsAsync(int firmId, int userId, IReadOnlyCollection<long> paymentBaseIds);
    }
}