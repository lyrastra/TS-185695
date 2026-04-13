using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.Upd;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Docs.Client.Upd
{
    public interface IUpdAutoCompleteApiClient : IDI
    {
        Task<List<UpdAsPossibleInvoiceReasonResponseDto>> GetPossibleInvoiceReasonAsync(int firmId, int userId, UpdAsPossibleInvoiceReasonRequestDto requestDto);

        Task<List<UpdAsReasonForPaymentResponseDto>> ReasonDocumentForPayment(int firmId, int userId, UpdAsReasonForPaymentRequestDto requestDto);
    }
}