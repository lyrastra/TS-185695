using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.PaymentBills;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BillingV2.Client.PaymentBill
{
    public interface IPaymentBillApiClient : IDI
    {
        Task<List<PaymentsAndBillsCreationResultDto>> CreatePaymentsAndBillsAsync(
            PaymentsAndBillsCreationRequestDto requestDto);
    }
}