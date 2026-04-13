using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Contracts.ApiClient.Abstractions.legacy;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Contracts
{
    [InjectAsSingleton(typeof(RentalPaymentItemReader))]
    internal sealed class RentalPaymentItemReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IContractsApiClient contractsClient;

        public RentalPaymentItemReader(IExecutionInfoContextAccessor contextAccessor, IContractsApiClient contractsClient)
        {
            this.contextAccessor = contextAccessor;
            this.contractsClient = contractsClient;
        }


        public async Task<IReadOnlyCollection<RentalPaymentItem>> GetByIdsAsync(IReadOnlyCollection<int> ids)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var dtos = await contractsClient.GetRentalPaymentItemsByIdsAsync(context.FirmId, context.UserId, ids);
            return dtos.Select(Map).ToArray();
        }

        private static RentalPaymentItem Map(RentalPaymentItemDto x)
        {
            return new RentalPaymentItem
            {
                Id = x.Id,
                PaymentDate = x.PaymentDate,
                AdvanceSum = x.AdvanceSum,
                BuyoutSum = x.BuyoutSum,
                PaymentSum = x.PaymentSum
            };
        }
    }
}
