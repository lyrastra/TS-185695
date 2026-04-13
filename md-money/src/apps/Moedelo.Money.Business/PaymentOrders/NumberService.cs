using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(INumberService))]
    internal sealed class NumberService : INumberService
    {
        private readonly IPaymentOrderApiClient paymentOrderApiClient;
        private readonly IExecutionInfoContextAccessor contextAccessor;

        public NumberService(
            IPaymentOrderApiClient paymentOrderApiClient,
            IExecutionInfoContextAccessor contextAccessor)
        {
            this.paymentOrderApiClient = paymentOrderApiClient;
            this.contextAccessor = contextAccessor;
        }

        public async Task<string> GetNextNumberAsync()
        {
            var context = contextAccessor.ExecutionInfoContext;
            var nextNumber = await paymentOrderApiClient
                .FindNextNumberByYearAsync(context.FirmId, context.UserId, DateTime.Now, null)
                .ConfigureAwait(false);
            return nextNumber.ToString();
        }
    }
}