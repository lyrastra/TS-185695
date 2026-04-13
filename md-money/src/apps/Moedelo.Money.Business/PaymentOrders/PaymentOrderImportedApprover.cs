using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderImportedApprover))]
    internal class PaymentOrderImportedApprover : IPaymentOrderImportedApprover
    {
        private readonly IPaymentOrderApiClient apiClient;

        public PaymentOrderImportedApprover(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public Task ApproveAsync(int? settlementAccountId, DateTime? skipline)
        {
            return apiClient.ApproveImportedAsync(settlementAccountId, skipline);
        }
    }
}